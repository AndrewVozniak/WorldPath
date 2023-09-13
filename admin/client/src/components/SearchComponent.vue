<script setup lang="ts">
import { Input} from "@/components/ui/input";
import {ref} from "vue";

const searchQuery = ref('');
</script>

<template>
  <div>
    <Input
        type="text"
        placeholder="Search..."
        class="w-full outline-0"
        @keydown.enter="console.log(searchQuery)"
        v-model="searchQuery"
    />
  </div>
</template>