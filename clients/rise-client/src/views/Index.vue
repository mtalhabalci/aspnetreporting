<template>
  <div class="container">
    <sdiKit-page header="Dashboard" :parent-header="pageConfig.parentHeader">
      <div class="container mt-2">
        <div class="row row-deck row-cards">
          <div class="col-md-4 col-sm-4 col-lg-4">
            <div class="card">
              <div class="card-body p-3 text-center">
                <div class="d-flex justify-content-center">
                  <span class="stamp stamp-md mr-3">
                    <i class="fe fe-tv"></i>
                  </span>
                  <div
                    class="text-muted mt-1 ml-1 mr-2 d-flex align-items-center"
                  >
                    Kullanıcı
                  </div>
                  <div class="h1 m-0 d-flex align-items-center">
                    <ICountUp :endVal="345" :options="optionsCountup" />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 col-sm-4 col-lg-4">
            <div class="card">
              <div class="card-body p-3 text-center">
                <div class="d-flex justify-content-center">
                  <span class="stamp stamp-md mr-3">
                    <i class="fe fe-calendar"></i>
                  </span>
                  <div
                    class="text-muted mt-1 ml-1 mr-2 d-flex align-items-center"
                  >
                    Kullanıcı
                  </div>
                  <div class="h1 m-0 d-flex align-items-center">
                    <ICountUp :endVal="345" :options="optionsCountup" />
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-4 col-sm-4 col-lg-4">
            <div class="card row-card row-deck">
              <div class="card-body p-3 text-center">
                <div class="d-flex justify-content-center">
                  <span class="stamp stamp-md mr-3">
                    <i class="fe fe-user"></i>
                  </span>
                  <div
                    class="text-muted mt-1 ml-1 mr-2 d-flex align-items-center"
                  >
                    Kullanıcı
                  </div>
                  <div class="h1 m-0 d-flex align-items-center">
                    <ICountUp :endVal="345" :options="optionsCountup" />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row mb-3" style="--bs-gutter-x:0;">
          <button @click="requestReport" class="btn-success col-12 px-1">Rapor Taleb Et</button>
        </div>
        <sdiKit-widget
          :header="pageConfig.header"
          :topRightButtonExists="true"
          :topRightButtonText="'Ekle'"
          :rightButtonOnClick="addNewPerson"
          class="no-padding has-dataGrid"
        >
          <div slot="body">
            <sdiKit-grid
              :filter="filter"
              :edit-page-url="'/person/edit/'"
              ref="sdiKitGridPersons"
              :columns="gridColumns"
              :component-columns="componentColumns"
              url="/person/get-all"
              :delete-action="deletePerson"
              :row-has-default-action="true"
            ></sdiKit-grid>
          </div>
        </sdiKit-widget>
      </div>
    </sdiKit-page>
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
        Kişiyi Sil <br />
        <code>Bu işlem geri alınamaz</code>
      </p>
    </b-modal>
  </div>
</template>
<script>
import PageConfig from "./pageConfig";
import ICountUp from "vue-countup-v2";
import { Types, Ajax, Storage } from "~/utils/index";

import PersonGridColumnItemExtraActions from "./Person/Components/person-grid-column-item-extra-actions.vue";

import Vue from "vue";
Vue.component(
  "person-grid-column-item-extra-actions",
  PersonGridColumnItemExtraActions
);

export default {
  mixins: [PageConfig],
  components: {
    ICountUp,
  },
  data() {
    return {
      filter: {},
      gridColumns: [],
      componentColumns: [
        {
          order: 0,
          key: "itemExtraActions",
          label: "",
          tdClass: "itemExtraActions",
          componentName: "person-grid-column-item-extra-actions",
          componentProps: {
            openContactInformationModal: this.openContactInformationModal,
          },
        },
      ],
      optionsCountup: {
        useEasing: true,
        useGrouping: true,
        separator: ".",
        decimal: ",",
        prefix: "",
        suffix: "",
      },
    };
  },
  methods: {
    requestReport(){
      Ajax.get("/report/request-report/", function (response) {
     debugger
    });
    },
    completeAddOperation(data) {
      debugger;
    },
    sdiKitGridRefresh() {
      this.$refs.sdiKitGridPersons.refresh();
    },
    deletePerson(item) {
      this.deleteItem = item;
      this.$refs["confirm-delete-modal"].show();
    },
    openContactInformationModal(item) {
      this.$refs["contact-information-modal"].show();
    },
    addNewPerson() {
      this.$router.push({ path: "/person/create" });
    },
    completeDeleteOperation() {
      this.$refs["confirm-delete-modal"].hide();
      Ajax.post("Person/delete", this.deleteItem.id, () => {
        this.sdiKitGridRefresh();
        this.deleteItem = null;
      });
    },
  },
  mounted() {
    var self = this;
    debugger;
    var assemblyName = "Rise.Application.Contracts";
    Ajax.get("/enums/load/" + assemblyName, function (response) {
      let result = response.result;
      if (!Storage.hasKey(Storage.keys.Enums)) {
        Storage.set(Storage.keys.Enums, result);
      }
    });

    Types.getViewModel(self.$enums.OutputEnums.PersonOutput, function (vm) {
      self.gridColumns = Types.createColumns(vm);
    });
  },
};
</script>
