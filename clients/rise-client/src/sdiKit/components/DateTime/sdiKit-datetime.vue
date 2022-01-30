<template>
  <div>
    <b-form-group :label="label" v-bind="layout">
      <div class="row">
        <div class="col-md-12 margin-bottom-sm">
          <datetime
            class="form-control"
            format="dd/MM/yyyy HH:mm"
            :minute-step="5"
            value-zone="Europe/Istanbul"
            input-class="sdiKit-vdatetime"
            :phrases="{ok: 'Seç', cancel: 'Çık'}"
            v-bind:class="{ 'is-invalid': isInvalid }"
            type="datetime"
            v-model="dateValue"
          ></datetime>
        </div>
        <div class="invalid-tooltip" v-if="state.$error && !state.required">Bu alan gereklidir</div>
      </div>
    </b-form-group>
  </div>
</template>

<script>
import { Datetime } from "vue-datetime";
import shared from "~/mixins/components";
import { Settings } from "luxon";
import "vue-datetime/dist/vue-datetime.css";

Settings.defaultLocale = "tr";

export default {
  props: {
    value: {
      type: String,
    }
  },
  components: {
    Datetime
  },
  computed: {
    dateValue: {
      get() {
        if (this.value) {
          return this.value;
        }
        return;
      },
      set(val) {
        if (val == null) {
          this.$emit("input", null);
          return;
        }
        this.$emit("input", val);
      }
    }
  },
  mixins: [shared]
};
</script>

<style>
.sdiKit-vdatetime {
  border: none !important;
  width: 100% !important;
}
</style>