<template>
  <span
    v-if="isInteger(field.type) || isDecimal(field.type) || field.type === 'single'"
  >{{ item[field.key] }}</span>
</template>

<script>
import gridColumns from "~/mixins/grid-columns";
export default {
  mixins: [gridColumns]
};
</script>