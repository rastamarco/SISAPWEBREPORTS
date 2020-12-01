import axios from 'axios';
import ParametersApontamento from '../models/parametersApontamento.model';

export const Apontamento = {
  state:{
    showReport: false,
    queryReport: null,
    idReport: null,
    params: null,
    reportModule: null
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
      parameter.shift = options.turno;
      parameter.localUser = options.localUser;
      console.log(options.idReport);
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
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