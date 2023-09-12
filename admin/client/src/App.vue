<script setup lang="ts">
import { RouterView } from 'vue-router'
import {provide, ref} from "vue";
import SidebarComponent from "@/components/SidebarComponent.vue";
import AuthView from "@/views/AuthView.vue";

const isLogged = ref(!!localStorage.getItem('token'));
provide('isLogged', isLogged);
</script>

<template>
  <div class="flex justify-between" v-if="isLogged">
    <SidebarComponent></SidebarComponent>

    <div class="flex w-full justify-start">
      <RouterView />
    </div>
  </div>

  <div v-else>
    <AuthView @login="isLogged = true"></AuthView>
  </div>
</template>

<style scoped>
</style>
