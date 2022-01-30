<template>
  <div>
    <b-form-group :label="label" v-bind="layout">
      <textarea
        :placeholder="placeholder"
        autocomplete="false"
        rows="5"
        v-model="text"
        @change="onChange"
        class="form-control"
        :maxlength="maxLength"
        v-bind:class="{ 'is-invalid': isInvalid}"
      ></textarea>
      <div class="invalid-tooltip" v-if="state.$error && state.required !== undefined && state.required==false">Bu alan gereklidir</div>
      <div
        class="invalid-tooltip"
        v-if="state.$error && state.maxLength == false"
      >Bu alana {{state.$params.maxLength.max}} karakter girebilirsiniz.</div>
      <small class="form-text text-muted pl-1" v-if="$slots.info">
        <slot name="info"></slot>
      </small>
    </b-form-group>
  </div>
</template>

<script>
import Shared from "~/mixins/components";
import Textbox from "~/mixins/textbox";

export default {
  props: {
    value: {
      type: String,
      default: ""
    },
    placeholder: {
      type: String
    },
    textchanged: {
      type: Function
    }
  },
  mixins: [Shared, Textbox]
};
</script>
<style>
textarea {
  min-height: 100px;
}
</style>

