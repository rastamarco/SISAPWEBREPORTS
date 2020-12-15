import axios from 'axios';
import ParametersControleOvos from '../models/parametersControleOvos.model';

export const ControleOvos = {
  state:{
  },
  getters:{
  },
  mutations:{
  },
  actions:{
    async reportFichaOvos({ commit }, options){
      const parameter = new ParametersControleOvos();
      parameter.InitialDate = options.initialDate;
      if (options.romaneio !== null || options.romaneio !== undefined){
        parameter.Romaneio = options.romaneio;
      }
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }
  
  }
};