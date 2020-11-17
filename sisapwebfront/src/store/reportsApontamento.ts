import axios from 'axios';

export const reportsApontamento = {
  state:{
    showReport: false
  },
  getters:{
    showReport(state) {
      return state.showReport;
    },
  },
  mutations:{
    setShowReport(state, value) {
      state.showReport = value;
    },
  },
  actions:{
    async reportApontamentoResfriado ({ commit }, options){
      /// Mandar os dados para API 
      await commit('setShowReport', true);
    },
    async reportApontamentoResfriadoFalse ({ commit }, options){
      /// Mandar os dados para API 
      await commit('setShowReport', false);
    }
  }
};