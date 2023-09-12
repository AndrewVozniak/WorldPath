<script setup>
import {inject} from "vue";

const isLogged = inject('isLogged');

const logout = () => {
  localStorage.removeItem('token');
  localStorage.removeItem('username');
  isLogged.value = false;
}
</script>

<template>
  <button class="btn btn-danger" @click="logout">
    <slot></slot>
  </button>
</template>

<style scoped>
.btn {
  padding: 10px 20px;
  border-radius: 5px;
  border: none;
  cursor: pointer;
  font-weight: bold;
  font-size: 16px;

  width: 100%;
}

.btn-danger {
  background-color: #e35151;
  color: #fff;

  transition: 0.2s ease-in;

  &:hover {
    background-color: #ef4a4a;
  }
}
</style>