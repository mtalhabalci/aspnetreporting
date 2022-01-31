import toQueryString from "./objectToQueryString";

export default function objectToQueryString (obj, prefix) {
    var str = [];
    for (var p in obj) {
        if (obj.hasOwnProperty(p)) {
            var k = prefix ? prefix + "[" + p + "]" : p;
            var v = obj[p];
            str.push(typeof v === "object"
                ? toQueryString(v, k) : encodeURIComponent(k) + "=" + encodeURIComponent(v));
        }
    }
    return str.join("&");
}
