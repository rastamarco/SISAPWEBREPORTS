import ParametersProducao from '../models/parametersProducao.model';

export const Producao = {
  state:{
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

    async reportApontamentoRoboCMS({ commit }, options){
      const parameter = new ParametersProducao();
      parameter.InitialDate = options.initialDate;
      parameter.EndDate = options.endDate;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }, 

  }
};