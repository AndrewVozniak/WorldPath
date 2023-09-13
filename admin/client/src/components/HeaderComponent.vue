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
  <nav :class="cn('flex flex-col md:flex-row md:items-center md:space-x-4 lg:space-x-6 xl:space-x-8 px-5 pt-4 pb-3 border-b border-zinc-800', $attrs.class ?? '')">
    <div class="flex flex-col md:flex-row md:items-center md:space-x-4 lg:space-x-6 xl:space-x-8 order-1 md:order-none">
      <RouterLink :to="link.path" v-for="link in links" :key="link.name" class="mb-2 md:mb-0">
        <span :class="{
                     'underline underline-offset-4': currentPath === link.path,
                     'text-muted-foreground': currentPath != link.path
                   }"
              class="text-lg md:text-sm font-medium transition-colors hover:text-white">
          {{ link.name }}
        </span>
      </RouterLink>
    </div>

    <SearchComponent class="w-full mb-7 md:mb-0"></SearchComponent>
  </nav>
</template>