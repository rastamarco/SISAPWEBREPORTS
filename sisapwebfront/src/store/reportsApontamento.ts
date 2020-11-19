import axios from 'axios';
import Parameters from '../models/parameters.model';

export const reportsApontamento = {
  state:{
    showReport: false,
    queryReport: null,
    idReport: null,
    params: null
  },
  getters:{
    showReport(state) {
      return state.showReport;
    },
    queryReport(state){
      return state.queryReport;
    },
    idReport(state){
      return state.idReport;
    },
    params(state){
      return state.params;
    }
  },
  mutations:{
    setShowReport(state, value) {
      state.showReport = value;
    },
    setQueryReport(state, value) {
      state.queryReport = value;
    },
    setIdReport(state, value){
      state.idReport = value;
    },
    setParams(state, value){
      state.params = value;
    }
  },
  actions:{
    async reportApontamentoProducao ({ commit }, options){
      const parameter = new Parameters();
      parameter.InitialDate = options.date;
      parameter.period = options.period;
      parameter.shift = options.turno;
      parameter.localUser = options.localUser;
      console.log(options.idReport);
      await commit('setIdReport', options.idReport);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },
    async noShowReport ({ commit }, options){ 
      await commit('setShowReport', options.show);
    },
    async setSelectedIdReport({ commit }, options) {
      await commit('setIdReport', options.id);
    }
  }
};