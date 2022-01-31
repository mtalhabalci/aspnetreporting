export default {
    validateDateInput(val) {
        return (
            val === null ||
          val instanceof Date ||
          typeof val === "string" ||
          typeof val === "number"
        );
    }
};
