<script setup>
let title = 'WorldPath Dashbaord';
</script>

<template>
  <div class="homepage">
    <h1 class="title">{{ title }}</h1>
  </div>
</template>

<style>
.homepage {
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.title {
  font-size: 2rem;
  font-weight: 700;
  margin-bottom: 1rem;
}
</style>