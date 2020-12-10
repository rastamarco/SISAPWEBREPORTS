import axios from 'axios';
import ParametersApontamento from '../models/parametersApontamento.model';

export const Apontamento = {
  state:{
    showReport: false,
    queryReport: null,
    idReport: null,
    params: null,
    reportModule: null,
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
    },
    reportModule(state){
      return state.reportModule;
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
    },
    setReportModule(state, value){
      state.reportModule = value;
    }
  },
  actions:{
    async reportApontamentoProducao ({ commit }, options){
      const parameter = new ParametersApontamento();
      parameter.InitialDate = options.date;
      parameter.period = options.period;
      parameter.shift = options.shift;
      parameter.localUser = options.localUser;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },
    
    async noShowReport ({ commit }, options){ 
      await commit('setShowReport', options.show);
      await commit('setIdReport', null);
      await commit('setReportModule', null);
    },

    async setSelectedIdReport({ commit }, options) {
      await commit('setIdReport', options.id);
    },


    async reportApontamentoRefeitorio({commit}, options){
      const parameter = new ParametersApontamento();
      parameter.InitialDate = options.InitialDate;
      if(options.EndDate){
        parameter.EndDate = options.EndDate;
      }
      parameter.InitialHour = options.InitialHour;
      parameter.EndHour = options.EndHour;
      parameter.shift = options.shift;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }

  }
};