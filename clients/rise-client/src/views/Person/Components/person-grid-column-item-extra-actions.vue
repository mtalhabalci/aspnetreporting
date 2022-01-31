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
        <sdiKit-enum-combobox
          ref="comboboxContactType"
          :label="'İletişim Bilgisi Türü Seçiniz'"
          :class="contactInformation.contactType > 0 ? 'col-6' : 'col-12'"
          v-model="contactInformation.contactType"
          enum="Rise.Application.Contracts.Types.Enums.ContactTypeEnum"
        ></sdiKit-enum-combobox>
        <sdiKit-textbox
          v-if="contactInformation.contactType == contactTypeEnum.Location"
          :label="'Değeri'"
          placeholder
          v-model="contactInformation.value"
          class="col-6"
        ></sdiKit-textbox>
        <sdiKit-textbox-email
          :label="'Değeri'"
          v-if="contactInformation.contactType == contactTypeEnum.Email"
          placeholder
          v-model="contactInformation.value"
          class="col-6"
        ></sdiKit-textbox-email>
        <sdiKit-phone
          :label="'Değeri'"
          placeholder
          v-if="contactInformation.contactType == contactTypeEnum.Phone"
          v-model="contactInformation.value"
          class="col-6"
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
      this.$router.push({ path: "/person/contactinformation/" + this.value.id });
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
    }
   
  },
};
</script>
