import axios from 'axios';
import ParametersIntegracaoUIA2 from '../models/parametersIntegracaoUIA2.model';

export const IntegracaoUIA2 = {
  state:{
  },
  getters:{
  },
  mutations:{
  },
  actions:{
    async reportProdSemIntegracao({commit}, options){
      const parameter = new ParametersIntegracaoUIA2();
      parameter.InitialDate = options.initialDate;
      parameter.localUser = options.filialname;
      if(options.shift){
        parameter.Shift = options.shift;
      }
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }
  
  }
};