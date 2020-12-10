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
    userFeatures: null,
    loginUser: null
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
    },
    loginUser(state){
      return state.loginUser;
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
      state.loginUser = value.loginUser;
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
          const decodeToken = JSON.parse(decodeURIComponent(escape(atob(response.data.token.split('.')[1]))));
          console.log(decodeToken);
          
          let isAdmin = false;
          if (decodeToken.TipoUsuario === 'Administrador'){
            isAdmin = true;
          }

          const UFeatures = new usersPermissionsViewModel();
          const typeUser = response.data.user.tipoUsuario;
          // PERMISSÕES DO USUÁRIO, MOSTRA EM QUAL SISTEMA ELE VAI SER REDIRECIONADO 
          if (typeUser === 'Controle da Produção' || typeUser === 'Embalagem Primária' || typeUser === 'Oficina' || (typeUser === 'Embalagem Secundária' && response.data.user.login !== 'exp') ||
          typeUser === 'Operador Apontamento' || typeUser === 'PCP' || typeUser === 'Expedição Controle') {
            UFeatures.isApontamento = true;
          } else if (typeUser === 'Monitor da Qualidade') {
            UFeatures.isQualidade = true;
          } else if (typeUser === 'Expedição Carregamen' || typeUser === 'Expedição Embalagem' || typeUser === 'Expedição Embarque' || typeUser === 'Expedição Controle' 
          || (typeUser === 'Embalagem Secundária' && response.data.user.login === 'exp')) {
            UFeatures.isExpedicao = true;
          } else if (typeUser === 'Produção' || typeUser === 'Gestão de Processos') {
            UFeatures.isProducao = true;
          } else if (typeUser === 'Etiqueta Exp' || typeUser === 'Etiqueta Insumo' || typeUser === 'Etiqueta Linguiça' || typeUser === 'Etiqueta')  {
            UFeatures.isEtiquetas = true;
          } else if (typeUser === 'Logística'){
            UFeatures.isAgendamentoCarga = true;
          } else if (typeUser === 'SIF'){
            UFeatures.isApontamentoParada = true;
          }
          
          const tokenData = {
            isAuthenticated: true,
            loginUser: response.data.user.login,
            filialName: decodeToken.Filial,
            userType: typeUser, 
            token: response.data.token,
            tokenExpiration: decodeToken.exp,
            isSysAdmin: isAdmin,
            nameUser: response.data.user.nomeUsuario,
            idUser: response.data.user.codUsuario
          };
          const authData = {
            isAuthenticated: true,
            loginUser: response.data.user.login,
            filialName: decodeToken.Filial,
            userType: typeUser,
            token: response.data.token,
            isSysAdmin: isAdmin,
            nameUser: response.data.user.nomeUsuario,
            idUser: response.data.user.codUsuario
          };
        
          
          const tokenEncoded = btoa(JSON.stringify(tokenData));
          window.localStorage.setItem('token', tokenEncoded);
          console.log(UFeatures);
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
        loginUser: decodeToken.loginUser,
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
      if (authData.userType === 'Controle da Produção' || authData.userType === 'Embalagem Primária' || authData.userType === 'Oficina' ||  (authData.userType === 'Embalagem Secundária' && authData.loginUser !== 'exp') ||
          authData.userType === 'Operador Apontamento' || authData.userType === 'PCP' || authData.userType === 'Expedição Controle') {
        UFeatures.isApontamento = true;
      } else if (authData.userType === 'Monitor da Qualidade') {
        UFeatures.isQualidade = true;
      } else if (authData.userType === 'Expedição Carregamen' || authData.userType === 'Expedição Embalagem' || authData.userType === 'Expedição Embarque' || authData.userType === 'Expedição Controle' 
          || (authData.userType === 'Embalagem Secundária' && authData.loginUser === 'exp')) {
        UFeatures.isExpedicao = true;
      } else if (authData.userType === 'Produção' || authData.userType === 'Gestão de Processos') {
        UFeatures.isProducao = true;
      } else if (authData.userType === 'Etiqueta Exp' || authData.userType === 'Etiqueta Insumo' || authData.userType === 'Etiqueta Linguiça' || authData.userType === 'Etiqueta')  {
        UFeatures.isEtiquetas = true;
      } else if (authData.userType === 'Logística'){
        UFeatures.isAgendamentoCarga = true;
      }  else if (authData.userType === 'SIF'){
        UFeatures.isApontamentoParada = true;
      }
    
      await commit('setUserFeatures', UFeatures);
      await commit('setAuthData', authData);
      await commit('setIsAuthenticated', true);
    }
  }

};
