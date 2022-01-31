export default {
    props: {
        value: {
            type: Object,
            required: true
        },
        editPageUrl: {
            type: String,
            required: false
        },
        viewPageUrl: {
            type: String,
            required: false
        },
        deleteAction: {
            type: Function
        }
    },
    data() {
        return {};
    },
    methods: {
        editRow() {
            debugger;
            this.$router.push({ path: this.editPageUrl + this.value.id });
        },
        viewRow() {
            this.$router.push({ path: this.viewPageUrl + this.value.id });
        },
        deleteRow() {
            if (this.deleteAction) {
                this.deleteAction(this.value);
            }
        }
    }
};
