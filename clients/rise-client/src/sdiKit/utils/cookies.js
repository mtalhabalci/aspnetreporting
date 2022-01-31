import Notify from './notify';
import _ from 'lodash';
import VueCookies from "vue-cookies"
function g() {
    let json = VueCookies.get("tokenAdmin")
    if (json) {
        return json;
    }
    return null;
}
export default {
    getToken: g
};
