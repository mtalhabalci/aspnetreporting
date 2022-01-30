import Fragment from 'vue-fragment';
import Vue from "vue";
import Enums from "@/mixins/enum";
import VueCookies from "vue-cookies"
export default {
    useRise() {
        Vue.use(Fragment.Plugin);
        Vue.use(Enums, { namespace: '$enums' });
        Vue.use(VueCookies);
    }
};