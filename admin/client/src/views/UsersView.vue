<script setup lang="ts">
import type {
  ColumnDef,
  ColumnFiltersState,
  SortingState,
  VisibilityState,
} from '@tanstack/vue-table'
import {
  FlexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useVueTable,
} from '@tanstack/vue-table'
import { ArrowUpDown, ChevronDown } from 'lucide-vue-next'

import { h, ref } from 'vue'
import { Button } from '@/components/ui/button'
import { DropdownMenu, DropdownMenuCheckboxItem, DropdownMenuContent, DropdownMenuTrigger } from '@/components/ui/dropdown-menu'
import { Input } from '@/components/ui/input'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import { valueUpdater } from '@/lib/utils'
import axios from "axios";
import { RouterLink } from 'vue-router';


const table = ref(null);

export interface User {
  id: string,
  avatar: string,
  name: string,
  email: string,
  banned: boolean,
  warned: boolean,
  muted: boolean,
  verified: boolean,
  admin: boolean,
  action: string
}

let tableData = ref<User[]>([]);

const getUsers = async () => {
  try {
    const response = await axios.get(`${import.meta.env.VITE_DOMAIN}/user/get_all_users`, {
      headers: {
        'Authorization': localStorage.getItem('token')
      }
    });

    const users: User[] = response.data.map((user: any) => ({
      id: user.id,
      avatar: user.profile_photo_path,
      name: user.name,
      email: user.email,
      banned: user.is_banned,
      warned: user.is_warned,
      muted: user.is_muted,
      verified: user.is_verified,
      admin: user.is_admin,
      action: "Manage"
    }));

    return users;
  } catch (error) {
    console.log(error);
  }
};


getUsers().then(response => {
  tableData.value = JSON.parse(JSON.stringify(response));

  const columns: ColumnDef<User>[] = [
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
      accessorKey: 'action',
      header: 'Action',
      cell: ({ row }) => h(RouterLink, { to: `/users/${row.getValue('id')}`, class: 'manage-button' }, row.getValue('action')),
    }
  ]

  const sorting = ref<SortingState>([])
  const columnFilters = ref<ColumnFiltersState>([])
  const columnVisibility = ref<VisibilityState>({})
  const rowSelection = ref({})

  table.value = useVueTable({
    data: tableData.value,
    columns,
    getCoreRowModel: getCoreRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    onSortingChange: updaterOrValue => valueUpdater(updaterOrValue, sorting),
    onColumnFiltersChange: updaterOrValue => valueUpdater(updaterOrValue, columnFilters),
    onColumnVisibilityChange: updaterOrValue => valueUpdater(updaterOrValue, columnVisibility),
    onRowSelectionChange: updaterOrValue => valueUpdater(updaterOrValue, rowSelection),
    state: {
      get sorting() { return sorting.value },
      get columnFilters() { return columnFilters.value },
      get columnVisibility() { return columnVisibility.value },
      get rowSelection() { return rowSelection.value },
    },
  })
});
</script>

<template>
  <div class="w-full">
    <h1 class="text-2xl font-black">Users</h1>

    <div class="flex gap-2 items-center py-4">
      <Input
          class="max-w-sm"
          placeholder="Filter emails..."
          :model-value="table.getColumn('email').getFilterValue() as string"
          @update:model-value="table.getColumn('email').setFilterValue($event)"
      />
      <DropdownMenu>
        <DropdownMenuTrigger as-child>
          <Button variant="outline" class="ml-auto">
            Columns <ChevronDown class="ml-2 h-4 w-4" />
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end">
          <DropdownMenuCheckboxItem
              v-for="column in table.getAllColumns().filter((column) => column.getCanHide())"
              :key="column.id"
              class="capitalize"
              :checked="column.getIsVisible()"
              @update:checked="(value) => {
              column.toggleVisibility(!!value)
            }"
          >
            {{ column.id }}
          </DropdownMenuCheckboxItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </div>

    <div class="rounded-md border">
      <Table>
        <TableHeader>
          <TableRow v-for="headerGroup in table.getHeaderGroups()" :key="headerGroup.id">
            <TableHead v-for="header in headerGroup.headers" :key="header.id">
              <FlexRender v-if="!header.isPlaceholder" :render="header.column.columnDef.header" :props="header.getContext()" />
            </TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          <template v-if="table.getRowModel().rows?.length">
            <TableRow
                v-for="row in table.getRowModel().rows"
                :key="row.id">
              <TableCell v-for="cell in row.getVisibleCells()" :key="cell.id">
                <FlexRender :render="cell.column.columnDef.cell" :props="cell.getContext()" />
              </TableCell>
            </TableRow>
          </template>

          <TableRow v-else>
            <TableCell
                col-span="{{ table.getAllColumns().length }}"
                class="h-24 text-center"
            >
              No results.
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </div>

    <div class="flex items-center justify-end space-x-2 py-4">
      <div class="flex-1 text-sm text-muted-foreground">
        {{ table.getFilteredSelectedRowModel().rows.length }} of
        {{ table.getFilteredRowModel().rows.length }} row(s) selected.
      </div>
      <div class="space-x-2">
        <Button
            variant="outline"
            size="sm"
            :disabled="!table.getCanPreviousPage()"
            @click="table.previousPage()"
        >
          Previous
        </Button>
        <Button
            variant="outline"
            size="sm"
            :disabled="!table.getCanNextPage()"
            @click="table.nextPage()"
        >
          Next
        </Button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.manage-button {
  padding: 10px 20px;
  background-color: #d7bb1f;
  border-radius: 5px;
  font-weight: 600;

  color: #fff;
  text-decoration: none;

  transition: background-color 0.2s ease-in-out;
}

.manage-button:hover {
  background-color: #c7a11a;
}
</style>