/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable no-unused-vars */
/* eslint-disable @typescript-eslint/no-unused-vars */
import Vue from 'vue';
import App from './App.vue';
import './registerServiceWorker';
import router from './router';
import store from './store';
import vuetify from './plugins/vuetify';
import Vuetify from 'vuetify';
import axios from 'axios';
import VueSweetalert2 from 'vue-sweetalert2';
import VueTheMask from 'vue-the-mask';

Vue.use(VueSweetalert2);
Vue.use(VueTheMask);
Vue.prototype.$http = axios;
Vue.config.productionTip = false;

let token: any = '';
Object.defineProperty(Vue.prototype, '$token', {
  get() {
    return token;
  },
  set(value) {
    token = value;
  }
});

axios.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    return error.response;
  }
);

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app');


