<script setup lang="ts">
import {ref} from "vue";
import Button from "@/components/ui/button/Button.vue";

const username = ref(localStorage.getItem('username'));
</script>

<template>
  <div style="height: 100vh" class="flex flex-col w-1/5 bg-gray-900 px-10 pt-5">
    <h1 class="text-center text-white text-2xl font-semibold">Hi {{ username }}!</h1>
    <h2 class="text-center text-white font-semibold">Welcome to the admin panel</h2>

    <div class="flex flex-col mt-10">
      <RouterLink to="/"><Button variant="secondary" class="w-full mb-5">Dashboard</Button></RouterLink>
      <RouterLink to="/users"><Button variant="secondary" class="w-full mb-5">Users</Button></RouterLink>
      <RouterLink to="/community"><Button variant="secondary" class="w-full mb-5">Community</Button></RouterLink>
      <RouterLink to="/reviews"><Button variant="secondary" class="w-full mb-5">Reviews</Button></RouterLink>
      <RouterLink to="/places"><Button variant="secondary" class="w-full mb-5">Places</Button></RouterLink>
      <RouterLink to="/trips"><Button variant="secondary" class="w-full mb-5">Trips</Button></RouterLink>
      <RouterLink to="/routes"><Button variant="secondary" class="w-full mb-5">Routes</Button></RouterLink>
      <RouterLink to="/weather"><Button variant="secondary" class="w-full mb-5">Weather</Button></RouterLink>
      <RouterLink to="/support"><Button variant="secondary" class="w-full mb-5">Support</Button></RouterLink>
    </div>
  </div>
</template>

<style scoped>

</style>