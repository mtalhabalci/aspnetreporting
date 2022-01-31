<template>
  <div id="app" class="page">
    <router-view />

    <notifications
      group="notifications-default"
      :duration="5000"
      animation-name="v-fade-right"
      position="bottom right"
    >
      <template slot="body" slot-scope="props">
        <div class="bs4-toast toast show mt-1" @click="props.close">
          <div :class="props.item.type + ' toast-header text-white'">
            <div class="mr-auto">{{ props.item.title }}</div>
            <button type="button" class="close d-block mb-1 ml-2">×</button>
          </div>
          <div class="toast-body">{{ props.item.text }}</div>
        </div>
      </template>
    </notifications>

    <b-modal id="exceptionModal" ref="exceptionModal" title="Bir Hata Oluştu" ok-only size="lg">
      <table class="table table-sm table-striped table-hover table-borderless small text-left m-0">
        <tbody>
          <tr>
            <th style="width: 120px;">
              <b>TraceId:</b>
            </th>
            <td>{{errorId}}</td>
          </tr>
          <tr>
            <th>
              <b>Error Code:</b>
            </th>
            <td>{{errorCode}}</td>
          </tr>
          <tr>
            <th>
              <b>Error Message:</b>
            </th>
            <td>
              <div style="overflow-y: auto; max-height:200px; max-width:600px;">
                <code>{{errorMessage}}</code>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </b-modal>
  </div>
</template>

<script>
export default {
  name: "app",
  metaInfo: {
    title: "Index"
  },

  data: () => ({
    errorMessage: "",
    errorCode: "",
    errorId: "",
    errorDate: null
  })
};
</script>

<style src="./style.scss" lang="scss"></style>
