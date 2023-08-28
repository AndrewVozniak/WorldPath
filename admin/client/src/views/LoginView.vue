<script setup>
import { ref } from 'vue'
import axios from "axios";
import PrimaryButtonComponent from "@/components/PrimaryButtonComponent.vue";

const username = ref('')
const password = ref('')

const error = ref('')

const emit = defineEmits();

const login = () => {
  // If devmode is enabled, we don't need to make a request to the server
  if(import.meta.env.VITE_DEV_MODE === 'true') {
    alert('You are in dev mode, you can login with any username and password')

    localStorage.setItem('username', username.value)
    localStorage.setItem('token', 'test')
    emit('login');

    // If devmode is disabled, we need to make a request to the server
  } else {
    axios.post(`${import.meta.env.VITE_DOMAIN}/user/sign_in_by_username`, {
      username: username.value,
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
}
</script>

<template>
  <div class="login">
    <div class="login__container">
      <h1 class="login__title">WorldPath Admin</h1>

      <h2 class="error__text">{{ error }}</h2>

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
      margin-bottom: 5px;
    }

    .error__text {
      color: #e35151;
      font-weight: 600;
      font-size: 16px;
      margin-bottom: 10px;
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