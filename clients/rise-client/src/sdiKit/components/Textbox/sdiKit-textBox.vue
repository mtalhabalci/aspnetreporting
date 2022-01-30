<template>
  <div>
    <b-form-group :label="label" v-bind="layout" :class="{ 'input-required': attrIsRequired }">
      <input
        :type="type"
        :placeholder="placeholder"
        autocomplete="true"
        v-model="text"
        :maxlength="maxLength"
        @change="onChange"
        v-bind:class="{ 'is-invalid': isInvalid}"
        class="form-control"
        :disabled="disabled"
      />
      <div
        class="invalid-tooltip"
        v-if="state.$error && state.required !== undefined && state.required==false"
      >Bu alan gereklidir</div>
      <div
        class="invalid-tooltip"
        v-if="state.$error  && state.url!==undefined && state.url == false"
      >Bu alan url olmalıdır</div>
      <div
        class="invalid-tooltip"
        v-if="state.$error && state.eMail!==undefined && state.eMail == false"
      >Bu alan e-posta adresi olmalıdır</div>

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
import textbox from "~/mixins/textbox";
import shared from "~/mixins/components";
export default {
  props: {
    maxLength: {
      type: Number
    },
    type: {
      type: String,
      default: "text"
    }
  },
  mixins: [shared, textbox]
};
</script>