<template>
  <div>
    <b-form-group :label="label" v-bind="layout">
      <markdown-editor
        ref="markdownEditor"
        v-bind:class="{ 'is-invalid': isInvalid , 'is-valid' : !isInvalid}"
        @input="changeEvent"
        v-model="text"
        :configs="configs"
      ></markdown-editor>
      <div class="invalid-tooltip" v-if="state.$error && !state.required">Bu alan gereklidir</div>

      <small class="form-text text-muted pl-1" v-if="$slots.info">
        <slot name="info"></slot>
      </small>
    </b-form-group>
  </div>
</template>

<style src="simplemde/dist/simplemde.min.css"></style>


<script>
import shared from "~/mixins/components";

export default {
  mixins: [shared],

  props: {
    value: {
      type: String,
      required: false,
      default: ""
    },
    postRender: {
      type: Function,
      default: function(html) {}
    }
  },
  data: () => ({
    configs: {
      spellChecker: false
    }
  }),
  mounted() {
    this.handleOutputHTML();
  },
  computed: {
    simplemde() {
      return this.$refs.markdownEditor.simplemde;
    },
    text: {
      set: function(val) {
        if (!val) {
          val = "";
        }
        this.$emit("input", val);
      },
      get: function() {
        return this.value || "";
      }
    }
  },
  methods: {
    handleOutputHTML() {
      if (this.value) {
        let output = this.simplemde.markdown(this.value);
        this.postRender(output);
      }
    },
    changeEvent() {
      this.$emit("change", this.value);
      //      this.handleOutputHTML();
    }
  }
};
</script>
