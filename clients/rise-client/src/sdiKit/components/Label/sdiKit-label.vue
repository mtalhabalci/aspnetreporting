<template>
  <b-form-group :label="getLabel" v-bind="layout">
    <div class="form-control-plaintext">{{ text }}</div>
  </b-form-group>
</template>

<script>
export default {
  props: {
    labelCols: {
      type: Number,
      default: 2
    },
    label: {
      type: String,
      required: true,
      default: "Etiket Yok"
    },
    value: {
      type: [String, Number, Boolean]
    },
    direction: {
      type: String,
      validator: val => ["vertical", "horizontal"].includes(val),
      default: "vertical"
    },
    hasColon: {
      type: Boolean,
      default: false
    }
  },
  computed: {
    getLabel() {
      return this.label + (this.hasColon ? ": " : "");
    },
    layout() {
      return this.direction === "horizontal"
        ? {
            "label-align-md": "right",
            "label-cols-md": this.labelCols
          }
        : null;
    },
    text: {
      get() {
        return this.value;
      },
      set(val) {
        this.$emit("input", val);
      }
    }
  },
  mounted() {
    if (typeof this.text == "boolean") {
      if (this.text === true) {
        this.text = "EVET";
      } else if (this.text === false) {
        this.text = "HAYIR";
      } else {
        this.text = "";
      }
    }
  }
};
</script>

