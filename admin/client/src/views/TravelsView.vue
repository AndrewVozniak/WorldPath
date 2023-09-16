<script lang="ts">
import {defineComponent, h, ref} from 'vue';
import {ArrowUpDown} from 'lucide-vue-next'
import {RouterLink} from 'vue-router';

import TableComponent from '@/components/TableComponent.vue';
import axios from "axios";
import {Button} from "@/components/ui/button";

export default defineComponent({
  components: { TableComponent },
  setup() {
    const tableData = ref([]);
    let isLoading = ref(false);

    const columns = [
      {
        accessorKey: 'id',
        header: 'ID',
        cell: ({ row }) => h('div', row.getValue('id')),
      },
      {
        accessorKey: 'title',
        header: 'Title',
        cell: ({ row }) => h('div', row.getValue('title')),
      },
      {
        accessorKey: 'description',
        header: 'Description',
        cell: ({ row }) => h('div', row.getValue('description')),
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
        cell: ({ row }) => h(RouterLink, { to: `/travels/${row.getValue('id')}`, class: 'manage-button' }, row.getValue('action')),
      }
    ];

    const fetchData = async () => {
      isLoading.value = true;

      try {
        const response = await axios.get(`${import.meta.env.VITE_DOMAIN}/travels/travel_service/travels`, {
          headers: {
            'Authorization': localStorage.getItem('token')
          }
        });
        isLoading.value = false;

        tableData.value = response.data.map((travel: any) => ({
          id: travel.id,
          title: travel.title,
          description: travel.description,
          type: travel.type,
          updated_at: travel.updated_at,
          created_at: travel.created_at,
          action: "Manage"
        }));
      } catch (error) {
        isLoading.value = false;
        console.log(error);
      }
    };

    fetchData();

    return { tableData, columns, isLoading };
  }
});
</script>

<template>
  <div class="w-full">
    <h1 class="text-2xl font-black mb-5">Travels</h1>

    <TableComponent :columns="columns" :data="tableData" :search-by="'title'" v-if="tableData[0]"/>
    <TableComponent :columns="columns" :data="tableData" :search-by="'title'" :is-loading="isLoading" v-else/>
  </div>
</template>