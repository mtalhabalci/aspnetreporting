<template>
  <div>
    <app-header />
    <!-- <app-sidenav></app-sidenav> -->
    <div class="page">
      <div class="content">
        <div class="router-transitions">
          <b-overlay :show="containerLoader">
            <router-view :key="$route.path"></router-view>
          </b-overlay>
        </div>
      </div>
    </div>
    <app-footer />
  </div>
</template>

<script>
import AppHeader from "./_header";
import AppFooter from "./_footer";
import AppVertical from "./Vertical";

export default {
  name: "app-default",
  components: {
    "app-vertical": AppVertical,
    "app-header": AppHeader,
    "app-footer": AppFooter,
  },
 
  computed: {
    containerLoader() {
      return this.$store.getters.getContainerLoader;
    },
  },
};
</script>
