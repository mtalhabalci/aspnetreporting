var axios = require('axios');
var v4 = require('uuid');

window.baseURL = process.env.VUE_APP_API_URL;
var axiosInstance = axios.create({
    baseURL: process.env.VUE_APP_API_URL,
    json: true
});
axiosInstance.interceptors.request.use((config) => {

    if (!window.sessionStorage.getItem("Correlation")) {
        window.sessionStorage.setItem("Correlation", v4());
    }
    config.headers['X-CORRELATION-ID'] = window.sessionStorage.getItem("Correlation");
    return config;
});
module.exports = axiosInstance;
