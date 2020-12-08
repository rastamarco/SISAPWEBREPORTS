import Vue from 'vue';
import Vuex from 'vuex';
import { login } from './login';
import { Apontamento } from './Apontamento';
import { Expedicao } from './Expedicao';
import { Qualidade } from './Qualidade';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    login,
    Apontamento,
    Expedicao,
    Qualidade
  }
});
