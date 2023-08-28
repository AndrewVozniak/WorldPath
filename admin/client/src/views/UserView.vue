<script setup>
import axios from 'axios'
import { useRoute } from 'vue-router';
import {ref} from "vue";

const route = useRoute();
const user_id = route.params.id;

let user = ref()

const getUser = async () => {
  const response = await axios.get(`${import.meta.env.VITE_DOMAIN}/user/user/${user_id}`);

  user.value = response.data;
}

getUser();
</script>

<template>
  <div class="page">
    <div class="content">
      <h1 class="title">{{ user.name }}</h1>
      <img :src="user.profile_photo_path" :alt="user.profile_photo_path">

    </div>
  </div>
</template>

<style scoped lang="scss">
.content {
  width: 95%;

  margin-left: auto;
  margin-right: auto;


}
</style>