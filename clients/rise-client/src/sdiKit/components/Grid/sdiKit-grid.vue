<template>
  <div>
    <div class="table-responsive">
      <!-- head-variant="light" -->
      <b-table
        ref="table"
        class="sdiKit-grid table-vcenter"
        :items="getItems"
        @sort-changed="sortingChanged"
        :busy.sync="isBusy"
        :stacked="false"
        :striped="true"
        :hover="true"
        :bordered="true"
        :borderless="false"
        :outlined="false"
        :small="false"
        :tbody-tr-class="rowClass"
        :fixed="false"
        :dark="false"
        :foot-clone="false"
        :current-page="currentPage"
        :fields="getGridColumns"
        primary-key="id"
        :per-page="perPage"
        show-empty
      >
        <div slot="table-busy" class="text-center m-1 pt-3 pb-0">
          <b-spinner class="align-middle"></b-spinner>
          <p class="pt-2">
            <strong>İşlem yapılıyor...</strong>
          </p>
        </div>

        <div slot="empty" class="text-center m-1 pt-3 pb-0">
          <p class="pt-2">
            <strong>Görüntülenecek öğe yok</strong>
          </p>
        </div>

        <!-- <template v-slot:[cell(${field.key})]="data">
          <i>{{ data.value }}</i> AAA
        </template>-->
        <template
          v-for="(field, index) in getGridColumns"
          :slot="`cell(${field.key})`"
          slot-scope="data"
        >
          <div :key="field.key">
            <div v-if="!field.componentName">
              <sdiKit-grid-column-string :item="data.item" :field="field"></sdiKit-grid-column-string>
              <sdiKit-grid-column-boolean :item="data.item" :field="field"></sdiKit-grid-column-boolean>
              <sdiKit-grid-column-number :item="data.item" :field="field"></sdiKit-grid-column-number>
              <sdiKit-grid-column-datetime :item="data.item" :field="field"></sdiKit-grid-column-datetime>
            </div>
            <div v-else>
              <component
                :is="field.componentName"
                v-model="data.item"
                v-bind="field.componentProps"
              ></component>
            </div>
          </div>
        </template>
      </b-table>
    </div>
    <div class="row mx-1">
      <div class="col-sm text-sm-left text-center mb-1 mb-sm-0">
        <span
          class="text-muted small align-middle"
          v-if="totalPages"
        >Sayfa {{ currentPage }} / {{ totalPages }}</span>
      </div>
      <div class="col-sm">
        <b-pagination
          class="justify-content-center justify-content-sm-end float-right"
          v-if="totalRowCount"
          v-model="currentPage"
          :total-rows="totalRowCount"
          :per-page="perPage"
          size="sm"
        />

        <b-select
          v-if="hasPerPage"
          size="sm"
          :options="perPageOptions"
          v-model="perPage"
          class="mr-3 mb-2 d-inline-block w-auto float-right"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { Ajax, ObjectToQueryString, Types } from "~/utils/index";
import { GridActionType } from "~/enums";

import ApplicationConstant from "@/sdiKit/constant";
import Grid from "~/mixins/grid";
import _ from "lodash";

export default {
  mixins: [Grid],
  props: {
    columns: {
      type: Array,
      required: true,
      default: function() {
        return [];
      }
    },
    hasPerPage: {
      type: Boolean,
      required: false,
      default: true
    }
  },
  data: () => ({
    hasFilter: false,
    isFilterButtonClicked: false,
    currentPage: 1,
    isBusy: false,
    perPage: 10,
    perPageOptions: [1, 10, 20, 30, 40, 50],
    totalRowCount: 0,
    totalPages: 1,
    options: {
      pageNumber: 1,
      sortColumn: "",
      sortDirection: 0
    },
    items: []
  }),
  computed: {
    totalItems() {
      return this.items.length;
    },
    getGridColumns() {
      let customComponentColumns = this.getComponentColumns();
      let calculatedColumns = this.columns.filter(function(c) {
        return c.isHidden == false;
      });

      if (this.columns.length > 0 && this.rowHasDefaultAction) {
        let addActionColumn = (arr, item) =>
          arr.find(x => x.key === item.key) || arr.push(item);
        if (this.defaultActionType == GridActionType.Inline) {
          addActionColumn(customComponentColumns, {
            key: "defaultActionColumn",
            label: "İşlemler",
            tdClass: "actionColumn actionInlineColumn",
            componentName: "sdiKit-grid-column-default-action",
            componentProps: {
              editPageUrl: this.editPageUrl,
              viewPageUrl: this.viewPageUrl,
              deleteAction: this.deleteAction
            }
          });
        } else {
          addActionColumn(customComponentColumns, {
            key: "defaultActionColumn",
            label: "",
            tdClass: "actionColumn actionContextColumn",
            componentName: "sdiKit-grid-column-default-action-context-menu",
            componentProps: {
              editPageUrl: this.editPageUrl,
              viewPageUrl: this.viewPageUrl,
              deleteAction: this.deleteAction
            }
          });
        }
      }
      let columns = [...calculatedColumns, ...customComponentColumns];
      let orderedColumns = _.sortBy(columns, o => o.order);
      return orderedColumns;
    }
  },
  methods: {
    refresh() {
      this.$refs.table.refresh();
    },
    getComponentColumns() {
      return [...this.componentColumns];
    },
    load(data) {
      console.log(data);
    },
    rowClass(item, type) {
      if (!item) return;
      if (item._loadedFirst === true) return "hidden" + item._rowIndex;
    },
    sortingChanged(ctx) {
      if (!ctx.sortBy) {
        return;
      }

      let { 0: column } = this.columns.filter(c => {
        return c.key == ctx.sortBy;
      });

      this.options.sortColumn = column.sortableAlias || ctx.sortBy;
      this.options.sortDirection = ctx.sortDesc ? 1 : 0;
    },
    getItems(ctx) {
      let items = this.ajax();
      let index = 0;

      return items;
    },
    ajax() {
      let self = this;
      this.options.pageNumber = this.currentPage;
      if (this.hasFilter == true) {
        this.options.pageNumber = 1;
        this.currentPage = 1;
      }
      let params = {
        pageNumber: this.options.pageNumber,
        rowCount: this.$refs.table.perPage,
        sortColumn: this.options.sortColumn,
        sortDirection: this.options.sortDirection
      };
      var postData = { ...params, ...this.filter };
      let requestUrl = this.url;

      let promise = Ajax.postPromise(requestUrl, postData);
      return promise
        .then(data => {
          let index = 1;
          let items = data.data.items.map(function(i) {
            i._loadedFirst = true;
            i._rowIndex = index++;
            return i;
          });
          this.totalRowCount = data.data.totalCount;
          this.totalPages = data.data.totalPages;
          this.$refs.table.refresh();
          this.$emit("getGridData", data.data.items);
          this.hasFilter = false;
          return items;
        })
        .catch(error => {
          return [];
        });
    }
  },
  watch: {
    currentPage(newPage, oldPage) {
      ApplicationConstant.console.developmentLog("currentPage changed");
    }
  }
};
</script>

<style lang="scss">
.sdiKit-grid {
  .item-status {
    background-color: #fff;
    padding: 1px;
    border-radius: 20px;
    padding: 3px 6px;
    font-size: 11px;
    .status-icon {
      display: inline-block;
      border-radius: 50%;
      width: 0.5rem;
      height: 0.5rem;
      background: currentColor;
      margin-right: 5px;
    }
  }

  .grid-item-isActive {
    width: 85px;
  }

  .flip-list-move {
    transition: all 0.3s;
  }

  .actionColumn {
    width: 40px;
    text-align: center;
  }
  .actionColumn.actionInlineColumn {
    width: 110px;
  }

  .custom-checkbox .custom-control-label::before {
    background-size: 50%;
  }

  .custom-control-input:disabled ~ .custom-control-label::before {
    border-color: var(--light) !important;
    background-color: var(--light) !important;
  }

  .custom-control-label::before {
    width: 1.5em;
    height: 1.5em;
  }

  .custom-control.custom-checkbox
    .custom-control-input:disabled:checked
    ~ .custom-control-label::before {
    border-color: var(--success) !important;
    background-color: var(--success) !important;
    background-image: url("data:image/svg+xml;charset=utf8,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 9.5 7.5'%3E%3Cpolyline points='0.75 4.35 4.18 6.75 8.75 0.75' style='fill:none;stroke:%23fff;stroke-linecap:round;stroke-linejoin:round;stroke-width:1.5px'/%3E%3C/svg%3E");
  }
}
</style>
