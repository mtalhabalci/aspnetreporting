<template>
  <b-form-group>
    <label class="switcher">
      <input type="checkbox" class="switcher-input" v-model="value" @change="change" />
      <span class="switcher-indicator">
        <span class="switcher-yes"></span>
        <span class="switcher-no"></span>
      </span>
      <span class="switcher-label">{{label}}</span>
    </label>
  </b-form-group>
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      default: "Etiket Yok"
    }
  },
  data: () => ({
    value: false
  }),
  methods: {
    change() {
        this.value = !this.value;
        this.$emit("input", this.value);
    }
  }
};
</script>
