export default function json2Array (j) {
    return Object.keys(j).map(function (_) { return j[_]; });
}
