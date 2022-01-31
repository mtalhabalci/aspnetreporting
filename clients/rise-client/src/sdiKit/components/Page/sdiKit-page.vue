<template>
  <div>
    <div class="page-header mb-1">
      <div class="row" v-if="header">
        <div class="col-12 pageconfig">
          <h3>
            <slot v-if="hasParentHeaderSlot" name="parent-header"></slot>

            <span v-if="!hasParentHeaderSlot">{{parentHeader}}</span>

            <span v-if="showParentHeader && parentHeader" class="mx-2">/</span>

            <slot v-if="hasHeaderSlot" name="header"></slot>

            <span v-if="!hasHeaderSlot">{{header}}</span>

            <sdiKit-tooltip v-if="isTooltipDescription" :title="header" :content="description" />
          </h3>
          <p class="pageconfig-description lead" v-if="!isTooltipDescription">{{ description }}</p>
        </div>
      </div>
    </div>
    <hr class="border-light mt-0 mb-4" v-if="header" />
    <div class="row" v-if="hasDefaultSlot">
      <div class="col-12">
        <slot></slot>
      </div>
    </div>
  </div>
</template>


<script>
import Pages from "~/mixins/pages";
export default {
  mixins: [Pages],
  name: "sdiKit-page",
  props: {
    showParentHeader: {
      type: Boolean,
      default: true
    },
    parentHeader: {
      type: String,
      required: false,
      default: undefined
    },
    header: {
      type: String,
      required: true,
      default: undefined
    },
    description: {
      type: String,
      required: false,
      default: undefined
    },
    captureLog: {
      type: Boolean,
      required: false
    },
    breadCrumbDetails: {
      type: Object,
      required: false
    }
  },
  computed: {
    isTooltipDescription() {
      return (
        this.app.page.showDescription == "true" &&
        this.app.page.showDescriptionInTooltip == "true" &&
        this.description
      );
    },
    hasDefaultSlot() {
      return !!this.$slots.default;
    },
    hasHeaderSlot() {
      return !!this.$slots.header;
    },
    hasParentHeaderSlot() {
      return !!this.$slots["parent-header"];
    }
  }
};
</script>
<style scoped lang="scss">
.blockui {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  opacity: 0.5;
  background-color: #000;
  z-index: 1000;
  overflow: auto;
}
.blockui div {
  position: absolute;
  top: 50%;
  left: 50%;
  width: 5em;
  height: 2em;
  margin: -1em 0 0 -2.5em;
  color: #fff;
  font-weight: bold;
}

.blockui img {
  position: relative;
  top: 43%;
  left: 48%;
}
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
