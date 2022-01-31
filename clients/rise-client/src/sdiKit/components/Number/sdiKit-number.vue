<template>
  <div>
    <b-form-group :label="label" v-bind="layout">
      <b-form-input
        number
        :placeholder="placeholder"
        class="form-control"
        v-model="text"
        type="number"
      ></b-form-input>
      <div class="invalid-tooltip" v-if="state.$error && !state.required">Bu alan gereklidir</div>
      <small class="form-text text-muted pl-1" v-if="$slots.info">
        <slot name="info"></slot>
      </small>
    </b-form-group>
  </div>
</template>

<script>
import shared from "~/mixins/components";

export default {
  props: {
    value: {
      type: Number
    },
    placeholder: {
      type: String
    },
    disabled: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    text: {
      get() {
        if (this.value) {
          return this.value;
        }
      },
      set(val) {
        this.$emit("input", val);
      }
    }
  },
  mixins: [shared]
};
</script>
