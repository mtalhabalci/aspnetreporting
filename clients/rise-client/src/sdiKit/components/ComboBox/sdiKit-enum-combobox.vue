<template>
  <div>
    <b-form-group :label="label" v-bind="layout" :class="{ 'input-required': attrIsRequired } ">
      <b-form-select v-model="selected" :options="options" v-bind:class="validate"></b-form-select>
      <div
        class="invalid-tooltip"
        v-if="state && state.$error && !state.required"
      >Bu alan gereklidir</div>
    </b-form-group>
  </div>
</template>

<script>
import { Ajax, Storage } from "~/utils/index";
import Components from "~/mixins/components";

import _ from "lodash";

export default {
  mixins: [Components],
  props: {
    value: {
      type: [String, Number],
      default: ""
    },
    enum: {
      type: String,
      required: true
    }
  },
  data: () => ({
    options: [
      {
        value: null,
        text: "Seçiniz"
      }
    ]
  }),
  mounted() {
    let self = this;

    function ListItem(value, text) {
      this.value = value;
      this.text = text;
    }

    let enumData = Storage.getEnum(self.enum);
    if (enumData !== null) {
      for (let i = 0; i < enumData.length; i++) {
        self.options.push(new ListItem(enumData[i].value, enumData[i].display));
      }
    } else {
      Ajax.get("/enums/" + this.enum, function(response) {
        let result = response.result;
        if (result && result.length > 0) {
          for (let i = 0; i < result.length; i++) {
            self.options.push(new ListItem(result[i].value, result[i].text));
          }
        }
      });
    }
  },
  computed: {
    selected: {
      get() {
        return this.value;
      },
      set(val) {
        let value = _.find(this.options, e => e.value == val);
        if (value) {
          this.$emit("input", val);
          this.$emit("valuechanged", value.text, this.label);
        }
      }
    },
    validate() {
      if (this.state.$error && !this.state.required) {
        return "form-control is-invalid";
      }
      return "";
    }
  }
};
</script>