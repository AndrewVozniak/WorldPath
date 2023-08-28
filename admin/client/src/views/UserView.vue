<script setup>
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';
import WarningButtonComponent from "@/components/WarningButtonComponent.vue";


const searchQuery = ref('');  // Add this line

const filteredUsers = computed(() => {  // Add this computed property
  if (!searchQuery.value) return users.value;

  return users.value.filter(user => {
    return user.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        user.email.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        user.id.toString().includes(searchQuery.value);  // преобразование id в строку
  });
});


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


const manageUser = async(id) => {
  alert(id)
}
</script>

<template>
  <div class="page">
    <div class="content">
      <h1 class="title">{{ title }}</h1>

      <div class="search-box">
        <input type="text" v-model="searchQuery" placeholder="Search by name, email or id..." />
      </div>

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
          <th>Manage</th>
        </tr>
        </thead>

        <tbody class="table-body">
          <tr class="table-row" v-for="user in filteredUsers" :key="user.id">
            <td id="user_info">
              <img :src="user.profile_photo_path" :alt="user.name + 'avatar'">
              <span class="username">{{ user.name }}</span>
            </td>

            <td>{{ user.email }}</td>
            <td>{{ user.is_banned ? 'Yes' : 'No' }}</td>
            <td>{{ user.is_warned ? 'Yes' : 'No' }}</td>
            <td>{{ user.is_muted ? 'Yes' : 'No' }}</td>
            <td>{{ user.is_verified ? 'Yes' : 'No' }}</td>
            <td>{{ user.is_admin ? 'Yes' : 'No' }}</td>
            <td>
              <WarningButtonComponent @click="manageUser(user.id)">Manage</WarningButtonComponent>
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

  .search-box {
    margin: 20px 0;

    input {
      width: 100%;
      padding: 10px;
      border-radius: 5px;
      border: 1px solid #ccc;
      font-size: 16px;
      outline: none;
      transition: border 0.3s;
      color: #18213e;

      &:focus {
        border-color: #007BFF;
      }
    }
  }

  .table {
    border-collapse: collapse;

    .table-head {
      background-color: #0d142a;

      th:nth-child(1) {
        text-align: start;
      }

      th {
        font-size: 18px;
        padding: 10px;
        color: #fff;
      }
    }

    .table-body {
      border-radius: 5px;

      #user_info {
        display: flex;
        align-items: center;

        img {
          width: 40px;
          height: 40px;

          border-radius: 50%;
        }

        .username {
          margin-left: 10px;

        }
      }

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