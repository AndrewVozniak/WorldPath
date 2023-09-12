<script setup lang="ts">
import {Button} from "@/components/ui/button";


async function logout() {
  localStorage.removeItem('token');
  localStorage.removeItem('username');
  window.location.reload();
}
</script>

<template>
  <Button variant="destructive" @click="logout">LOGOUT</Button>
</template>
