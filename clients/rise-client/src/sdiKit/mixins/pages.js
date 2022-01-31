import { Notify } from "~/utils/index";

export default {
    props: {
        store: {
            type: Object
        }
    },
    data() {
        return {
            id: undefined,
            rules: [],
            selected: {},
            filter: {},
            settings: [],
            model: {},
            isSubmitted: false,
            isNewRecord: true,
            deleteButtonExists: false,
            extras: {
                canSave: true
            },
            configs: {
                spellChecker: false
            }
        };
    },

    validations() {
        return {
            selected: this.rules
        };
    },
    computed: {
        pageWidgetTitle() {
            return this.id ? 'Kayıt Güncelle' : 'Yeni Kayıt';
        }
    },
    methods: {
        validate() {
            this.$v.selected.$touch();
            var isValid = !this.$v.selected.$invalid;
            this.$emit("on-validate", this.selected, isValid);
            if (isValid === false) {
                Notify.error("Lütfen form/model alanlarını kontrol ediniz.");
            }
            return isValid;
        },
        setRules(rules) {
            this.rules = rules;
        }
    }

};
