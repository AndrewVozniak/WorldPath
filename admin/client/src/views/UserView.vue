<script setup>
import axios from 'axios';
import { ref, onMounted, computed } from 'vue';
import WarningButtonComponent from "@/components/WarningButtonComponent.vue";

// SEARCHING
const searchQuery = ref('');

const filteredUsers = computed(() => {
  if (!searchQuery.value) return users.value;

  return users.value.filter(user => {
    return user.name.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        user.email.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
        user.id.toString().includes(searchQuery.value);  // преобразование id в строку
  });
});

// SORTING
const sortBy = ref('name'); // по умолчанию сортировка по имени
const sortOrder = ref('asc'); // по умолчанию сортировка по возрастанию

const sortedUsers = computed(() => {
  return filteredUsers.value.sort((a, b) => {
    if (a[sortBy.value] < b[sortBy.value]) return sortOrder.value === 'asc' ? -1 : 1;
    if (a[sortBy.value] > b[sortBy.value]) return sortOrder.value === 'asc' ? 1 : -1;
    return 0;
  });
});

// Функция для изменения сортировки по колонке
const toggleSort = (column) => {
  if (sortBy.value === column) {
    sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc';
  } else {
    sortBy.value = column;
    sortOrder.value = 'asc';
  }
};


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

// pagination
const currentPage = ref(1);
const pageSize = ref(10);
const totalPages = computed(() => Math.ceil(sortedUsers.value.length / pageSize.value));

const paginatedUsers = computed(() => {
  const start = (currentPage.value - 1) * pageSize.value;
  const end = start + pageSize.value;
  return sortedUsers.value.slice(start, end);
});


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
          <th @click="toggleSort('id')">ID</th>
          <th @click="toggleSort('name')">Name</th>
          <th @click="toggleSort('email')">Email</th>
          <th @click="toggleSort('is_banned')">Banned</th>
          <th @click="toggleSort('is_warned')">Warned</th>
          <th @click="toggleSort('is_muted')">Muted</th>
          <th @click="toggleSort('is_verified')">Verified</th>
          <th @click="toggleSort('is_admin')">Admin</th>
          <th>Manage</th>
        </tr>
        </thead>

        <tbody class="table-body">
          <tr class="table-row" v-for="user in paginatedUsers" :key="user.id">
            <td>{{ user.id }}</td>
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

      <div class="pagination">
        <button @click="currentPage = Math.max(currentPage - 1, 1)" :disabled="currentPage === 1">Previous</button>
        <span>Page {{ currentPage }} of {{ totalPages }}</span>
        <button @click="currentPage = Math.min(currentPage + 1, totalPages)" :disabled="currentPage === totalPages">Next</button>
      </div>
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

        cursor: pointer;

        &:hover {
          background-color: #121a32;
        }
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

  .pagination {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 20px;

    button {
      padding: 10px 15px;
      margin: 0 10px;
      border: none;
      border-radius: 5px;
      background-color: #0d142a;
      color: #fff;
      cursor: pointer;
      transition: background-color 0.3s;

      &:disabled {
        background-color: #0d142a;
        cursor: not-allowed;
      }

      &:not(:disabled):hover {
        background-color: #121a32;
      }
    }

    span {
      margin: 0 10px;
    }
  }
}
</style>