import { Cookies, Notify, Request, Storage } from "~/utils/index";

import router from "@/routers";
import store from '@/store/store';

export default {
    get: (url, callback, errorCallback) => {
        Request.get(url, {
            headers: getHeaderInformation()
        }).then(function (response) {
            handleResponse(response, callback);
        }).catch(function (error) {
            handleResponse(error.response, errorCallback);
        });
    },
    getBlobFile: (url, callback, errorCallback) => {
        Request.get(url, {
            responseType: 'blob',
            headers: getHeaderInformation()
        }).then(function (response) {
            handleResponse(response, callback);
        }).catch(function (error) {
            handleResponse(error.response, errorCallback);
        });
    },
    post: (url, data, callback, errorCallback) => {
        let axiosConfig = {
            headers: getHeaderInformation()
        };
        Request.post(url, data, axiosConfig)
            .then(function (response) {
                handleResponse(response, callback);
            })
            .catch(function (error) {
                handleResponse(error.response, errorCallback);
            });
    },
    postMultiPart: (url, data, callback, errorCallback) => {
        let axiosConfig = {
            headers: getHeaderInformation()
        };
        axiosConfig.headers['Content-Type'] = 'multipart/form-data';
        Request.post(url, data, axiosConfig)
            .then(function (response) {
                handleResponse(response, callback);
            })
            .catch(function (error) {
                handleResponse(error.response, errorCallback);
            });
    },
    postPromise: (url, data) => {
        let axiosConfig = {
            headers: getHeaderInformation()
        };
        return Request.post(url, data, axiosConfig).catch(function (error) {
            handleResponse(error.response);
        });
    },
    getPromise: (url) => {
        return Request.get(url, {
            headers: getHeaderInformation()
        }).catch(function (error) {
            handleResponse(error.response);
        });
    }
};

function useErrorModal(data) {
    let layout = window.rootElement.$children[0];
    if (layout && layout.$refs) {
        if (layout.$refs.exceptionModal) {
            layout.errorMessage = data.content ? data.content : "Beklenmeyen bir hata oluştu.";
            layout.errorId = data.id;
            layout.errorCode = data.Code;
            layout.errorDate = new Date(data.Date);
            layout.$refs.exceptionModal.show();
        }
    }
}

function handleResponse(response, callback) {
    if (!response) {
        store.dispatch("setContainerLoader", false);
        Notify.error("Sunucudan cevap alınamıyor");
        return;
    }

    if (response.headers.hasOwnProperty("exception")) {
        if (response.headers.exception === "DomainException") {
            if (response.data.content) {
                store.dispatch("setContainerLoader", false);
                Notify.error(response.data.content);
            } else {
                store.dispatch("setContainerLoader", false);
                Notify.error("Beklenmedik bir hata oluştu.");
            }
            if (callback) {
                callback(response.data);
            }
        } else {
            useErrorModal(response.data);
            if (callback) {
                callback(response.data);
            }
        }
        return;
    }

    if (response.status === 200) {
        if (callback) {
            callback(response.data);
        }
    } else if (response.status === 201) {
        Notify.info(response.data.content);
        if (callback) {
            callback(response.data);
        }
    } else if (response.status === 503) {
        if (callback) {
            callback(response.data);
        }
    }
    else if (response.status === 202) {
        Notify.warning(response.data.content);
        if (callback) {
            callback(response.data);
        }
    }
    else if (response.status === 400) {
        if (response.data.content) {
            store.dispatch("setContainerLoader", false);
            Notify.error(response.data.content);
        } else {
            store.dispatch("setContainerLoader", false);
            Notify.error("400 Bad Request");
        }
        if (callback) {
            callback(response.data);
        }
    } else if (response.status === 401) {
        if ((response.headers.hasOwnProperty("Token-Expired") && response.headers["Token-Expired"] === "true") ||
            (response.headers.hasOwnProperty("token-expired") && response.headers["token-expired"] === "true")) {
            window.location.href = "/account/login?action=expired";
            return;
        }

        router.push({ name: "error-401" });
    } else {
        if (response.data.content) {
            Notify.error(response.data.content);
            store.dispatch("setContainerLoader", false);
        } else {
            Notify.error("Beklenmedik bir hata oluştu.");
            store.dispatch("setContainerLoader", false);
        }

        if (callback) {
            callback(response.data);
        }
    }
}

function getHeaderInformation() {
    let jwt = Cookies.getToken();
    return {
        'Content-Type': 'application/json;charset=UTF-8',
        "Access-Control-Allow-Origin": "*",
        "Authorization": `Bearer ${jwt}`
    };
}
