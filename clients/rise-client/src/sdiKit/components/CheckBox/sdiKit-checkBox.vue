<template>
  <div>
    <b-form-group v-if="hasFormGroup" :label="label" v-bind="layout">
      <b-form-checkbox v-model="checked" class="checkBox selection-none">
        <slot name="text"></slot>
      </b-form-checkbox>
    </b-form-group>

    <b-form-checkbox v-if="!hasFormGroup" v-model="checked" class="checkBox selection-none">
      <slot name="text">{{ label }}</slot>
    </b-form-checkbox>
  </div>
</template>

<script>
import shared from "~/mixins/components";
export default {
  props: {
    value: {
      type: Boolean,
      default: false
    },
    label: {
      type: String,
      default: "Etiket Yok"
    },
    hasFormGroup: {
      type: Boolean
    }
  },
  computed: {
    checked: {
      get() {
        return this.value;
      },
      set(val) {
        this.$emit("input", val);
      }
    }
  },
  mixins: [shared]
};
</script>
<style>
.checkBox {
  margin-top: 7px;
}
</style>