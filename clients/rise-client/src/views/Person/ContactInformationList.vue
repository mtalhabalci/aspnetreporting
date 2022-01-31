<template>
  <div class="container-fluid">
    <sdiKit-page-grid
      :has-filter="false"
      :page-config="pageConfig"
      :filter="filter"
      ref="sdiKitGridCategoryInformations"
      :showNewButton="false"
      :grid-model-name="$enums.OutputEnums.ContactInformationOutput"
      url="/contactInformation/get-all"
      :delete-action="deleteContactInformation"
      :component-columns="componentColumns"
      :row-has-default-action="true"
    >
      <span
        slot="parent-header"
        class="pageconfig-parentheader text-muted font-weight-light"
        >Kişi</span
      >

      <span slot="header" class="pageconfig-header">İletişim Bilgileri</span>
    </sdiKit-page-grid>
    <b-modal
      :no-close-on-esc="true"
      :no-close-on-backdrop="true"
      ref="confirm-delete-modal"
      class="modal-center delete-modal"
      @ok="completeDeleteOperation"
      :ok-title="'Tamam'"
      ok-variant="danger"
      :cancel-title="'İptal'"
      cancel-variant="light"
      centered
      header-bg-variant="danger"
      header-class="text-white"
    >
      <div slot="modal-title">
        <div v-if="true">
          <h4 class="text-bold mb-0">Silme İşlemi</h4>
        </div>
      </div>
      <p>
        İletişim Bilgisini Sil <br />
        <code>Bu işlem geri alınamaz</code>
      </p>
    </b-modal>
  </div>
</template>
<script>
import PageConfig from "./pageConfig";
import { Ajax } from "~/utils/index";

import ContactInformationGridColumnItemExtraActions from "./Components/contact-information-grid-column-item-extra-actions.vue";

import Vue from "vue";
Vue.component(
  "contact-information-grid-column-item-extra-actions",
  ContactInformationGridColumnItemExtraActions
);

export default {
  mixins: [PageConfig],
  methods: {
    deleteContactInformation(item) {
      this.deleteItem = item;
      this.$refs["confirm-delete-modal"].show();
    },
    sdiKitGridRefresh() {
      debugger;
      this.$refs.sdiKitGridCategoryInformations.$refs.sdiKitGrid.refresh();
    },
    completeDeleteOperation() {
      debugger;
      this.$refs["confirm-delete-modal"].hide();
      Ajax.post("ContactInformation/delete", this.deleteItem.id, () => {
        this.sdiKitGridRefresh();
        this.deleteItem = null;
      });
    },
  },
  data() {
    return {
      deleteItem: {},
      filter: {
        name: "",
        isActive: true,
        personId: this.$route.params.id,
      },
      componentColumns: [
        {
          order: 0,
          key: "itemExtraActions",
          label: "",
          tdClass: "itemExtraActions",
          componentName: "contact-information-grid-column-item-extra-actions",
          componentProps: {},
        },
      ],
    };
  },
};
</script>

<style scoped>
/deep/ table.sdiKit-grid td:nth-child(1) {
  width: 76px;
}
</style>