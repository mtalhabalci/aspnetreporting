<template>
  <footer class="footer footer-transparent">
    <div class="container">
      <div class="row text-center align-items-center flex-row-reverse">
        <div class="col-lg-auto ml-lg-auto">
          <ul class="list-inline list-inline-dots mb-0">
            <li class="list-inline-item">
              <a href="#" class="link-secondary">Documentation</a>
            </li>
            <li class="list-inline-item">
              <a href="#" class="link-secondary">FAQ</a>
            </li>
          </ul>
        </div>
        <div class="col-12 col-lg-auto mt-3 mt-lg-0">
          Copyright © 2020
          <a
            href="https://admin.ecosim/"
            class="link-secondary"
            targer="_blank"
          >{{ app.project.company }}</a>
          .
          All rights reserved. - {{ app.project.version }}
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: "app-footer"
};
</script>
