<template>
  <div class="container-fluid">
    <sdiKit-form
      :url="url"
      :sdi-kit-form-model="selected"
      :delete-item="deleteItem"
      :after-submit="afterSubmit"
      :show-delete-button="deleteButtonExists"
      :header="pageWidgetTitle"
      :parent-header="'pageConfig.parentHeader'"
      :header-description="'header-description'"
      :before-submit="beforeSubmit"
    >
      <router-link
        class="text-decoration-none"
        slot="parent-header"
        :to="{ name: 'person.list' }"
        >{{ "pageConfig.parentHeader" }}</router-link
      >

      <span slot="header" class="pageconfig-header">{{ pageWidgetTitle }}</span>
      <div slot="body">
          <div class="row">

        <sdiKit-textbox
          class="col-4"
          :label="'Name'"
          placeholder
          v-model="selected.name"
          :state="$v.selected.name"
        ></sdiKit-textbox>

        <sdiKit-textbox
          :label="'Surname'"
          placeholder
          v-model="selected.surname"
          class="col-4"
          :state="$v.selected.surname"
        ></sdiKit-textbox>
        <sdiKit-textbox
          :label="'Company'"
          class="col-4"
          placeholder
          v-model="selected.company"
          :state="$v.selected.company"
        ></sdiKit-textbox>
          </div>
      </div>
    </sdiKit-form>
  </div>
</template>
<script>
import { Ajax, Types } from "~/utils/index";
import Pages from "~/mixins/pages";
import PageConfig from "./pageConfig";
export default {
  mixins: [Pages, PageConfig],
  data() {
    return {
      id: this.$route.params.id,
      url: "/Person/save",
      cancelUrl: "/Person/list",
      afterSubmitUrl: "/home/index",
    };
  },
  beforeMount() {
    let self = this;
    if (self.$route.params.id) self.showLoaderContainer();
    Types.getViewModel(
      self.$enums.OutputEnums.CreateOrEditPersonInput,
      self,
      () => {
        self.getData();
      }
    );
  },

  mounted() {},
  methods: {
    read() {
      this.$router.push({ path: "/person/read/" + this.id });
    },
    afterSubmit() {
      this.$router.push({ path: this.afterSubmitUrl });
      return true;
    },
    afterDelete() {
      this.$router.push({ path: "/" });
      return true;
    },
    deleteItem() {
      Ajax.post("/Person/delete", this.id, this.afterDelete);
    },
    getData() {
      var self = this;
      if (this.id) {
        Ajax.get("/Person/" + this.id + "/get", function (response) {
          let result = response.result;
          Types.mapDtoBaseToVm(self, result);
          self.selected.name = result.name;
          self.selected.surname = result.surname;
          self.selected.company= result.company;
          self.isNewRecord = false;
          self.hideLoaderContainer();
        });
      } else self.hideLoaderContainer();
    },
    beforeSubmit() {
      return this.validate();
    },
  },
};
</script>
