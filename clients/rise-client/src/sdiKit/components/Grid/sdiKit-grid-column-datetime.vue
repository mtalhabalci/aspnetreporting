<template>
  <span v-if="field.type === 'datetime' && item[field.key]">{{item[field.key] | date-format}}</span>
</template>

<script>
import gridColumns from "~/mixins/grid-columns";
export default {
  mixins: [gridColumns]
};
</script>