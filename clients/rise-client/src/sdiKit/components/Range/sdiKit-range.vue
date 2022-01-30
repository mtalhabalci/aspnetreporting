<template>
  <div>
    <b-form-group :label="label" v-bind="layout">
      <b-form-input v-model="rangeValue" type="range" :min="5" :max="maxLength" :step="step"></b-form-input>
      <div class="mt-2">
        {{ rangeValue }}
        <span v-if="suffix">({{suffix}})</span>
      </div>
      <div
        class="invalid-tooltip"
        v-if="state.$error && state.maxLength == false"
      >Bu alana en çok {{state.$params.maxLength.max}} karakter girebilirsiniz.</div>
    </b-form-group>
  </div>
</template>

<script>
import shared from "~/mixins/components";

export default {
  props: {
    value: {
      type: Number,
      default: 5
    },
    step: {
      type: Number,
      default: 5
    },
    maxLength: {
      required: true,
      type: Number,
      default: 500
    },
    suffix: {
      type: String,
      default: ""
    }
  },
  computed: {
    rangeValue: {
      get() {
        if (this.value) {
          return parseInt(this.value);
        }
        return;
      },
      set(val) {
        if (val == null) {
          this.$emit("input", null);
          return;
        }
        this.$emit("input", parseInt(val));
      }
    }
  },
  mixins: [shared]
};
</script>