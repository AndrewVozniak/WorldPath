<script setup lang="ts">

import { cn } from '@/lib/utils'
import { buttonVariants } from '@/components/ui/button'
import UserAuthForm from "@/components/UserAuthForm.vue";

const emit = defineEmits();

async function login() {
  emit('login');
}
</script>

<template>
  <div style="height: 100vh" class="container relative flex-col items-center justify-center md:grid lg:max-w-none lg:grid-cols-2 lg:px-0">
    <a
        href="/login"
        :class="cn(
        buttonVariants({ variant: 'ghost' }),
        'absolute right-4 top-4 md:right-8 md:top-8 hidden md:flex items-center',
      )"
    >
      Login
    </a>
    <div class="relative hidden h-full flex-col bg-muted p-10 text-white dark:border-r lg:flex">
      <div class="absolute inset-0 bg-zinc-900" />
      <div class="relative z-20 flex items-center text-lg font-medium">
        <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
            class="mr-2 h-6 w-6"
        >
          <path d="M15 6v12a3 3 0 1 0 3-3H6a3 3 0 1 0 3 3V6a3 3 0 1 0-3 3h12a3 3 0 1 0-3-3" />
        </svg>
        WorldPath Company
      </div>
      <div class="relative z-20 mt-auto">
        <blockquote class="space-y-2">
          <p class="text-lg">&ldquo;Travel with no confidence.&rdquo;</p>
          <footer class="text-sm">Andrew Vozniak</footer>
        </blockquote>
      </div>
    </div>
    <div class="lg:p-8">
      <div class="mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px]">
        <div class="flex flex-col space-y-2 text-center">
          <h1 class="text-2xl font-semibold tracking-tight">Sign In</h1>
          <p class="text-sm text-muted-foreground">Enter your email below to enter in your account</p>
        </div>

        <UserAuthForm @login="login"/>

        <p class="px-8 text-center text-sm text-muted-foreground">
          By clicking continue, you agree to WorldPath
          <span class="underline underline-offset-4">Terms of Service</span>
          and
          <span class="underline underline-offset-4">Privacy Policy</span>.
        </p>
      </div>
    </div>
  </div>
</template>