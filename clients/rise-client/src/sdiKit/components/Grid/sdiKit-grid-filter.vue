<template>
  <!-- <b-modal ref="modal" :id="id" size="lg" class="modal-top filter-modal right">
    <div slot="modal-title">
      {{ header }}
      <div v-if="subHeader">
        <small class="text-muted">{{ subHeader }}</small>
      </div>
    </div>
    <slot></slot>
    <template v-slot:modal-footer>
      <div class="w-100">
        <div class="row">
          <div class="col">
            <b-button variant="outline-secondary" size="m" @click="hideModal">İptal</b-button>

            <b-button variant="primary" size="m" @click="handleOk" class="float-right">Filtrele</b-button>
            <b-button
              variant="outline-secondary"
              size="m"
              @click="handleClear"
              class="float-right mr-1"
            >Temizle</b-button>
          </div>
        </div>
      </div>
    </template>
  </b-modal>-->

  <b-sidebar v-model="modalState" ref="modal" :id="id" :title="header" backdrop shadow right>
    <template v-slot:footer="{ hide }">
      <div class="d-flex bg-dark text-light align-items-center px-3 py-2">
        <!-- <b-button variant="secondary" size="m" @click="hide">İptal</b-button> -->
        <b-button variant="secondary" size="m" @click="handleClear" class="mr-auto">Temizle</b-button>
        <b-button variant="primary" size="m" @click="handleOk">Filtrele</b-button>
      </div>
    </template>
    <div class="px-3 py-2">
      <div v-if="subHeader" class="mb-2">
        <small class="text-muted">{{ subHeader }}</small>
      </div>

      <div>
        <slot></slot>
      </div>
    </div>
  </b-sidebar>
</template>

<script>
export default {
  data() {
    return {
      modalState: false
    };
  },
  props: {
    filter: {
      type: Object
    },
    id: {
      type: String,
      required: true
    },
    header: {
      type: String,
      default: "Filtre"
    },
    subHeader: {
      type: String
    },
    callback: {
      type: Function,
      default: data => console.log("Modal callback.")
    },
    filterData: {
      type: Object,
      default: {}
    },
    clearCallback: {
      type: Function,
      default: () => {}
    }
  },
  methods: {
    hideModal(bvModalEvt) {
      this.modalState = false;
    },
    handleOk(bvModalEvt) {
      this.callback();
      this.modalState = false;
    },
    handleClear(bvModalEvt) {
      for (var prop in this.filterData) {
        if (Array.isArray(this.filter[prop])) {
          while (this.filter[prop].length > 0) {
            this.filter[prop].pop();
          }
        } else {
          this.filter[prop] = this.filterData[prop];
        }
      }
    }
  }
};
</script>

<style scoped>
/deep/.modal-dialog {
  margin-right: 0;
  height: 100%;
}
/deep/.filter-modal .modal {
  overflow-x: hidden !important;
  overflow-y: hidden !important;
}
/deep/.modal-content {
  height: 100%;
}
</style>
<style >
.filter-modal .fade .modal-dialog {
  right: -480px;
  -webkit-transition: opacity 0.3s linear, right 0.3s ease-out !important;
  -moz-transition: opacity 0.3s linear, right 0.3s ease-out !important;
  -o-transition: opacity 0.3s linear, right 0.3s ease-out !important;
  transition: opacity 0.3s linear, right 0.3s ease-out !important;
}

.filter-modal .fade.show .modal-dialog {
  right: 0;
}
.filter-modal .modal-dialog {
  box-shadow: 0 0.5rem 1rem rgba(24, 28, 33, 0.2) !important;
  position: fixed;
  margin: auto;
  width: 480px;
  height: 100%;
  -webkit-transform: translate3d(0%, 0, 0);
  -ms-transform: translate3d(0%, 0, 0);
  -o-transform: translate3d(0%, 0, 0);
  transform: translate3d(0%, 0, 0);
}

.filter-modal .modal-content {
  height: 100%;
  overflow-y: auto;
  border-radius: 0;
}

.filter-modal .modal-body {
  padding: 15px 15px 80px;
}
</style>