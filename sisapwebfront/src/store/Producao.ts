import ParametersProducao from '../models/parametersProducao.model';

export const Qualidade = {
  state:{
    allChambers: null
  },
  getters:{
  },
  mutations:{
  },
  actions:{
    async reportMapaMensal({ commit }, options){
      const parameter = new ParametersProducao();
      parameter.localUser = options.filialName;
      parameter.InitialDate = options.initialDate;
      parameter.EndDate = options.endDate;
      parameter.Line = options.line;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }, 

  }
};