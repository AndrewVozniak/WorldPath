<script setup>
import { ref } from 'vue'
import axios from "axios";
import PrimaryButtonComponent from "@/components/PrimaryButtonComponent.vue";

const username = ref('')
const password = ref('')

const domain = import.meta.env.VITE_DOMAIN
const emit = defineEmits();

const login = () => {
  // If devmode is enabled, we don't need to make a request to the server
  if(import.meta.env.VITE_DEV_MODE) {
    alert('You are in dev mode, you can login with any username and password')

    localStorage.setItem('username', username.value)
    localStorage.setItem('token', 'test')
    emit('login');

    // If devmode is disabled, we need to make a request to the server
  } else {
    // TODO create server at flask
    axios.post(`${domain}/api/auth/login`, {
      username: username.value,
      password: password.value
    }).then((res) => {
      localStorage.setItem('token', res.data.token)
      localStorage.setItem('username', username.value)
      emit('login');
    }).catch((err) => {
      console.log(err)
    })
  }
}
</script>

<template>
  <div class="login">
    <div class="login__container">
      <h1 class="login__title">WorldPath Admin</h1>

      <form class="login__form" @submit.prevent="login">
        <input type="text" class="login__input" placeholder="Username" v-model="username">
        <input type="password" class="login__input" placeholder="Password" v-model="password">

        <PrimaryButtonComponent class="login_button">Login</PrimaryButtonComponent>
      </form>
    </div>
  </div>
</template>

<style scoped lang="scss">
.login {
  margin-left: auto;
  margin-right: auto;

  padding-top: 30px;

  .login__container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    .login__title {
      margin-bottom: 25px;
    }

    .login__form {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;

      .login__input {
        margin-top: 10px;
        border-radius: 5px;

        padding: 10px 10px;

        background-color: #121a32;
        color: #70A74E;

        font-weight: 600;

        border: none;

        &:focus {
          outline: none;
        }
      }

      .login_button {
        margin-top: 20px;
      }
    }
  }
}
</style>