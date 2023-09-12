<script setup>
import { RouterView } from 'vue-router'
import { ref, provide } from "vue";

import LoginView from "@/views/LoginView.vue";
import SidebarComponent from "@/components/SidebarComponent.vue";

const isLogged = ref(!!localStorage.getItem('token'));
provide('isLogged', isLogged);
</script>

<template>
  <div class="main-container" v-if="isLogged">
    <SidebarComponent></SidebarComponent>

    <div class="content">
      <RouterView/>
    </div>
  </div>

  <div class="main-container" v-else>
    <LoginView @login="isLogged = true"></LoginView>
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
