import { GridActionType } from "~/enums";
import { Json2Array } from "~/utils";

export default {
    data: () => ({
        gridColumns: [],
        filterTemp: {}
    }),
    props: {
        componentColumns: {
            type: Array,
            default: function() {
                return [];
            }
        },
        filter: {
            type: Object,
            required: false,
            default: () => ({})
        },
        gridModelName: {
            type: String,
            required: false
        },
        rowHasDefaultAction: {
            type: Boolean,
            required: false
        },
        defaultActionType: {
            type: String,
            validator: val => Json2Array(GridActionType).includes(val),
            default: "inline"
        },
        url: {
            type: String,
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
    methods: {

    }

};
