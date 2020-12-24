import axios from 'axios';
import ParametersExpedicao from '../models/parametersExpedicao.model';

export const Expedicao = {
  state:{
    allChambers: null
  },
  getters:{
    allChambers(state){
      return state.allChambers;
    }
  },
  mutations:{
    setAllChambers(state, value) {
      state.allChambers = value;
    }
  },
  actions:{
    async reportFormacaoPallets({ commit }, options){
      const parameter = new ParametersExpedicao();
      parameter.nrPallet = options.nrPallet;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    }, 

    async reportMovimentoCamaraOperador({ commit }, options) {
      const parameter = new ParametersExpedicao();
      const chamberToReport: Array<any> = [];
      // 8 CAMARAS, caso aumente, coloca mais aqui e mais parametros no relatorio que vai dar certo ou implemente uma forma melhor =) 
      for(let i = 0; i < 8;i++){
        if(options.chambers[i] === undefined || options.chambers[i] === null){
          chamberToReport.push('');  
        }else{
          chamberToReport.push(options.chambers[i]);
        }
      }
      parameter.Chambers = chamberToReport;
      parameter.InitialDate = options.initialDate;
      if(options.registration){
        parameter.Registration = options.registration;
      }
      if(options.endDate){
        parameter.EndDate = options.endDate;
      }
      if(options.shift){
        parameter.Shift = options.shift;
      }
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    async getChambersByFilial({ commit }, options){
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/expedition/chambers/${options.filial}?`;
      const response = await axios({
        method: 'get',
        url: routeAPILogin
      });
      if (response) {
        if (response.status === 200) {
          await commit('setAllChambers', response.data);
        }
      } 
    },

    async reportCamara({commit }, options){
      const parameter = new ParametersExpedicao();
      
    }
  }
};