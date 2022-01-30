export default function randomElementId () {
    return '_' + (
        Number(String(Math.random()).slice(2)) +
        Date.now() +
        Math.round(performance.now())
    ).toString(36);
}
