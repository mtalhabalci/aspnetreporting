<template>
  <b-form-group :label="label" v-bind="layout" class="phoneNumberWrapper">
    <vue-tel-input
      v-model="text"
      :placeholder="placeholder"
      :validCharactersOnly="true"
      default-country="TR"
      @input="input"
      @validate="validate($event)"
      @close="clearValue"
      @open="openCombobox"
      :maxLen="maxLen"
      @blur="blur"
      :disabled="disabled"
      wrapper-classes="phoneClass"
      v-bind:class="{ 'is-invalid': isInvalid }"
    ></vue-tel-input>
    <div class="invalid-tooltip" v-if="state.$error && !state.required">Bu alan gereklidir</div>

    <div class="invalid-tooltip invalid-phone" v-if="!isValid && blurred">Geçerli bir telefon numarası giriniz.</div>
  </b-form-group>
</template>
<script src="https://unpkg.com/vue-tel-input"></script>
<script>
import VueTelInput from "vue-tel-input";
import "vue-tel-input/dist/vue-tel-input.css";
import textbox from "~/mixins/textbox";
import shared from "~/mixins/components";

export default {
  props: {
    placeholder: {
      default: "Numara Giriniz"
    },
    checkValidation: {
      type: Function,
      default: () => {}
    }
  },

  components: {
    VueTelInput
  },
  data: () => ({
    maxLen: 17,
    isValid: true,
    blurred: false
  }),
  computed: {
    isValidPhone: {
      get() {
        return this.isValid;
      },
      set(val) {
        this.$emit("validate", val);
      }
    }
  },
  methods: {
    blur() {
      this.blurred = true;
    },
    input(number, isValid, country) {
      this.isValid = isValid.isValid;
      this.checkValidation(this.isValid);
    },
    validate(e) {
      if (e.isValid) {
        let maxLen = e.number
          .replace(e.country.dialCode, "")
          .replace("+", "")
          .replace(/ /g, "").length;
        this.maxLen = maxLen;
      } else {
        this.maxLen = 17;
      }
    },
    clearValue() {
      console.log("close");
    },
    openCombobox() {
      console.log("openCombobox");
      this.text = "";
    }
  },
  mixins: [shared, textbox]
};
</script>

<style>
.phoneClass {
  border: 1px solid rgba(24, 28, 33, 0.1) !important;
  border-radius: 0.25rem;
  height: calc(1.54em + 0.876rem + 2px);
}

.vue-tel-input.phoneClass:focus,
.vue-tel-input.phoneClass:focus-within {
  box-shadow: none !important;
  outline: 0 !important;
  border-color: #fc5a5c !important;
}
.dropdown-arrow {
  padding-left: 5px;
}
</style>