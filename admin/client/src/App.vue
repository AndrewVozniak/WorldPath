<script setup>
import { RouterView } from 'vue-router'
import SidebarComponent from "@/components/SidebarComponent.vue";
</script>

<template>
  <div class="main-container">
    <SidebarComponent></SidebarComponent>

    <div class="content">
      <RouterView />
    </div>
  </div>

</template>

<style scoped>
.main-container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.content {
  width: 90%;

  padding-top: 15px;
  padding-left: 30px;
}
</style>
