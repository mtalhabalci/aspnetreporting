<template>
  <div>
    <b-button
      variant="default btn-sm icon-btn md-btn-flat bg-white"
      @click="downloadFile"
      v-b-tooltip.hover.right
      :title="'Dosyayı İndir'"
    >
      <i class="fas fa-download"></i>
    </b-button>
  </div>
</template>

<script>
export default {
  props: {
    value: {
      type: Object,
      required: true,
    },
    download: {
      type: Function,
      default: () => {},
    },
  },
  methods: {
    downloadFile() {
      this.download();
    },
  },
};
</script>
