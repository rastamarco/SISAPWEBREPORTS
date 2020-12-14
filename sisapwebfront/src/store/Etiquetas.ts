import axios from 'axios';
import ParametersEtiquetas from '../models/parametersEtiqueta.model';

export const Etiquetas = {
  state:{
    allProviders: null,
    loadingProviders: false
  },
  getters:{
    allProviders(state) {
      return state.allProviders;
    },
    loadingProviders(state){
      return state.loadingProviders;
    }
  },
  mutations:{
    setAllProviders(state, value){
      state.allProviders = value;
    },
    setLoadingProviders(state, value){
      state.loadingProviders = value;
    }
  },
  actions:{

    async getAllProviders({commit}, options){
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/scheduling/providers/all?${options.filialName}`;
      const response = await axios({
        method: 'get',
        url: routeAPILogin
      });
      if (response) {
        if (response.status === 200) {
          await commit('setAllProviders', response.data);
          await commit('setLoadingProviders', true);
        } else {
          await commit('setLoadingProviders', false);
        }
      } 
    },

    async reportEtiquetaPallet({ commit }, options){
      const parameter = new ParametersEtiquetas();
      parameter.providerName = options.providerName;
      parameter.idProvider = options.idProvider;
      parameter.nrPlaca = options.nrPlaca;
      parameter.nrNote = options.nrNote;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }, 

  }
};