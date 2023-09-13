<script setup lang="ts">
import { cn } from '@/lib/utils'
import { ref, computed } from "vue";
import { useRoute } from 'vue-router';
import SearchComponent from "@/components/SearchComponent.vue";

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

const route = useRoute();
const currentPath = computed(() => route.path);
</script>

<template>
  <nav :class="cn('flex flex-col md:flex-row md:items-center md:space-x-4 lg:space-x-8 pt-4 pl-5 pb-2 border-b border-zinc-800', $attrs.class ?? '')">
    <RouterLink :to="link.path" v-for="link in links" :key="link.name">
      <span :class="{'text-muted-foreground': currentPath != link.path}"
              class="text-md font-medium transition-colors hover:text-white">{{ link.name }}</span>
    </RouterLink>

    <SearchComponent></SearchComponent>
  </nav>
</template>