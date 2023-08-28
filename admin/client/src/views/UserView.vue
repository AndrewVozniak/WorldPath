<script setup>
import axios from 'axios';
import {ref} from "vue";

let title = 'Users';
let users = ref([]);

axios.defaults.headers.common['Authorization'] = localStorage.getItem('token');
axios.get('http://localhost:3000/user/get_all_users')
  .then(res => {
    users = res.data;
    console.log(users);
  })
  .catch(err => {
    console.log(err);
  });
</script>

<template>
  <div class="page">
    <h1 class="title">{{ title }}</h1>

    <div class="content">
      <div class="rows">
        <div class="row" v-for="user in users">
          <div class="section">{{ user.name }}</div>
          <div class="section">{{ user.email }}</div>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>

</style>