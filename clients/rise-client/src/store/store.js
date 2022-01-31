import * as actions from "./actions";
import * as getters from "./getters";
import * as mutations from "./mutations";

import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        isAuthenticated: false,
        jwt: null,
        user: {},
        privileges: [],
        privilegeNames: {},
        applicationInformationIsLoaded: false,
        containerLoader: false
    },
    getters,
    actions,
    mutations
});
