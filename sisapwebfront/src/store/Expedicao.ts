import axios from 'axios';
import ParametersExpedicao from '../models/parametersExpedicao.model';

export const Expedicao = {
  state:{
    allChambers: null
  },
  getters:{
    allChambers(state){
      return state.allChambers;
    }
  },
  mutations:{
    setAllChambers(state, value) {
      state.allChambers = value;
    }
  },
  actions:{
    async reportFormacaoPallets({ commit }, options){
      const parameter = new ParametersExpedicao();
      parameter.nrPallet = options.nrPallet;
      await commit('setIdReport', options.idReport);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }, 

    async reportMovimentoCamaraOperador({ commit }, options) {
      console.log('Teste');
      
    },

    async getChambersByFilial({ commit }, options){
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/chambers/${options.filial}?`;
      const response = await axios({
        method: 'get',
        url: routeAPILogin
      });
      if (response) {
        if (response.status === 200) {
          await commit('setAllChambers', response.data);
        }
      } 
    }
  }
};