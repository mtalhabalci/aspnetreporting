<template>
  <div>
    <b-form-group :label="label" v-bind="layout">
      <file-upload
        :name="name"
        :multiple="isMultiple"
        :accept="filter"
        :directory="false"
        :drop="false"
        :drop-directory="false"
        :add-index="false"
        :post-action="postAction"
        v-model="file"
        @input-filter="inputFilter"
        @input-file="inputFile"
        ref="upload"
        @vdropzone-success="vsuccess"
      />
    </b-form-group>
    <div class="table-responsive">
      <table class="table" v-if="file.length">
        <thead>
          <tr>
            <th>Adı</th>
            <th>Boyutu</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(f) in file" :key="f.id">
            <td>
              <div class="filename">{{f.name}}</div>
              <b-progress
                :value="Number(f.progress)"
                :variant="f.error ? 'danger' : ''"
                :animated="f.active"
                v-if="f.active || f.progress !== '0.00'"
                height="6px"
                style="margin: 4px 0 0 0"
              />
            </td>
            <td width="100px">{{ f.size/1024/1024 | fileSize }} MB</td>
            <td width="30px">
              <b-btn size="sm" variant="outline-danger" @click="$refs.upload.remove(f)">Sil</b-btn>
            </td>
          </tr>
        </tbody>
      </table>
      <label :for="name" class="btn btn-primary">Dosya Seç</label>
      <small class="form-text text-muted pl-1" v-if="$slots.info">
        <slot name="info"></slot>
      </small>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import vue2Dropzone from "vue2-dropzone";
import vueUpload from "vue-upload-component";

Vue.filter("fileSize", function(value) {
  if (!value) return "";
  const parts = String(value).split(".");
  return `${parts[0]}.${parts[1].slice(0, 2)}`;
});

export default {
  props: {
    value: {
      type: Array,
      default: function() {
        return [];
      }
    },
    name: {
      type: String,
      default: "",
      required: true
    },
    filter: {
      type: String,
      default: ""
    },
    isMultiple: {
      type: Boolean,
      default: false
    },
    label: {
      type: String,
      default: "Etiket Yok"
    },
    postAction: {
      type: String,
      default: window.baseURL + "/file/upload"
    },
    placeholder: {
      type: String
    },
    direction: {
      type: String,
      validator: val => ["vertical", "horizontal"].includes(val),
      default: "vertical"
    }
  },
  components: {
    "file-upload": vueUpload,
    vueDropzone: vue2Dropzone
  },
  data: function() {
    return {
      dropzoneOptions: {
        headers: { aa: "xx" }
      }
    };
  },
  methods: {
    inputFilter(newFile, oldFile, prevent) {
      if (newFile && !oldFile) {
        if (/(\/|^)(Thumbs\.db|desktop\.ini|\..+)$/.test(newFile.name)) {
          return prevent();
        }
        if (/\.(php5?|html?|jsx?)$/i.test(newFile.name)) {
          return prevent();
        }
      }
      if (newFile && (!oldFile || newFile.file !== oldFile.file)) {
        // Create a blob field
        newFile.blob = "";
        let URL = window.URL || window.webkitURL;
        if (URL && URL.createObjectURL) {
          newFile.blob = URL.createObjectURL(newFile.file);
        }
        // Thumbnails
        newFile.thumb = "";
        if (newFile.blob && newFile.type.substr(0, 6) === "image/") {
          newFile.thumb = newFile.blob;
        }
      }
    },
    inputFile(newFile, oldFile) {
      //newFile.active = true;
      this.$refs.upload.active = true;
      //$refs.upload.active = true;
      console.log(newFile);
      if (newFile && oldFile) {
        if (newFile.active && !oldFile.active) {
          if (
            newFile.size >= 0 &&
            this.minSize > 0 &&
            newFile.size < this.minSize
          ) {
            this.$refs.upload.update(newFile, { error: "size" });
          }
        }
      }
    },
    vsuccess(file, response) {
      console.log("test");
      this.success = true;
      // window.toastr.success('', 'Event : vdropzone-success')
    }
  },
  mounted() {
    var c = document.getElementsByClassName("file-uploads");
    for (let el = 0; el < c.length; el++) {
      c[el].parentElement.style.height = "0px";
    }
  },
  computed: {
    file: {
      get() {
        return this.value;
      },
      set(val) {
        this.$emit("input", val);
      }
    },
    multipleAttribute() {
      return this.isMultiple ? { multiple: "multiple" } : null;
    },
    layout() {
      return this.direction == "horizontal"
        ? {
            "label-align-md": "right",
            "label-cols-md": 2
          }
        : null;
    }
  }
};
</script>
<style scoped>
.form-group {
  margin-bottom: 0px;
}
</style>

