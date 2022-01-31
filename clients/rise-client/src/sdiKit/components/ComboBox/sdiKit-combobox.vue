<template>
  <div>
    <b-form-group :label="label" v-bind="layout" :class="{ 'input-required': attrIsRequired } ">
      <multiselect
        class="multiselect-info-light"
        v-model="selected"
        placeholder="Seçiniz"
        :options="options"
        :multiple="isMultiple"
        :taggable="isTaggable"
        :internal-search="true"
        :searchable="isSearchable"
        :loading="isLoading"
        :hide-selected="false"
        :show-labels="false"
        :limit-text="limitText"
        :show-no-results="true"
        @search-change="search"
        @input="updateValue"
        @open="open"
        @tag="addTag"
        label="text"
        track-by="id"
        :clear-on-select="false"
        :close-on-select="true"
        v-bind:class="{ 'is-invalid': isInvalid }"
        :maxlength="maxLength"
      >
        <template slot="clear">
          <div class="multiselect__clear" v-if="selected" @mousedown.prevent.stop="clear">
            <i class="ion ion-md-close"></i>
          </div>
        </template>

        <span slot="noResult">Sonuç bulunamadı...</span>
        <span slot="noOptions">Sonuç bulunamadı...</span>
      </multiselect>
      <div class="invalid-tooltip" v-if="state.$error && !state.required">Bu alan gereklidir</div>
    </b-form-group>
  </div>
</template>

<script>
import {
  Ajax,
  ObjectToQueryString,
  RandomElementId,
  Notify
} from "~/utils/index";
import Components from "~/mixins/components";

import _ from "lodash";
import vueMultiSelect from "vue-multiselect";
import { Promise } from "q";

export default {
  props: {
    url: {
      type: String,
      required: true
    },
    parent: {
      type: [Number, String]
    },
    changed: {
      type: Function
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    isTaggable: {
      type: Boolean,
      default: false
    },
    isSearchable: {
      type: Boolean,
      default: true
    },
    maxLength: {
      type: Number
    }
  },
  data: () => ({
    selected: [],
    options: [],
    isLoading: false
  }),
  methods: {
    createAutoCompleteObject(id, text, code, isTag) {
      return {
        id: id || 0,
        text: text || null,
        code: code || null
      };
    },
    addItemToSelected(autoCompleteObject) {
      let keywordExistsInSelected = this.selected.find(
        a => a["text"] == autoCompleteObject.text
      );
      this.selected.push(autoCompleteObject);
      if (!keywordExistsInSelected) {
      }
    },
    addItemToOptions(autoCompleteObject) {
      this.options.push(autoCompleteObject);
    },

    addTag(newTag) {
      if (newTag.length > this.maxLength) {
        Notify.error(
          this.label +
            " alanında " +
            this.maxLength +
            " karakterden fazla olamaz."
        );
        return;
      }

      let tagObject = this.createAutoCompleteObject(
        RandomElementId(),
        newTag,
        null
      );
      this.addItemToOptions(tagObject);
      this.addItemToSelected(tagObject);
      this.updateValue();
    },
    limitText(count) {
      return "Daha fazla...";
    },
    search: _.debounce(function(query) {
      this.ajax(query);
    }, 750),
    clear() {
      this.selected = [];
      this.updateValue();
    },
    set(id, then) {
      this.ajax(null, id, then);
    },
    updateValue() {
      if (this.selected && !Array.isArray(this.selected)) {
        this.selected = [this.selected];
      }
      this.$emit("input", this.selected ? this.selected : null);
      this.$emit("valuechanged", this.selected ? this.selected : null);
      if (this.changed) {
        this.changed();
      }
    },
    open() {
      // if (this.options.length > 0) {
      //   return;
      // }
      this.ajax();
    },
    ajax(query, param, callBack) {
      var self = this;
      query = query || "";
      self.isLoading = true;
      let request = {
        pageSize: 15,
        searchTerm: query,
        selectedItems: param || null
      };
      if (this.parent) {
        request.parentId = this.parent ? this.parent : 0;
      }
      let requestUrl = this.url + "?" + ObjectToQueryString(request);
      Ajax.post(this.url, request, response => {
        let data = response;
        self.options = data.results;

        if (request.selectedItems && data.results && data.results.length > 0) {
          for (let index = 0; index < data.results.length; index++) {
            const item = data.results[index];
            let tagObject = self.createAutoCompleteObject(item.id, item.text);
            self.addItemToSelected(tagObject);
          }
          self.updateValue();
        }
        if (callBack) {
          callBack();
        }

        self.isLoading = false;
      });
    }
  },
  components: {
    multiselect: vueMultiSelect
  },
  mixins: [Components],
  watch: {
    parent: function(newVal, oldVal) {
      this.clear();
      this.options = [];
    }
  }
};
</script>