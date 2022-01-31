<template>
  <b-form-group :label="label" v-bind="layout">
    <verte picker="square" v-model="hexValue" model="hex" :show-history="true" menuPosition="top"></verte>
    <small class="form-text text-muted pl-1" v-if="$slots.info">
      <slot name="info"></slot>
    </small>
  </b-form-group>
</template>

<script>
import verte from "verte";
import shared from "~/mixins/components";
import "verte/dist/verte.css";

export default {
  name: "sdiKit.color.picker",
  props: {
    value: {
      type: String,
      default: "#000"
    }
  },
  components: {
    verte
  },
  computed: {
    hexValue: {
      get() {
        if (this.value) {
          return this.value;
        }
        return;
      },
      set(val) {
        this.$emit("input", val);
      }
    }
  },
  mixins: [shared]
};
</script>

<style scoped>
.verte {
  justify-content: flex-start;
}
</style>