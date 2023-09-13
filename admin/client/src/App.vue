<script setup lang="ts">
import { RouterView } from 'vue-router'
import {provide, ref} from "vue";
import AuthView from "@/views/AuthView.vue";
import HeaderComponent from "@/components/HeaderComponent.vue";

const isLogged = ref(!!localStorage.getItem('token'));
provide('isLogged', isLogged);
</script>

<template>
  <div class="bg-black text-white flex flex-col min-h-screen" v-if="isLogged">
    <HeaderComponent></HeaderComponent>

    <div class="flex w-full justify-start pt-5">
      <RouterView/>
    </div>
  </div>

  <div v-else>
    <AuthView @login="isLogged = true"></AuthView>
  </div>
</template>

<style scoped>

</style>
