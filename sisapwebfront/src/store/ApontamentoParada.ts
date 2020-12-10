import axios from 'axios';
import ParametersApontamentoParadas from '../models/parametersApontamentoParadas.model';

export const ApontamentoParada = {
  state:{
    allCities: null,
    loadingCities: false
  },
  getters:{
    allCities(state) {
      return state.allCities;
    },
    loadingCities(state){
      return state.loadingCities;
    }
  },
  mutations:{
    setAllCities(state, value){
      state.allCities = value;
    },
    setLoadingCities(state, value){
      state.loadingCities = value;
    }
  },
  actions:{

    async getAllCities({commit}, options){
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/stops/city/all`;
      const response = await axios({
        method: 'get',
        url: routeAPILogin
      });
      if (response) {
        if (response.status === 200) {
          await commit('setAllCities', response.data);
          await commit('setLoadingCities', true);
        } else {
          await commit('setLoadingCities', false);
        }
      } 
    },

    async reportCondenacoesMunicipio({commit}, options){
      const parameter = new ParametersApontamentoParadas();
      parameter.InitialDate = options.initialDate;
      parameter.EndDate = options.endDate;
      if(options.idCity !== null || options.idCity !== undefined){
        parameter.city = options.idCity;
      }
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    
  }
};