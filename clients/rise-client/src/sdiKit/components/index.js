import SdiKitCheckBox from './CheckBox/sdiKit-checkBox';
import SdiKitComboBox from './ComboBox/sdiKit-combobox';
import SdiKitDate from './DateTime/sdiKit-date';
import SdiKitDateTime from './DateTime/sdiKit-datetime';
import SdiKitColorPicker from './ColorPicker/sdiKit-color-picker';
import SdiKitRange from './Range/sdiKit-range';
import SdiKitEnumComboBox from './ComboBox/sdiKit-enum-combobox';
import SdiKitFile from './File/sdiKit-file';
import SdiKitForm from './Form/sdiKit-form';
import SdiKitGrid from './Grid/sdiKit-grid';
import SdiKitGridColumnBoolean from './Grid/sdiKit-grid-column-boolean';
import SdiKitGridColumnDateTime from './Grid/sdiKit-grid-column-datetime';
import SdiKitGridColumnDefaultAction from './Grid/sdiKit-grid-column-default-action';
import SdiKitGridColumnDefaultActionContextMenu from './Grid/sdiKit-grid-column-default-action-context-menu';
import SdiKitGridColumnNumber from './Grid/sdiKit-grid-column-number';
import SdiKitGridColumnString from './Grid/sdiKit-grid-column-string';
import SdiKitGridFilter from './Grid/sdiKit-grid-filter';
import SdiKitLabel from './Label/sdiKit-label';
import SdiKitNumber from './Number/sdiKit-number';
import SdiKitPage from './Page/sdiKit-page';
import SdiKitPageGrid from './PageGrid/sdiKit-page-grid';
import SdiKitPhone from './Phone/sdiKit-phone';
import SdiKitSwitch from './Switch/sdiKit-switch';
import SdiKitSwitcher from './Switch/sdiKit-switcher';
import SdiKitTextArea from './Textarea/sdiKit-textarea';
import SdiKitTextBox from './Textbox/sdiKit-textBox';
import SdiKitTextBoxEmail from './TextboxEmail/sdiKit-textbox-email';
import SdiKitTooltipInfo from './TooltipInfo/sdiKit-tooltip-info';
import SdiKitWidget from './Widget/sdiKit-widget';
import MarkDownEditor from 'vue-simplemde/src/markdown-editor';
import SdiKitEditor from './Editor/sdiKit-editor'
import VJstree from 'vue-jstree';
import Vue from 'vue';
import VueLadda from "../vendor/ladda/Ladda";


export default function UseSdiKitComponent () {
    Vue.component('sdiKit-page', SdiKitPage);
    Vue.component('sdiKit-tooltip', SdiKitTooltipInfo);
    Vue.component('sdiKit-combobox', SdiKitComboBox);
    Vue.component('sdiKit-enum-combobox', SdiKitEnumComboBox);

    Vue.component('sdiKit-checkbox', SdiKitCheckBox);
    Vue.component('sdiKit-date', SdiKitDate);
    Vue.component('sdiKit-datetime', SdiKitDateTime);
    Vue.component('sdiKit-color-picker', SdiKitColorPicker);
    Vue.component('sdiKit-range', SdiKitRange);
    Vue.component('sdiKit-textbox', SdiKitTextBox);

    Vue.component('sdiKit-label', SdiKitLabel);
    Vue.component('sdiKit-textbox-email', SdiKitTextBoxEmail);
    Vue.component('sdiKit-textarea', SdiKitTextArea);

    Vue.component('sdiKit-number', SdiKitNumber);
    Vue.component('sdiKit-phone', SdiKitPhone);
    Vue.component('sdiKit-switch', SdiKitSwitch);

    Vue.component('sdiKit-switcher', SdiKitSwitcher);
    Vue.component('sdiKit-file', SdiKitFile);
    Vue.component('sdiKit-widget', SdiKitWidget);
    Vue.component('sdiKit-form', SdiKitForm);
    Vue.component('sdiKit-editor', SdiKitEditor);

    Vue.component('sdiKit-grid-column-number', SdiKitGridColumnNumber);
    Vue.component('sdiKit-grid-column-string', SdiKitGridColumnString);
    Vue.component('sdiKit-grid-column-boolean', SdiKitGridColumnBoolean);
    Vue.component('sdiKit-grid-column-datetime', SdiKitGridColumnDateTime);
    Vue.component('sdiKit-grid-filter', SdiKitGridFilter);
    Vue.component('sdiKit-grid-column-default-action-context-menu', SdiKitGridColumnDefaultActionContextMenu);
    Vue.component('sdiKit-grid-column-default-action', SdiKitGridColumnDefaultAction);
    Vue.component('sdiKit-grid', SdiKitGrid);

    Vue.component('sdiKit-widget', SdiKitWidget);
    Vue.component('sdiKit-page-grid', SdiKitPageGrid);

    Vue.component('markdown-editor', MarkDownEditor);
    Vue.component('v-jstree', VJstree);
    Vue.component('vue-ladda', VueLadda);
}
