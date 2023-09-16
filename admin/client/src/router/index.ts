import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import UsersView from '../views/UsersView.vue'
import ReviewsView from "../views/ReviewsView.vue";
import TravelsView from "../views/TravelsView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/users',
      name: 'users',
      component: UsersView
    },
    {
      path: '/reviews',
      name: 'reviews',
      component: ReviewsView
    },
    {
        path: '/travels',
        name: 'travels',
        component: TravelsView
    }
  ]
})

export default router
