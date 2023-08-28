<script setup>
import axios from 'axios';
import { ref, onMounted } from 'vue';
import WarningButtonComponent from "@/components/WarningButtonComponent.vue";

const getUsers = async () => {
  try {
    const response = await axios.get('http://localhost:3000/user/get_all_users', {
      headers: {
        'Authorization': localStorage.getItem('token')
      }
    });
    users.value = response.data;

    console.log(users.value);
  } catch (error) {
    console.log(error);
  }
};

const title = 'Users';
const users = ref([]);

onMounted(getUsers);
</script>

<template>
  <div class="page">
    <div class="content">
      <h1 class="title">{{ title }}</h1>

      <table class="table">
        <thead class="table-head">
        <tr class="table-row">
          <th>Name</th>
          <th>Email</th>
          <th>Banned</th>
          <th>Warned</th>
          <th>Muted</th>
          <th>Verified</th>
          <th>Admin</th>
          <th>Details</th>
        </tr>
        </thead>

        <tbody class="table-body">
        <tr class="table-row"
            v-for="user in users"
            :key="user.id">
          <td>{{ user.name }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.is_banned ? 'Yes' : 'No' }}</td>
          <td>{{ user.is_warned ? 'Yes' : 'No' }}</td>
          <td>{{ user.is_muted ? 'Yes' : 'No' }}</td>
          <td>{{ user.is_verified ? 'Yes' : 'No' }}</td>
          <td>{{ user.is_admin ? 'Yes' : 'No' }}</td>
          <td>
            <RouterLink :to="'/user/' + user.id">
              <WarningButtonComponent>View</WarningButtonComponent>
            </RouterLink>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped lang="scss">
.content {
  width: 95%;

  margin-left: auto;
  margin-right: auto;

  .table {
    border-collapse: collapse;

    .table-head {
      background-color: #0d142a;

      th {
        font-size: 18px;
        padding: 10px;
        color: #fff;
      }
    }

    .table-body {
      border-radius: 5px;

      .table-row {
        text-align: center;

        td {
          padding: 10px;
        }

        &:nth-child(odd) {
          background-color: #121a32;
        }

        &:nth-child(even) {
          background-color: #0d142a;
        }
      }
    }
  }
}
</style>