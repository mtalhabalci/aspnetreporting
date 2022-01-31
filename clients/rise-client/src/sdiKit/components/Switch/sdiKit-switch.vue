<template>
<b-form-group>
    <label class="switcher">
        <input type="checkbox" class="switcher-input" v-model="checked">
        <span class="switcher-indicator">
            <span class="switcher-yes"></span>
            <span class="switcher-no"></span>
        </span>
        <span class="switcher-label">{{label}}</span>
    </label>
</b-form-group>
</template>

<script>
export default {
    props: {
        value: {
            type: Boolean,
            default: false
        },
        label: {
            type: String,
            default: "Etiket Yok"
        }
    },
    computed: {
        checked: {
            get() {
                return this.value;
            },
            set(val) {
                this.$emit('input', val);
            }
        }
    }
};
</script>
