<script setup>
</script>

<template>
  <button class="btn btn-primary">
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

.btn-primary {
  background-color: #121a32;
  color: #fff;

  transition: 0.2s ease-in;

  &:hover {
    background-color: #253158;
  }
}
</style>