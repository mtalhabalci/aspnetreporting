<template>
  <span>
    <b-button v-bind:id="id" variant="link" class="m-0 px-2 py-0">
      <i class="fas fa-question-circle"></i>
    </b-button>
    <b-popover
      v-bind:id="id + '-container'"
      :target="id"
      :container="id + '-container'"
      v-bind:class="'popover-'+ variant +' d-inline-block sdiKit-popover'"
      :placement="placement"
      triggers="hover"
      :title="title"
      :content="content"
    ></b-popover>
  </span>
</template>

<script>
import { Json2Array, RandomElementId } from "~/utils";
import { Placement, BootstrapVariant } from "~/enums";

export default {
  name: "sdiKit-tooltip",
  props: {
    id: {
      type: String,
      required: false,
      default: () => {
        return RandomElementId();
      }
    },
    title: {
      type: String,
      default: "Bilgi"
    },
    content: {
      type: String,
      required: true,
      default: ""
    },
    placement: {
      type: String,
      validator: val => Json2Array(Placement).includes(val),
      default: Placement.Bottom
    },
    variant: {
      type: String,
      validator: val => Json2Array(BootstrapVariant).includes(val),
      default: BootstrapVariant.Primary
    }
  }
};
</script>
