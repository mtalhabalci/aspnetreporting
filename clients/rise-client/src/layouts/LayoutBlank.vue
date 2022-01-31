<template>
  <div class="border-top-wide border-primary d-flex flex-column">
    <div class="flex-fill d-flex flex-column justify-content-center">
        <div class="background-image-class">
        <div class="bg"></div>
        <div class="info-holder">
        </div>
      </div>
      <div class="router-transitions">
        <router-view :key="$route.path"></router-view>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "app-layout-blank"
};
</script>
<style>
.background-image-class .bg {
    position: absolute;
    opacity: 0.73;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
  background-image: url("/images/login-background.jpg");
    background-size: cover;
    background-position: center;
    z-index: -1;
}
</style>