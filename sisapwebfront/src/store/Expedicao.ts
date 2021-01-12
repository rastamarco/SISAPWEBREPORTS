import axios from 'axios';
import ParametersExpedicao from '../models/parametersExpedicao.model';
import Product from '../models/product.model';

export const Expedicao = {
  state:{
    allChambers: null,
    productName: null,
    validPallet: null,
    box: null
  },
  getters:{
    allChambers(state){
      return state.allChambers;
    },
    productName(state){
      return state.productName;
    },
    validPallet(state){
      return state.validPallet;
    },
    box(state){
      return state.box;
    }
  },
  mutations:{
    setAllChambers(state, value) {
      state.allChambers = value;
    },
    setProductName(state, value) {
      state.productName = value;
    },
    setValidPallet(state, value){
      state.validPallet = value;
    },
    setBox(state, value){
      state.box = value;
    },
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
      if(options.shift || options.shift !== ''){
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
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    async ReportHistoricoCamaraPallet({commit}, options){
      const parameter = new ParametersExpedicao();
      const chamberToReport: Array<any> = [];
      if(options.codSicop){
        parameter.CodSicop = options.codSicop;
      }
      chamberToReport.push(options.idChamber);
      parameter.Chambers = chamberToReport;
      parameter.InitialDate = options.initialDate;
      parameter.EndDate = options.endDate;
      parameter.localUser = options.filialName;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);

    },

    async getProductName({ commit }, options){
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/informations/product/${options.productId}/${options.filialName}?`;
      const response = await axios({
        method: 'get',
        url: routeAPILogin
      });
      if (response) {
        if (response.status === 200) {
          await commit('setProductName', response.data);
        } else {
          await commit('setProductName', null);
        }
      } 
    },

    async getValidPallet({ commit }, options){
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/expedition/pallet/${options.filialName}/${options.nrPallet}?`;
      const response = await axios({
        method: 'get',
        url: routeAPILogin
      });
      if (response) {
        if (response.status === 200) {
          await commit('setValidPallet', response.data);
        } else {
          await commit('setValidPallet', null);
        }
      } 
    },

    async ReportLocalizacaoProdutos({commit}, options){
      const parameter = new ParametersExpedicao();
      parameter.CodSicop = options.codSicop;
      parameter.nrPallet = options.nrPallet;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    async ReportPosicaoCamaraVazia({commit}, options){
      const parameter = new ParametersExpedicao();
      parameter.localUser = options.filialName;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    async SetIdBox({commit}, options){
      commit('setBox', options.id);
    },

    async ReportPesoProdutoCamara({commit}, options){
      const parameter = new ParametersExpedicao();
      const chamberToReport: Array<any> = [];
      if(options.codSicop){
        parameter.CodSicop = options.codSicop;
      }
      chamberToReport.push(options.idChamber);
      parameter.Chambers = chamberToReport;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    async ReportProdutosEmbarcados({commit}, options){
      const parameter = new ParametersExpedicao();
      parameter.InitialDate = options.initialDate;
      parameter.nrCarga = options.nrCarga;
      parameter.nrConteiner = options.nrConteiner;
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);
    },

    async ReportEmbarquesPeriodo({commit}, options){
      const parameter = new ParametersExpedicao();
      parameter.InitialDate = options.initialDate;
      parameter.EndDate = options.endDate;
      if (options.nrConferente){
        parameter.nrNote = options.nrConferente;
      }
      await commit('setIdReport', options.idReport);
      await commit('setReportModule', options.reportModule);
      await commit('setParams', JSON.stringify(parameter)); 
      await commit('setShowReport', true);

    }


  }
};