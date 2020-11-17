import Vue from 'vue';
import Vuex from 'vuex';
import { login } from './login';
import { reportsApontamento } from './reportsApontamento';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    login,
    reportsApontamento
  }
});
