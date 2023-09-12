<script setup lang="ts">
import {ref} from "vue";
import Button from "@/components/ui/button/Button.vue";

const username = ref(localStorage.getItem('username'));

const links = [
  { name: 'Dashboard', path: '/' },
  { name: 'Users', path: '/users' },
  { name: 'Community', path: '/community' },
  { name: 'Reviews', path: '/reviews' },
  { name: 'Places', path: '/places' },
  { name: 'Trips', path: '/trips' },
  { name: 'Routes', path: '/routes' },
  { name: 'Weather', path: '/weather' },
  { name: 'Support', path: '/support' },
];
</script>

<template>
  <div style="height: 100vh" class="flex flex-col w-1/5 bg-gray-900 px-10 pt-5">
    <h1 class="text-center text-white text-2xl font-semibold">Hi {{ username }}!</h1>
    <h2 class="text-center text-white font-semibold">Welcome to the admin panel</h2>

    <div class="flex flex-col mt-10">
      <RouterLink :to="link.path" v-for="link in links" :key="link.name">
        <Button variant="secondary" class="w-full mb-5 text-md font-semibold">{{ link.name }}</Button>
      </RouterLink>
    </div>
  </div>
</template>

<style scoped>

</style>