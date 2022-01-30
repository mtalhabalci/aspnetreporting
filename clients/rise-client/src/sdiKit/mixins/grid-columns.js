export default {
    props: {
        item: {
            type: Object,
            required: true
        },
        field: {
            type: Object,
            required: true
        }
    },

    methods: {
        isInteger(type) {
            if (type && type.includes("int")) {
                return true;
            }
            return false;
        },
        isDecimal(type) {
            if (type && type.startsWith("decimal")) {
                return true;
            }
            return false;
        }
    }

};
