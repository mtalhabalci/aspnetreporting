<template>
  <div>
    <b-button
      variant="default btn-sm icon-btn md-btn-flat bg-white"
      @click="openModal"
      v-b-tooltip.hover.right
      :title="'Kişiye İletişim Bilgisi Ekle'"
    >
      <i class="fas fa-plus-circle"></i>
    </b-button>
    <b-button
      variant="default btn-sm icon-btn md-btn-flat bg-white"
      @click="listContactInformations"
      v-b-tooltip.hover.right
      :title="'Kişinin İletişim Bilgileri'"
    >
      <i class="fas fa-info"></i>
    </b-button>
    <b-modal
      :no-close-on-esc="false"
      :no-close-on-backdrop="false"
      ref="contact-information-modal"
      class="modal-center"
      @ok="completeAddOperation"
      :ok-title="'Ekle'"
      ok-variant="success"
      :cancel-title="'İptal'"
      size="xl"
      cancel-variant="danger"
      centered
      header-bg-variant="primary"
      header-class="text-white"
    >
      <div slot="modal-title">
        <div v-if="true">
          <h4 class="text-bold mb-0">İletişim Bilgisi Ekleme</h4>
        </div>
      </div>
      <div class="row">
        <sdiKit-textbox
          :label="'Konum'"
          placeholder
          v-model="contactInformation.location"
          class="col-4"
        ></sdiKit-textbox>
        <sdiKit-textbox-email
          :label="'E-posta'"
          placeholder
          v-model="contactInformation.email"
          class="col-4"
        ></sdiKit-textbox-email>
        <sdiKit-phone
          :label="'Telefon'"
          placeholder
          v-model="contactInformation.phone"
          class="col-4"
        ></sdiKit-phone>
      </div>
    </b-modal>
  </div>
</template>

<script>
import { Ajax } from "~/utils/index";
import { ContactType } from "@/enums/index";
export default {
  data() {
    return {
      contactTypeEnum: ContactType,
      contactInformation: {
        contactType: null,
        value: "",
        personId: this.value.id,
      },
    };
  },

  props: {
    value: {
      type: Object,
      required: true,
    },

    openContactInformationModal: {
      type: Function,
      default: () => {},
    },
    add: {
      type: Function,
      default: () => {},
    },
  },
  watch: {
    "contactInformation.contactType": {
      handler() {
        this.contactInformation.value = "";
      },
    },
  },
  methods: {
    listContactInformations() {
      this.$router.push({
        path: "/person/contactinformation/" + this.value.id,
      });
    },
    checkValidation() {
      return true; //validasyonlar yapılacak
    },
    completeAddOperation() {
      var validationResult = this.checkValidation();
      if (validationResult) {
        Ajax.post(
          "/ContactInformation/save",
          this.contactInformation,
          (response) => {
            debugger;
          }
        );
      }
    },
    openModal() {
      this.$refs["contact-information-modal"].show();
    },
  },
};
</script>
