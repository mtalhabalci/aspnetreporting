<template>
  <div>
    <b-form-group :label="label" v-bind="layout">
      <datepicker
        v-model="dateValue"
        :language="language"
        :bootstrapStyling="true"
        :monday-first="true"
        :full-month-name="true"
        :placeholder="placeholder"
        :calendar-button="true"
        calendar-button-icon="fas fa-calendar"
        :clear-button="true"
        :format="customFormatter"
        v-bind:class="{ 'is-invalid': isInvalid }"
        @selected="itemSelected"
        :use-utc="useUtc"
      />
      <div class="invalid-tooltip" v-if="state.$error && !state.required">Bu alan gereklidir</div>
      <small class="form-text text-muted pl-1" v-if="$slots.info">
        <slot name="info"></slot>
      </small>
    </b-form-group>
  </div>
</template>

<script>
import ApplicationConstant from "~/constant";

import moment from "moment";
import Datepicker from "vuejs-datepicker";
import { tr } from "vuejs-datepicker/dist/locale";
import shared from "~/mixins/components";
import dateTimeUtils from "./dataTimeUtils";
export default {
  name: "sdiKit-date", // using EXACTLY this name is essential
  props: {
    value: {
      validator: val => dateTimeUtils.validateDateInput(val)
    },
    placeholder: {
      type: String
    },
    selected: {
      type: Function
    }
  },
  data: () => ({
    language: tr,
    useUtc: true
  }),
  components: {
    Datepicker
  },
  methods: {
    customFormatter(date) {
      return moment(date).format(ApplicationConstant.settings.dateFormat);
    },
    itemSelected: function(args) {
      if (this.selected) {
        this.selected(args);
      }
    }
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
        var selectedDate = moment(val)
          .utc(true)
          .hour(0)
          .minute(0)
          .second(0)
          .millisecond(0);
        this.$emit("input", selectedDate.toDate());
      }
    }
  },
  mixins: [shared]
};
</script>