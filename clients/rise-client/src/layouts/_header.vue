<template>
  <b-navbar toggleable="lg" type="dark" variant="primary">
    <b-navbar-brand to="/home/index" class="mr-3">Ecosim</b-navbar-brand>

    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item to="/home/index">
          <i class="fe fe-home mr-2"></i> Ana Sayfa
        </b-nav-item>
        <b-nav-item to="/report/list">
          <i class="fe fe-package mr-2"></i> Rapor Yönetimi
        </b-nav-item>
      </b-navbar-nav>

    </b-collapse>
  </b-navbar>
</template>
	

<script>
export default {
  data() {
    return {};
  },
  computed: {
    currentUser() {
      let user = this.$store.getters.getUser;
      return user;
    },
  },
  mounted() {
  },
  name: "app-header",
  beforeMount() {
  },
  methods: {
  },
};
</script>
