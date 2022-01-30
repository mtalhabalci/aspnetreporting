<template>
  <sdiKit-page
    :showParentHeader="showParentHeader"
    :parentHeader="pageConfig.parentHeader"
    :header="pageConfig.header"
    :description="pageConfig.description"
  >
    <template v-for="(_, slot) in $slots">
      <template :slot="slot">
        <slot :name="slot"></slot>
      </template>
    </template>

    <sdiKit-grid-filter
    v-if="hasFilter"
      id="grid-filter"
      :filter="filter"
      :filter-data="filterTemp"
      :callback="filterAndSdiKitGridRefresh"
    >
      <template>
        <slot name="filter"></slot>
      </template>
    </sdiKit-grid-filter>

    <sdiKit-widget :header="pageConfig.header" :hasDataGrid="true">
      <template #header>
        <b-btn
          v-if="backButtonExists"
          variant="primary btn-sm btn-icon btn-white"
          size="sm"
          @click="sdiKitGridBack"
          class="sdiKit"
        >
          <i class="fas fa-arrow-left ml-1 mr-2"></i> Geri
        </b-btn>
        <b-btn
          variant="primary btn-sm btn-icon btn-white"
          size="sm"
          @click="sdiKitgridRefresh"
          class="sdiKit refresh-button"
          v-b-tooltip.hover
          title="Listeyi Yenile"
        >
          <i class="fas fa-sync-alt ml-1 mr-2"></i> Yenile
        </b-btn>
        <b-btn
        v-if="hasFilter"
          variant="primary btn-sm btn-icon btn-white"
          size="sm"
          v-b-toggle="'grid-filter'"
          class="sdiKit filter-button px-2"
        >
          <i class="fas fa-search ml-1 mr-2"></i> Filtrele
        </b-btn>
        <b-btn
          v-if="showNewButton"
          variant="primary btn-sm btn-icon btn-white"
          size="sm"
          @click="newRecord"
          class="sdiKit new-button px-2"
        >
          <i class="fas fa-plus-square ml-1 mr-2 text-success"></i> Yeni Kayıt
        </b-btn>
        <slot name="extra-buttons"></slot>
      </template>
      <template #body>
        <sdiKit-grid
          :filter="filter"
          ref="sdiKitGrid"
          :columns="gridColumns"
          :component-columns="componentColumns"
          :url="url"
          :edit-page-url="editPageUrl"
          :view-page-url="viewPageUrl"
          :delete-action="deleteAction"
          :row-has-default-action="rowHasDefaultAction"
          :default-action-type="defaultActionType"
        ></sdiKit-grid>
      </template>
    </sdiKit-widget>
  </sdiKit-page>
</template>

<script>
import { Types } from "~/utils/index";
import Grid from "~/mixins/grid";

export default {
  mixins: [Grid],
  props: {
    pageConfig: {
      type: Object
    },
    hasFilter:{
      type:Boolean,
      default:true
    },
    modalCallback: {
      type: Function,
      default: () => console.log("modalCallback")
    },

    newRecord: {
      type: Function
    },

    showNewButton: {
      type: Boolean,
      default: true
    },
    backButtonExists: {
      type: Boolean,
      default: false
    },
    showParentHeader: {
      type: Boolean,
      default: true
    }
  },

  mounted() {
    this.filterTemp = { ...this.filter };
    var self = this;
    Types.getViewModel(this.gridModelName, function(vm) {
      self.gridColumns = Types.createColumns(vm);
    });
  },
  methods: {
    sdiKitGridBack() {
      this.$router.go(-1);
    },
    sdiKitgridRefresh() {
      this.$refs.sdiKitGrid.refresh();
      this.modalCallback();
    },
    filterAndSdiKitGridRefresh() {
      this.$refs.sdiKitGrid.hasFilter = true;
      this.$refs.sdiKitGrid.refresh();
      this.modalCallback();
    }
  }
};
</script>