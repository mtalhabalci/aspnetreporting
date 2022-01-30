<template>
  <div v-if="field.type === 'boolean'" style="text-align: center;">
    <span class="item-status" v-if="item[field.key] == false">
      <span class="status-icon bg-dark"></span> Hayır
    </span>

    <span class="item-status" v-if="item[field.key] == true">
      <span class="status-icon bg-success"></span> Evet
    </span>

    <!-- <b-form-checkbox v-model="item[field.key]" disabled></b-form-checkbox> -->
  </div>
</template>

<script>
import gridColumns from "~/mixins/grid-columns";
export default {
  mixins: [gridColumns]
};
</script>