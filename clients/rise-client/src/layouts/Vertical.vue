<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="dark" class="navbar-vertical navbar-icon">
      <div class="container">
        <b-navbar-brand to="/home/index" class="navbar-brand-autodark">
          <img src="/images/logo-white-without-text.png" class="my-4" />
        </b-navbar-brand>

        <b-collapse id="nav-collapse" is-nav>
          <b-navbar-nav>
            <b-nav-item
              href="#"
              class="navbar-icon-item active mb-3"
              v-b-tooltip.hover.bottom="'Ana Sayfa'"
            >
              <i class="fe fe-home"></i>
            </b-nav-item>
            <b-nav-item href="#" class="navbar-icon-item">
              <i class="fe fe-users"></i>
            </b-nav-item>
          </b-navbar-nav>
        </b-collapse>
      </div>
    </b-navbar>

    <div class="router-transitions">
      <router-view :key="$route.path" />
    </div>
  </div>
</template>

<script>
import AppFooter from "./_footer";

export default {
  name: "app-default",
  components: {
    "app-footer": AppFooter
  }
};
</script>
<style lang="scss" scoped>
.navbar-icon {
  width: 75px;

  .navbar-icon-item {
    &.active {
      // background: #2c3645;
    }
    a {
      &:hover {
        background: #2c3645;
      }
      padding: 0.6rem !important;

      i {
        font-size: 1.5rem;
      }
    }
  }
}
</style>
