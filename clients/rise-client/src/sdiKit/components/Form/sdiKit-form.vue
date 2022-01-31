<template>
  <sdiKit-page
    :show-parent-header="true"
    :header="header"
    :parent-header="parentHeader"
    :description="headerDescription"
  >
    <template v-for="(_, slot) in $slots">
      <template :slot="slot">
        <slot :name="slot"></slot>
      </template>
    </template>
    <div class="row">
      <div
        v-if="withCard"
        v-bind:class="{ 'col-10': layoutOptions == 'half', 'col': layoutOptions == 'full' }"
      >
        <b-card :header="header" header-tag="h3" footer-tag="footer">
          <slot name="body"></slot>
          <div class="row" slot="footer">
            <div class="col">
              <div class="d-inline-block">
                <b-button
                  variant="outline-danger"
                  v-if="showDeleteButton"
                  :label-cols-md="2"
                  v-b-modal="'delete-modal'"
                >Sil</b-button>
              </div>
            </div>
            <div class="col text-right">
              <b-btn class="mr-1" type="submit" variant="light" @click="cancellation">İptal</b-btn>​
              <vue-ladda
                :loading="loading"
                @click.native="submitForm"
                class="btn btn-success"
                data-style="expand-right"
              >Kaydet</vue-ladda>
            </div>
          </div>
        </b-card>
      </div>
      <div v-if="!withCard" class="col">
        <slot name="body"></slot>​
        <b-card no-body class="mt-2">
          <b-card-body class="p-2">
            <div class="row">
              <div class="col">
                <b-button
                  variant="outline-danger"
                  v-if="showDeleteButton"
                  :label-cols-md="2"
                  @click="deleteModalShow = true"
                >Sil</b-button>
              </div>
              <div class="col text-right">
                <b-btn class="mr-1" type="submit" variant="light" @click="cancellation">İptal</b-btn>
                <vue-ladda
                  :loading="loading"
                  @click.native="submitForm"
                  class="btn btn-success"
                  data-style="expand-right"
                >Kaydet</vue-ladda>
              </div>
            </div>
          </b-card-body>
        </b-card>
      </div>
    </div>​
    <b-modal
      v-model="deleteModalShow"
      :no-close-on-esc="true"
      :no-close-on-backdrop="true"
      id="delete-modal"
      ref="delete-modal"
      class="modal-center delete-modal"
      @ok="deleteObject"
      ok-title="Sil"
      ok-variant="danger"
      cancel-title="İptal"
      cancel-variant="light"
      centered
      header-bg-variant="danger"
      header-class="text-white"
    >
      <div slot="modal-title">
        <div v-if="true">
          <h4 class="text-bold mb-0">Uyarı!</h4>
        </div>
      </div>
      <p>
        Öğeyi silmek istediğinizden emin misiniz?
        <br />
        <code>Bu işlem geri alınamaz!</code>
      </p>
    </b-modal>
  </sdiKit-page>
</template>
<style scoped lang="scss">
.modal-header {
  color: #fff !important;
}
</style>
<script>
import { Ajax, Notify } from "~/utils/index";
import Pages from "~/mixins/pages";
export default {
  mixins: [Pages],
  props: {
    withCard: {
      type: Boolean,
      default: true
    },
    layoutOptions: {
      type: String,
      validator: val => ["full", "half"].includes(val),
      default: "half"
    },
    showDeleteButton: {
      type: Boolean,
      default: false
    },
    sdiKitFormModel: {
      type: Object,
      default: undefined
    },
    url: {
      type: String,
      default: "/values"
    },
    afterSubmit: {
      type: Function,
      default: () => {
        return true;
      }
    },
    beforeSubmit: {
      type: Function,
      default: () => {
        return true;
      }
    },
    submit: {
      type: Function,
      default: undefined
    },
    canSubmit: {
      type: Boolean,
      default: false
    },
    modelId: {
      type: String,
      default: undefined
    },
    beforeDelete: {
      type: Function,
      default: () => {
        return true;
      }
    },
    deleteItem: {
      type: Function,
      default: () => {
        return true;
      }
    },
    parentHeader: {
      type: String,
      default: ""
    },
    header: {
      type: String,
      default: ""
    },
    headerDescription: {
      type: String,
      default: ""
    },
    formData: {
      type: FormData
    },
    cancel: {
      type: Function,
      default: () => {
        window.history.back();
      }
    }
  },
  data: () => ({
    loading: false,
    deleteModalShow: false
  }),
  beforeDestroy() {
    this.$refs["delete-modal"].onAfterLeave();
  },
  methods: {
    submitForm() {
      if (this.submit) {
        return this.submit();
      } else {
        if (this.beforeSubmit()) {
          this.loading = true;
          var data = JSON.stringify(this.sdiKitFormModel);
          if (this.formData) {
            Ajax.postMultiPart(
              this.url,
              this.formData,
              this.requestCallBack,
              this.requestErrorCallBack
            );
          } else {
            Ajax.post(
              this.url,
              data,
              this.requestCallBack,
              this.requestErrorCallBack
            );
          }
          return true;
        }
        return false;
      }
    },
    cancellation() {
      this.cancel();
    },
    deleteObject() {
      if (this.beforeDelete()) {
        this.deleteItem();
        this.deleteModalShow = false;
      } else {
        this.deleteModalShow = false;
      }
    },
    requestCallBack(response) {
      let data = response.data;
      this.loading = false;
      this.afterSubmit(data);
    },
    requestErrorCallBack(response) {
      this.loading = false;
    }
  }
};
</script>