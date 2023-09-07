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

  if(user.value.email_verified_at == null) {
    user.value.email_verified_at = 'Not verified';
  }
}

getUser();
</script>

<template>
  <div class="page">
    <div class="content" v-if="user">
      <RouterLink to="/users" class="page_link">To all users...</RouterLink>

      <h1 class="title">{{ user.name }}</h1>

      <div class="user__top__info">
        <img :src="user.profile_photo_path" :alt="user.name" class="user-photo">

        <div class="user__info__data">
          <span><b>Email verified at:</b> {{ user.email_verified_at }}</span>
          <span><b>Updated at:</b> {{ user.updated_at }}</span>
          <span><b>Created at:</b> {{ user.created_at }}</span>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.content {
  width: 95%;

  margin-left: auto;
  margin-right: auto;

  .page_link {
    color: #ec971f;
    margin-bottom: 20px;

    transition: all 0.3s;

    &:hover {
      color: #f8ad43;
    }
  }

  .user__top__info {
    display: flex;
    align-items: start;
    justify-content: space-between;

    margin-top: 20px;

    .user-photo {
      width: 150px;
      height: 150px;
      border-radius: 50%;
    }

    .user__info__data {
      display: flex;
      flex-direction: column;

      text-align: end;

      b {
        font-weight: 600;
      }

      span {
        margin-bottom: 10px;
      }
    }
  }
}
</style>