<script setup>
import { RouterLink } from 'vue-router'

const pages = [
  {
    title: 'Dashboard',
    link: '/'
  },
  {
    title: 'Users',
    link: '/users'
  },
  {
    title: 'Community',
    link: '/community'
  },
  {
    title: 'Reviews',
    link: '/reviews'
  },
  {
    title: 'Places',
    link: '/places'
  },
  {
    title: 'Trips',
    link: '/trips'
  },
  {
    title: 'Routes',
    link: '/routes'
  },
  {
    title: 'Weather',
    link: '/weather'
  },
  {
    title: 'Support',
    link: '/support'
  }
]
</script>

<template>
  <div class="sidebar">
    <RouterLink to="/" class="sidebar__title">WorldPath Admin</RouterLink>

    <div class="sidebar__items">
      <RouterLink :to="page.link" class="sidebar__item" v-for="page in pages" :key="page.title">
        {{ page.title }}
      </RouterLink>
    </div>
  </div>
</template>

<style scoped lang="scss">
.sidebar {
  display: flex;
  flex-direction: column;

  height: 100vh;
  width: 100%;

  max-width: 330px;

  background-color: #121a32;

  padding: 15px 30px;

  text-align: center;

  .sidebar__title {
    color: #fff;
    font-size: 24px;
    font-weight: 600;

    white-space: nowrap;

    margin-bottom: 20px;
  }

  .sidebar__items {
    display: flex;
    flex-direction: column;
    justify-content: space-between;

    width: 100%;

    .sidebar__item {
      margin-bottom: 10px;

      padding: 7px;

      border-radius: 5px;

      background-color: #253158;

      cursor: pointer;
      transition: 0.2s ease-in;

      &:hover {
        background-color: #4d8d28;
        font-weight: 600;
        transform: scale(1.05);
      }
    }
  }
}
</style>