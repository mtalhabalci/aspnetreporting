<template>
  <div>
    <b-dropdown size="sm" variant="link" toggle-class="text-decoration-none" no-caret>
      <template slot="button-content">
        <i class="fas fa-ellipsis-v text-secondary"></i>
      </template>
      <b-dropdown-header class="p-2">İşlemler</b-dropdown-header>
      <b-dropdown-item v-if="this.editPageUrl" @click="editRow" class="small p-2">
        <i class="fas fa-pen mr-1"></i> Düzenle
      </b-dropdown-item>
      <b-dropdown-item v-if="this.viewPageUrl" @click="viewRow" class="small p-2">
        <i class="far fa-eye mr-1"></i> Görüntüle
      </b-dropdown-item>
      <b-dropdown-divider v-if="this.deleteAction" class="m-0"></b-dropdown-divider>

      <b-dropdown-item v-if="this.deleteAction" @click="deleteRow" class="small p-2">
        <i class="far fa-trash-alt mr-1"></i> Sil
      </b-dropdown-item>
    </b-dropdown>
  </div>
</template>

<script>
import SdiKitGridColumnDefaultAction from "./sdiKit-grid-column-default-action-mixin";

export default {
  mixins: [SdiKitGridColumnDefaultAction]
};
</script>