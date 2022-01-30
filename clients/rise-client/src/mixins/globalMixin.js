import store from '@/store/store';
import constant from '~/constant';
import { OutputEnums } from "@/enums/index";
export default {
    data(){
        return{
            app: constant
        }
    }, 
    enums: {
        OutputEnums
    },
    methods: {
        showLoaderContainer() {
            store.dispatch("setContainerLoader", true);
        },
        hideLoaderContainer() {
            store.dispatch("setContainerLoader", false);
        }
    }
};
