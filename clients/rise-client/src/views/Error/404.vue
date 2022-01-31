<template>
  <div class="container-tight py-5">
    <div class="empty">
      <div class="empty-icon">
        <div class="display-4">404</div>
      </div>
      <p class="empty-title h2">Hay aksi…</p>
      <p class="empty-subtitle text-muted">Erişmeye çalıştığınız sayfa bulunamadı...</p>
      <div class="empty-action">
        <b-btn @click="goBack" variant="primary">
          <i class="fe fe-arrow-left mr-2"></i> Geri Dön
        </b-btn>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    goBack: () => {
      window.history.back();
    }
  }
};
</script>