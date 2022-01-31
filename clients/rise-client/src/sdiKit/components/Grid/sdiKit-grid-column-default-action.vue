<template>
  <div>
    <b-button
      variant="primary btn-sm btn-icon btn-white"
      v-if="this.viewPageUrl"
      @click="viewRow"
      v-b-tooltip.hover.right
      title="Satırı Görüntüle"
    >
      <i class="far fa-eye"></i>
    </b-button>
    <b-button
      variant="primary btn-sm btn-icon btn-white"
      v-if="this.editPageUrl"
      @click="editRow"
      v-b-tooltip.hover.right
      title="Satırı Düzenle"
    >
      <i class="fas fa-pen"></i>
    </b-button>

    <b-button
      variant="primary btn-sm btn-icon btn-white"
      v-if="this.deleteAction"
      @click="deleteRow"
      v-b-tooltip.hover.right
      title="Satırı Sil"
    >
      <i class="far fa-trash-alt"></i>
    </b-button>
  </div>
</template>

<script>
import SdiKitfGridColumnDefaultAction from "./sdiKit-grid-column-default-action-mixin";

export default {
  mixins: [SdiKitfGridColumnDefaultAction],
  created() {}
};
</script>