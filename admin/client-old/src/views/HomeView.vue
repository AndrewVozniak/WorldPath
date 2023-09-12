<script setup>
import LogOutButtonComponent from "@/components/LogOutButtonComponent.vue";

let title = 'Dashbord';
let username = localStorage.getItem('username');
</script>

<template>
  <div class="page">
    <h1 class="title">{{ title }}</h1>

    <div class="content">
      <div class="entry">
        <h3 class="subtitle">Hi {{ username }}</h3>

        <LogOutButtonComponent>Log Out</LogOutButtonComponent>
      </div>
    </div>
  </div>
</template>

<style lang="scss">
.page {
  display: flex;
  flex-direction: column;
  align-items: start;

  .title {
    margin-bottom: 10px;
  }

  .content {
    display: flex;
    flex-direction: column;

    .entry {
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: center;

      .subtitle {
        white-space: nowrap;
        margin-right: 10px;
      }
    }
  }
}
</style>