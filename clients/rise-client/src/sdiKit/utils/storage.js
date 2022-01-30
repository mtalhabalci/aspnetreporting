import Notify from './notify';
import VueCookies from "vue-cookies"
import _ from 'lodash';

function a(key, value) {
    window.localStorage.setItem(key, JSON.stringify(value));
}

function b(code) {
    let json = window.sessionStorage.getItem(d.LookUps);
    let items = JSON.parse(json);
    let lookup = _.find(items, l => l.code === code);
    if (!lookup) {
        Notify.error('Üst veri kodu eşleştirilemedi');
        return null;
    }
    return lookup.id;
}

function c(key) {
    return window.sessionStorage.getItem(key);
}

var d = {
    LookUps: 'lookups',
    Enums: 'enums',
    Session: 'session',
    Views: 'views',
    Correlation: 'correlation'
};

function e(name) {
    let json = window.localStorage.getItem(d.Enums);
    let items = JSON.parse(json);

    let data = _.find(items, l => l.name === name);
    if (!data) {
        Notify.error('Anahtar kelime eşleştirilemedi');
        return null;
    }
    return data.values;
}

function f() {
    let json = window.sessionStorage.getItem(d.Session);
    let item = JSON.parse(json);
    if (item) {
        return item.user;
    }
    return null;
}

function g() {
    let json = VueCookies.get("tokenAdmin")
    if (json) {
        return json;
    }
    return null;
}

function h() {
    window.sessionStorage.removeItem(d.Enums);
    window.sessionStorage.removeItem(d.LookUps);
    window.sessionStorage.removeItem(d.Views);
}

function i(name) {
    return JSON.parse(window.sessionStorage.getItem(name));
}
function j() {
    return window.sessionStorage.getItem(d.Views);
}

export default {
    hasKey: c,
    set: a,
    get: i,
    getLookup: b,
    keys: d,
    getEnum: e,
    getUser: f,
    getToken: g,
    clear: h,
    getViews: j
};
