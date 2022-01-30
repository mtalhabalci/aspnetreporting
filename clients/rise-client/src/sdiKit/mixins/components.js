export default {
    props: {
        label: {
            type: String,
            default: "Etiket Yok"
        },
        direction: {
            type: String,
            validator: val => ["vertical", "horizontal"].includes(val),
            default: "vertical"
        },
        state: {
            type: Object,
            default: function () {
                return {};
            }
        }
    },
    computed: {
        layout() {
            return this.direction === "horizontal" ? {
                "label-align-md": "right",
                "label-cols-md": 2
            }
                : null;
        }, 
        attrIsRequired() {
            if (
                this.state.$params &&
                this.state.$params.required &&
                this.state.$params.required.type == "required"
            )
                return true;
            else return false;
        },
        isInvalid() {
            if (this.state) return this.state.$error;
            return false;
        }
    }
};
