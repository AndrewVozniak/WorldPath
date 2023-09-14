<script setup lang="ts">
import { RouterView } from 'vue-router'
import {provide, ref} from "vue";
import AuthView from "@/views/AuthView.vue";
import HeaderComponent from "@/components/HeaderComponent.vue";

const isLogged = ref(!!localStorage.getItem('token'));
provide('isLogged', isLogged);

const themeStyles = ref('bg-black text-white dark-theme');
</script>

<template>
  <div :class="themeStyles"
       class="flex flex-col min-h-screen" v-if="isLogged">
    <HeaderComponent></HeaderComponent>

    <div class="flex mx-auto w-11/12 justify-start pt-10">
      <RouterView/>
    </div>
  </div>

  <div v-else>
    <AuthView @login="isLogged = true"></AuthView>
  </div>
</template>

<style scoped>

</style>
