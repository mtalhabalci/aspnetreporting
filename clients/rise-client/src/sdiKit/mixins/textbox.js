export default {
    props: {
        value: {
            type: String,
            default: ''
        },
        placeholder: {
            type: String
        },
        textchanged: {
            type: Function
        },
        maxLength: {
            type: Number
        },
        disabled: {
            type: Boolean,
            default: false
        }
    },
    methods: {
        onChange() {
            if (this.textchanged) {
                this.textchanged();
            }
        }
    },
    computed: {
        text: {
            get() {
                return this.value;
            },
            set(val) {
                this.$emit('input', val);
            }
        }
    }
};
