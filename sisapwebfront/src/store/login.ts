import axios from 'axios';
import User from '../models/user.model';
import usersPermissionsViewModel from '../models/usersPermissions.view.model';

export const login = {
  state :{
    isAuthenticated: false,
    filialName: null,
    userType: null,
    token: null,
    isSysAdmin: null,
    nameUser: null,
    idUser: null,
    userFeatures: null
  },
  getters: {
    
    isAuthenticated(state) {
      return state.isAuthenticated;
    },
    filialName(state) {
      return state.filialName;
    },
    userType(state) {
      return state.userType;
    },
    token(state) {
      return state.token;
    },
    isSysAdmin(state) {
      return state.isSysAdmin;
    },
    nameUser(state) {
      return state.nameUser;
    },
    idUser(state) {
      return state.idUser;
    },
    userFeatures(state){
      return state.userFeatures;
    }
    
  },
  mutations: {
    setIsAuthenticated(state, value) {
      state.isAuthenticated = value;
    },
    setAuthData(state, value) {
      state.isAuthenticated = value.isAuthenticated;
      state.filialName = value.filialName;
      state.userType = value.userType;
      state.token = value.token;
      state.isSysAdmin = value.isSysAdmin;
      state.nameUser = value.nameUser;
      state.idUser = value.idUser;
    },
    setUserFeatures(state, value) {
      state.userFeatures = value;
    }
  },
  actions:{
    async doLogin({ commit }, options){      
      const user = new User();
      user.codFilial = options.codFilial;
      user.login = options.login;
      user.senha = options.senha;
      const routeAPILogin = `${process.env.VUE_APP_API_URL}/api/auth/login`;
      const response = await axios({
        method: 'post',
        url: routeAPILogin,
        data: user
      });
      console.log(response);
      if (response) {
        if (response.status === 200) {
          const decodeToken = JSON.parse(atob(response.data.token.split('.')[1]));
          let isAdmin = false;
          if (decodeToken.TipoUsuario === 'Administrador'){
            isAdmin = true;
          }

          const UFeatures = new usersPermissionsViewModel();
          const systemRouter = response.data.user.tipoUsuario;
          // PERMISSÕES DO USUÁRIO, MOSTRA EM QUAL SISTEMA ELE VAI SER REDIRECIONADO -------  REFATORAR!!!!
          if (systemRouter === 'Controle da Produç�o' || systemRouter === 'Embalagem Primária' || (systemRouter === 'Embalagem Secund�ria' && decodeToken.user.login !== 'exp') || systemRouter === 'Oficina' ||
          systemRouter === 'Operador Apontamento' || systemRouter === 'PCP' || systemRouter === 'Expedição Controle') {
            UFeatures.isApontamento = true;
          } else if (systemRouter === 'Monitor da Qualidade') {
            UFeatures.isQualidade = true;
          } else if (systemRouter === 'Expedição Carregamen' || systemRouter === 'Expediç�o Embalagem' || systemRouter === 'Expediç�o Embarque' || systemRouter === 'Expediç�o Controle' 
          || (systemRouter === 'Embalagem Secund�ria' && decodeToken.user.login === 'exp')) {
            UFeatures.isExpedicao = true;
          }  

          const tokenData = {
            isAuthenticated: true,
            filialName: decodeToken.Filial,
            userType: systemRouter, 
            token: response.data.token,
            tokenExpiration: decodeToken.exp,
            isSysAdmin: isAdmin,
            nameUser: response.data.user.nomeUsuario,
            idUser: response.data.user.codUsuario
          };
          const authData = {
            isAuthenticated: true,
            filialName: decodeToken.Filial,
            userType: systemRouter,
            token: response.data.token,
            isSysAdmin: isAdmin,
            nameUser: response.data.user.nomeUsuario,
            idUser: response.data.user.codUsuario
          };
        
          
          const tokenEncoded = btoa(JSON.stringify(tokenData));
          window.localStorage.setItem('token', tokenEncoded);
          await commit('setUserFeatures', UFeatures);
          await commit('setAuthData', authData);
        } else {
          await commit('setIsAuthenticated', false);
        }
      } else {
        await commit('setIsAuthenticated', false);
      }
    },

    async reloadUser({ commit }, options){
      const decodeToken = JSON.parse(atob(options.token));

      const authData = {
        isAuthenticated: true,
        filialName: decodeToken.filialName,
        userType: decodeToken.userType,
        token: decodeToken.token,
        tokenExpiration: decodeToken.exp,
        isSysAdmin: decodeToken.isSysAdmin,
        nameUser: decodeToken.nameUser,
        idUser: decodeToken.idUser
      };
      const UFeatures = new usersPermissionsViewModel();
      // PERMISSÕES DO USUÁRIO, MOSTRA EM QUAL SISTEMA ELE VAI SER REDIRECIONADO
      if (authData.userType === 'Controle da Produção' || authData.userType === 'Embalagem Primária' || authData.userType === 'Embalagem Secundária' || authData.userType === 'Oficina' ||
        authData.userType === 'Operador Apontamento' || authData.userType === 'PCP' || authData.userType === 'Expedição Controle') {
        UFeatures.isApontamento = true;
      } else if (authData.userType === 'Monitor da Qualidade') {
        UFeatures.isQualidade = true;
      } else if (authData.userType === 'Expedição Carregamento' || authData.userType === 'Expedição Embalagem') {
        UFeatures.isExpedicao = true;
      }
      await commit('setUserFeatures', UFeatures);
      await commit('setAuthData', authData);
      await commit('setIsAuthenticated', true);
    }
  }

};
