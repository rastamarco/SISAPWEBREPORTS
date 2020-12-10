import ParametersAgendamentoCargas from '../models/parametersAgendamentoCargas.model';

export const AgendamentoCarga = {
  state:{
  },
  getters:{
  },
  mutations:{
  },
  actions:{
    async reportAgendamentoSemEmbarque({commit}, options){
      const parameter = new ParametersAgendamentoCargas();
      parameter.InitialDate = options.initialDate;
      parameter.EndDate = options.endDate;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    
  }
};