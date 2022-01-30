<template>
  <div>
    <b-form-group :label="label" v-bind="layout" :class="{ 'input-required': attrIsRequired }">
      <masked-input
        type="text"
        class="form-control"
        :placeholder="placeholder"
        :mask="emailMask"
        v-model="text"
        v-bind:class="{ 'is-invalid': isInvalid }"
        @change="onChange"
        autocomplete="false"
        :maxlength="maxLength"
      />
      <div class="invalid-tooltip" v-if="state.$error && !state.required">Bu alan gereklidir</div>
      <div
        class="invalid-tooltip"
        v-if="state.$error && !state.email && state.required"
      >Geçerli bir e-posta adresi giriniz</div>

      <div
        class="invalid-tooltip"
        v-if="state.$error && state.minLength == false"
      >Bu alana en az {{state.$params.minLength.min}} karakter girebilirsiniz.</div>

      <div
        class="invalid-tooltip"
        v-if="state.$error && state.maxLength == false"
      >Bu alana en çok {{state.$params.maxLength.max}} karakter girebilirsiniz.</div>

      <small class="form-text text-muted pl-1" v-if="$slots.info">
        <slot name="info"></slot>
      </small>
    </b-form-group>
  </div>
</template>


<script>
import MaskedInput from "vue-text-mask";
import * as textMaskAddons from "text-mask-addons/dist/textMaskAddons";
import shared from "~/mixins/components";
import textbox from "~/mixins/textbox";
export default {
  components: {
    MaskedInput
  },
  data: () => ({
    emailMask: textMaskAddons.emailMask
  }),
  mixins: [shared, textbox]
};
</script>