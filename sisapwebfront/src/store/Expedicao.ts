import ParametersExpedicao from '../models/parametersExpedicao.model';

export const Expedicao = {
  state:{
  },
  getters:{
  },
  mutations:{
  },
  actions:{
    async reportFormacaoPallets({ commit }, options){
      const parameter = new ParametersExpedicao();
      parameter.nrPallet = options.nrPallet;
      await commit('setIdReport', options.idReport);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }, 

    async reportMovimentoCamaraOperador({ commit }, options) {
      console.log('Teste');
      
    }
  }
};