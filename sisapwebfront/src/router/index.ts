import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Home from '../views/Home.vue';
import Login from '../views/Login.vue';
import moment from 'moment';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Login',
    component: Login,
  },
  {
    path: '/PortalSISAP',
    name: 'PortalSISAP',
    component: Home,
    meta: { requiresAuth: true }
  },
  { path: '/Local',
    name: 'Local', 
    component: Login 
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
      next({
        path: '/PortalSISAP',
        query: { redirect: to.fullPath }
      });
    } else {
      next();
    }
  } else if (to.matched.some(record => record.meta.requiresAuth) && isTokenValid()) {
    next();
  } else {
    window.localStorage.removeItem('token');
    next({
      path: '/',
      query: { redirect: to.fullPath }
    });
  }
});

export default router;
