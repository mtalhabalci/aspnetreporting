import 'vue2-perfect-scrollbar/dist/vue2-perfect-scrollbar.css'

import App from './App.vue'
import BootstrapVue from 'bootstrap-vue';
import Notifications from 'vue-notification';
import PerfectScrollbar from 'vue2-perfect-scrollbar'
import Register from './register'
import RiseComponents from '@/components/index';
import Vue from 'vue'
import VueCollapse from 'vue2-collapse'
import Vuelidate from "vuelidate"
import globalMixin from "@/mixins/globalMixin";
import moment from 'moment';
import router from './routers';
import store from '@/store/store';
Vue.filter("date-format", (item) => {
  return item ? moment(item).format("DD/MM/YYYY") : '';
});

Vue.filter("datetime-format", (item) => {
  return item ? moment(item).format("DD/MM/YYYY HH:mm") : '';
});
Vue.filter("datetime-format2", (item) => {
  return item ? moment(item).format("DD/MM/YYYY HH:mm:ss") : '';
});

Vue.use(VueCollapse)
Vue.use(Vuelidate);
Vue.config.productionTip = false
Vue.use(PerfectScrollbar);
Register.useRise()
Vue.use(BootstrapVue);;
Vue.use(Notifications);
Vue.mixin(globalMixin);
RiseComponents();

var rootElement = new Vue({
  store,
  router,
  render: h => h(App),
}).$mount('#app')


window.rootElement = rootElement;
