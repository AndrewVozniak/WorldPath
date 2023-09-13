<script setup lang="ts">
import {Button} from "@/components/ui/button";


async function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('username');
  window.location.reload();
}
</script>

<template>
    <div class="flex flex-col items-center text-white">
    <h1 class="text-4xl font-semibold mb-5">Welcome to the admin panel</h1>
    <h2 class="text-2xl font-semibold mb-5">Please login to continue</h2>

    <Button variant="destructive" @click="logout">LOGOUT</Button>
  </div>
</template>
