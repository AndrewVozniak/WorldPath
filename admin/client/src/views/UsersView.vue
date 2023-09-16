<script lang="ts">
import { h, ref, defineComponent } from 'vue';
import { ArrowUpDown } from 'lucide-vue-next'
import { RouterLink } from 'vue-router';

import TableComponent from '@/components/TableComponent.vue';
import axios from "axios";
import {Button} from "@/components/ui/button";

export default defineComponent({
  components: { TableComponent },
  setup() {
    const tableData = ref([]);
    const columns = [
      {
        accessorKey: 'id',
        header: 'ID',
        cell: ({ row }) => h('div', row.getValue('id')),
      },
      {
        accessorKey: 'avatar',
        header: 'Avatar',
        cell: ({ row }) => h('img', { src: row.getValue('avatar'), alt: 'User Avatar', class: 'rounded-full w-12' }),
      },
      {
        accessorKey: 'name',
        header: ({ column }) => {
          return h(Button, {
            variant: 'ghost',
            onClick: () => column.toggleSorting(column.getIsSorted() === 'asc'),
          }, () => ['Name', h(ArrowUpDown, { class: 'ml-2 h-4 w-4' })])
        },
        cell: ({ row }) => h('div', row.getValue('name')),
      },
      {
        accessorKey: 'email',
        header: ({ column }) => {
          return h(Button, {
            variant: 'ghost',
            onClick: () => column.toggleSorting(column.getIsSorted() === 'asc'),
          }, () => ['Email', h(ArrowUpDown, { class: 'ml-2 h-4 w-4' })])
        },
        cell: ({ row }) => h('div', { class: 'lowercase' }, row.getValue('email')),
      },
      {
        accessorKey: 'banned',
        header: 'Banned',
        cell: ({ row }) => h('div', row.getValue('banned')),
      },
      {
        accessorKey: 'warned',
        header: 'Warned',
        cell: ({ row }) => h('div', row.getValue('warned')),
      },
      {
        accessorKey: 'muted',
        header: 'Muted',
        cell: ({ row }) => h('div', row.getValue('muted')),
      },
      {
        accessorKey: 'verified',
        header: 'Verified',
        cell: ({ row }) => h('div', row.getValue('verified')),
      },
      {
        accessorKey: 'admin',
        header: 'Admin',
        cell: ({ row }) => h('div', row.getValue('admin')),
      },
      {
        accessorKey: 'updated_at',
        header: ({ column }) => {
          return h(Button, {
            variant: 'ghost',
            onClick: () => column.toggleSorting(column.getIsSorted() === 'asc'),
          }, () => ['Updated', h(ArrowUpDown, { class: 'ml-2 h-4 w-4' })])
        },
        cell: ({ row }) => h('div', row.getValue('updated_at')),
      },
      {
        accessorKey: 'created_at',
        header: ({ column }) => {
          return h(Button, {
            variant: 'ghost',
            onClick: () => column.toggleSorting(column.getIsSorted() === 'asc'),
          }, () => ['Created', h(ArrowUpDown, { class: 'ml-2 h-4 w-4' })])
        },
        cell: ({ row }) => h('div', row.getValue('created_at')),
      },
      {
        accessorKey: 'action',
        header: 'Action',
        cell: ({ row }) => h(RouterLink, { to: `/users/${row.getValue('id')}`, class: 'manage-button' }, row.getValue('action')),
      }
    ];

    const fetchData = async () => {
      try {
        const response = await axios.get(`${import.meta.env.VITE_DOMAIN}/user/get_all_users`, {
          headers: {
            'Authorization': localStorage.getItem('token')
          }
        });

        const users = response.data.map((user: any) => ({
          id: user.id,
          avatar: user.profile_photo_path,
          name: user.name,
          email: user.email,
          banned: user.is_banned,
          warned: user.is_warned,
          muted: user.is_muted,
          verified: user.is_verified,
          admin: user.is_admin,
          updated_at: user.updated_at,
          created_at: user.created_at,
          action: "Manage"
        }));

        tableData.value = users;
      } catch (error) {
        console.log(error);
      }
    };

    fetchData();

    return { tableData, columns };
  }
});
</script>

<template>
  <div class="w-full">
    <h1 class="text-2xl font-black mb-5">Users</h1>

    <TableComponent  :columns="columns" :data="tableData" :search-by="'email'" v-if="tableData[0]"/>
    <TableComponent :columns="columns" :data="tableData" :search-by="'email'" v-else/>
  </div>
</template>