<template>
  <div class="container-fluid">
    <sdiKit-page-grid
      :page-config="pageConfig"
      :filter="filter"
      :showNewButton="false"
      :has-filter="false"
      :grid-model-name="$enums.OutputEnums.ReportOutput"
      url="/report/get-all"
      :component-columns="componentColumns"
      :row-has-default-action="false"
    >
      <span
        slot="parent-header"
        class="pageconfig-parentheader text-muted font-weight-light"
        >{{ pageConfig.parentHeader }}</span
      >

      <span slot="header" class="pageconfig-header">{{
        pageConfig.header
      }}</span>
      <b-form slot="filter">
        <sdiKit-textbox
          :label="'asdfasdfas'"
          v-model="filter.name"
        ></sdiKit-textbox>
        <sdiKit-checkbox
          :label="'asdfasdfas'"
          v-model="filter.isActive"
        ></sdiKit-checkbox>
      </b-form>
    </sdiKit-page-grid>
  </div>
</template>
<script>
import ReportGridColumnItemExtraActions from "./Components/report-grid-column-item-extra-actions";

import Vue from "vue";
Vue.component(
  "report-grid-column-item-extra-actions",
  ReportGridColumnItemExtraActions
);

import PageConfig from "./pageConfig";

export default {
  mixins: [PageConfig],
  methods: {
    downloadReport() {
      //indirme işlemi yapılacak
    },
  },
  data() {
    return {
      filter: {
        name: "",
        isActive: true,
      },
      componentColumns: [
        {
          order: 0,
          key: "itemExtraActions",
          label: "İşlemler",
          tdClass: "itemExtraActions",
          componentName: "report-grid-column-item-extra-actions",
          componentProps: {
            download: this.downloadReport,
          },
        },
      ],
    };
  },
};
</script>

<style scoped>
/deep/ table.sdiKit-grid td:nth-child(1) {
  width: 150px;
}
/deep/ table.sdiKit-grid td:nth-child(2) {
  width: 150px;
}
/deep/ table.sdiKit-grid td:nth-child(4) {
  width: 450px;
}
</style>