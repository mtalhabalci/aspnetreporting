import Vue from "vue";

export default {
    success(text, duration) {
        Vue.notify({
            group: "notifications-default",
            title: "İşlem Başarılı",
            text: resolveMessage(text),
            duration: duration || 5000,
            type: "bg-success",
            data: 1
        });
    },
    warning(text, duration) {
        Vue.notify({
            group: "notifications-default",
            title: "Uyarı",
            text: resolveMessage(text),
            duration: duration || 5000,
            type: "bg-warning",
            data: 2
        });
    },
    info(text, duration) {
        Vue.notify({
            group: "notifications-default",
            title: "Bilgilendirme",
            text: resolveMessage(text),
            duration: duration || 5000,
            type: "bg-info",
            data: 3
        });
    },
    error(text, duration) {
        Vue.notify({
            group: "notifications-default",
            title: "Hata",
            text: resolveMessage(text),
            duration: duration || 5000,
            type: "bg-danger",
            data: 4
        });
    },
    other(text, duration) {
        Vue.notify({
            group: "notifications-default",
            title: "Bilgilendirme",
            text: resolveMessage(text),
            duration: duration || 5000,
            type: "bg-secondary",
            data: 3
        });
    }
};

function resolveMessage(data) {
    let text = "";
    if (typeof data === "object") {
        if (data.Code) {
            text += data.Code;
        }
        if (data.Message) {
            text += " " + data.Message;
        }
    } else {
        text = data;
    }
    return text;
}
