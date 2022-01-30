<template>
  <span v-if="field.type === 'string'">{{ item[field.key] }}</span>
</template>

<script>
import gridColumns from "~/mixins/grid-columns";
export default {
  mixins: [gridColumns]
};
</script>