import axios from 'axios';
import ParametersQualidade from '../models/parametersQualidade.model';

export const Qualidade = {
  state:{
    allChambers: null
  },
  getters:{
  },
  mutations:{
  },
  actions:{
    async reportQualidadeFomento({ commit }, options){
      const parameter = new ParametersQualidade();
      parameter.Month = options.month;
      parameter.Year = options.year;
      parameter.InitialDate = options.initialDate;
      parameter.EndDate = options.endDate;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }, 

  }
};