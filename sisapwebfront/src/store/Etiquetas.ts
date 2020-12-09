import axios from 'axios';
import ParametersEtiquetas from '../models/parametersEtiqueta.model';

export const Etiquetas = {
  state:{
    allProviders: null
  },
  getters:{
    allProviders(state) {
      return state.allProviders;
    },
  },
  mutations:{
    setAllProviders(state, value){
      state.allProviders = value;
    }
  },
  actions:{

    async getAllProviders({commit}, options){
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/providers/all`;
      const response = await axios({
        method: 'get',
        url: routeAPILogin
      });
      if (response) {
        if (response.status === 200) {
          await commit('setAllProviders', response.data);
        }
      } 
    },

    async reportEtiquetaPallet({ commit }, options){
      const parameter = new ParametersEtiquetas();
      // parameter.InitialDate = options.initialDate;
      // parameter.EndDate = options.endDate;
      // await commit('setIdReport', options.idReport);
      // await commit('setReportModule', options.reportModule);
      // await commit('setParams', JSON.stringify(parameter)); 
      // await commit('setShowReport', true);
    }, 

  }
};