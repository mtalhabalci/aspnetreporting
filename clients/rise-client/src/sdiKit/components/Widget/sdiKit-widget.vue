<template>
  <div class="row">
    <div class="col-12">
      <b-card no-body header-tag="header" v-bind:class="[{ 'has-dataGrid': hasDataGrid }, 'mb-4']">
        <b-card-header v-if="header" class="pt-2 pb-2 pl-3 pr-2">
          <div class="row">
            <div class="col-md pt-1 pl-2">
              <h3 class="card-title">{{header}}</h3>
            </div>
            <div class="col-md text-right">
              <slot name="header">
                <b-btn
                  v-if="topRightButtonExists"
                  @click="rightButtonOnClick"
                  variant="outline-secondary"
                  size="sm"
                  class="sdiKit filter-button"
                >
                  <i :class="topRightButtonIcon"></i>&nbsp;&nbsp; {{topRightButtonText}}
                </b-btn>
              </slot>
            </div>
            <div class="col-12 text-muted small my-1 pt-1 pl-2" v-if="description">{{description}}</div>
          </div>
        </b-card-header>
        <b-card-body>
          <slot name="body"></slot>
        </b-card-body>
        <b-card-footer v-if="$slots.footer" class="p-2">
          <slot name="footer"></slot>
        </b-card-footer>
      </b-card>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    header: {
      type: String,
      default: ""
    },
    description: {
      type: String,
      default: ""
    },
    hasDataGrid: {
      type: Boolean,
      default: false
    },
    topRightButtonExists: {
      type: Boolean,
      default: false
    },
    rightButtonOnClick:{
      type:Function,
      default:()=>{}
    },
    topRightButtonIcon:{
      type:String,
      default:"ion ion-ios-arrow-back"
    },
    topRightButtonText:{
      type:String,
      default:"Geri"
    }
  },
  methods: {
    
  }
};
</script>
