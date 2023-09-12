<script setup lang="ts">
import { ref } from 'vue'
import axios from "axios";

import { cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Alert, AlertDescription, AlertTitle } from '@/components/ui/alert'

const isLoading = ref(false)

const email = ref('')
const password = ref('')

const error = ref('')

const emit = defineEmits();

async function onSubmit(event: Event) {
  event.preventDefault();

  axios.post(`${import.meta.env.VITE_DOMAIN}/user/sign_in_by_email`, {
    email: email.value,
    password: password.value
  }).then((res) => {
    if(res.data.error) {
      error.value = res.data.error
      return
    }

    localStorage.setItem('token', res.data.token)
    localStorage.setItem('username', res.data.username)
    emit('login');
  }).catch((err) => {
    console.log(err)
  })
}

</script>

<template>
  <div :class="cn('grid gap-6', $attrs.class ?? '')">
    <form @submit="onSubmit">
      <div class="grid gap-2">
        <div v-if="error">
          <Alert>
            <AlertTitle>Ooops!</AlertTitle>
            <AlertDescription>
              {{ error }}
            </AlertDescription>
          </Alert>
        </div>

        <div class="grid gap-1">
          <Label class="sr-only" for="email">
            Email
          </Label>
          <Input
              id="email"
              placeholder="name@example.com"
              type="email"
              auto-capitalize="none"
              auto-complete="email"
              auto-correct="off"
              :disabled="isLoading"
              v-model="email"
              required
          />
        </div>

        <div class="grid gap-1">
          <Label class="sr-only" for="password">
            Password
          </Label>
          <Input
              id="password"
              placeholder="I dont know..."
              type="password"
              auto-capitalize="none"
              auto-complete="password"
              auto-correct="off"
              :disabled="isLoading"
              v-model="password"
              required
          />
        </div>
        <Button :disabled="isLoading">
          Sign In with Email
        </Button>
      </div>
    </form>
    <div class="relative">
      <div class="absolute inset-0 flex items-center">
        <span class="w-full border-t" />
      </div>
      <div class="relative flex justify-center text-xs uppercase">
        <span class="bg-background px-2 text-muted-foreground">
          Or continue with
        </span>
      </div>
    </div>
    <Button variant="outline" type="button" :disabled="isLoading">
      GitHub
    </Button>
  </div>
</template>