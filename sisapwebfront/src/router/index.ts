import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Home from '../views/Home.vue';
import Login from '../views/Login.vue';
import Reports from '../views/reports/Reports.vue';
import moment from 'moment';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Login',
    component: Login,
  },
  {
    path: '/Home',
    name: 'Home',
    component: Home,
    meta: { requiresAuth: true }
  },
  { path: '/reports', 
    component: Reports 
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});



function isTokenValid(): boolean {
  const tokenEncoded: any = window.localStorage.getItem('token');
  const auth = tokenEncoded != null ? JSON.parse(atob(tokenEncoded)) : '';
  return tokenEncoded && moment(new Date()).isSameOrBefore(auth.exp);
}

router.beforeEach((to, from, next) => {
  if (to.path === '/') {
    if (isTokenValid()) {
      // TODO:  buscar as features admin
      next({
        path: '/Home',
        query: { redirect: to.fullPath }
      });
    } else {
      next();
    }
  } else if (to.matched.some(record => record.meta.requiresAuth) && isTokenValid()) {
    next();
    // } else if (to.path === '/changepassword') {
    //   next();
    // } else {
  } else {
    window.localStorage.removeItem('token');
    next({
      path: '/',
      query: { redirect: to.fullPath }
    });
  }
});

export default router;
