import Vue from 'vue';
import Vuex from 'vuex';
import { login } from './login';
import { Apontamento } from './Apontamento';
import { Expedicao } from './Expedicao';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    login,
    Apontamento,
    Expedicao
  }
});
