import { email, maxLength, minLength, required, url } from 'vuelidate/lib/validators';

import { Ajax } from "~/utils/index";
import moment from 'moment';

export default {
    getViewModel(typeName, vm, callBack = undefined) {
        var namespace = btoa(typeName);
        Ajax.get('/Types/' + namespace, (response) => {
            let data = response;
            if (vm.settings) {
                let rules = this.createValidations(data);
                vm.setRules(this.getRuleInfo(rules));

                this.buildModel(vm, data);
                vm.$set(vm, 'model', data.properties);
            } else {
                vm(data);
            }

            if (callBack) {
                callBack();
            }
        });
    },

    createColumns(definition) {
        var columns = [];
        for (let i = 0; i < definition.properties.length; i++) {
            const def = definition.properties[i];
            columns.push(new CreateColumn(def));
        }

        function CreateColumn(col) {
            this.key = col.name;
            this.tdClass = "grid-item-" + col.name;
            this.sortable = col.sortable;
            this.sortableAlias = col.sortableAlias;
            this.label = col.label;
            this.type = col.type;
            this.order = col.order;
            this.isHidden = col.isHidden;
            if (this.type === 'datetime') {
                this.formatter = (value, index, item) => {
                    return moment(value).format('DD/MM/YYYY');
                };
            }
        }
        return columns;
    },
    createValidations(definition) {
        var rules = [];
        for (let i = 0; i < definition.properties.length; i++) {
            const def = definition.properties[i];
            rules.push(new CreateValidation(def));
        }
        function CreateValidation(rule) {
            var obj = {};
            obj.key = rule.name;
            obj.isRequired = rule.isRequired;
            obj.isEmail = rule.isEmail;
            obj.isUrl = rule.isUrl;
            obj.maxLength = rule.maxLength;
            obj.minLength = rule.minLength;
            obj.step = rule.stepNumber;
            return obj;
        }
        return rules;
    },
    getRuleInfo(rules) {
        let obj = {
        };
        for (let index = 0; index < rules.length; index++) {
            let rule = rules[index];

            obj[rule.key] = {};
            if (rule.isRequired) {
                obj[rule.key].required = required;
            }
            if (rule.maxLength) {
                obj[rule.key].maxLength = maxLength(rule.maxLength);
            }
            if (rule.minLength) {
                obj[rule.key].minLength = minLength(rule.minLength);
            }
            if (rule.isEmail) {
                obj[rule.key].eMail = email;
            }
            if (rule.isUrl) {
                obj[rule.key].url = url;
            }
        }
        return obj;
    },
    buildModel(vm, model) {
        for (let index = 0; index < model.properties.length; index++) {
            const property = model.properties[index];
            vm.$set(vm.selected, property.name, property.value);
        }
    },
    mapDtoBaseToVm(vm, data) {
        vm.selected.id = data.id;
        vm.selected.created = data.created;
        vm.selected.createdOaDate = data.createdOaDate;
        vm.selected.createdBy = data.createdBy;
        vm.selected.modified = data.modified;
        vm.selected.modifiedOaDate = data.modifiedOaDate;
        vm.selected.modifiedBy = data.modifiedBy;
        vm.selected.isActive = data.isActive;
    }
};
