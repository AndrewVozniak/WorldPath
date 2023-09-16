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
        accessorKey: 'rating',
        header: ({ column }) => {
          return h(Button, {
            variant: 'ghost',
            onClick: () => column.toggleSorting(column.getIsSorted() === 'asc'),
          }, () => ['Rating', h(ArrowUpDown, { class: 'ml-2 h-4 w-4' })])
        },
        cell: ({ row }) => h('div', row.getValue('rating')),
      },
      {
        accessorKey: 'text',
        header: 'Text',
        cell: ({ row }) => h('div', row.getValue('text')),
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
        cell: ({ row }) => h(RouterLink, { to: `/reviews/${row.getValue('id')}`, class: 'manage-button' }, row.getValue('action')),
      }
    ];

    const fetchData = async () => {
      try {
        const response = await axios.get(`${import.meta.env.VITE_DOMAIN}/reviews/reviews`, {
          headers: {
            'Authorization': localStorage.getItem('token')
          }
        });

        const reviews = response.data.map((review: any) => ({
          id: review.id,
          text: review.text,
          rating: review.rating,
          updated_at: review.updated_at,
          created_at: review.created_at,
          action: "Manage"
        }));

        tableData.value = reviews;
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
    <h1 class="text-2xl font-black mb-5">Reviews</h1>

    <TableComponent :columns="columns" :data="tableData" :search-by="'id'" v-if="tableData[0]"/>
    <TableComponent :columns="columns" :data="tableData" :search-by="'id'" v-else/>
  </div>
</template>