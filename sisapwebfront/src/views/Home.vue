<template>
<div class="grey lighten-3">
  <v-app-bar app color="primary">
    <headerButtonAdmin v-if="isSysAdmin || userType === 'Consultoria'" v-on="{getSystem}"/>
    <h2 class="title-header-admin" v-if="isSysAdmin || userType === 'Consultoria'"> {{systemName}} </h2>
    <h2 class="title-header" v-if="!isSysAdmin && userType !== 'Consultoria'"> Portal SISAP </h2>
    <headerBtn />
    <v-spacer></v-spacer>
  </v-app-bar>
  <v-container  style="margin-top: 65px;">
    <adminHome v-if="(isSysAdmin === true || userType === 'Consultoria') && this.system === 0"  />
    <apontamentos v-if="userFeatures.isApontamento || this.system === 1" /> 
    <expedicao v-if="userFeatures.isExpedicao || this.system === 2" />
    <qualidade v-if="userFeatures.isQualidade || this.system === 3"/>
    <ctrlProducao v-if="userFeatures.isProducao || this.system === 4" />
    <etiquetas v-if="userFeatures.isEtiquetas || this.system === 5" />
    <agendamentoCargas v-if="userFeatures.isAgendamentoCarga || this.system === 6" />
    <apontamentoParadas v-if="userFeatures.isApontamentoParada || this.system === 7" />
    <controleOvos v-if="userFeatures.isControleOvos || this.system === 8" />
    <integracaoUIA2 v-if="userFeatures.isQualidadeUIA2 || this.system === 9" />
    <integracaoUIA2 v-if="userFeatures.isIntegracaoUIA2 || this.system === 10" />
  </v-container>
</div>
</template>

<script lang="ts">
import {
  Component,
  Vue,
  Watch
} from 'vue-property-decorator';
import {
  Action,
  Getter
} from 'vuex-class';
import headerBtn from './header/header-button.vue';
import headerButtonAdmin from './header/header-button-admin.vue';
import apontamentos from './SistGerenciamentoProducao/SistApontamentosProducao/apontamentos.vue';
import expedicao from './SistGerenciamentoProducao/SistControleExpedição/expedicao.vue';
import qualidade from './SistGerenciamentoProducao/SistCTRLQualidade/qualidade.vue';
import ctrlProducao from './SistGerenciamentoProducao/SistControleProducao/ctrlProducao.vue';
import etiquetas from './SistImpressaoEtiquetas/etiquetas.vue';
import agendamentoCargas from './SistAgendamentoDeCargas/agendamentoCargas.vue';
import apontamentoParadas from './SistApontamentoParadas/apontamentoParadas.vue';
import adminHome from './adminManager/adminHome.vue';
import controleOvos from './SistCTLRColetaOvos/ctrlOvos.vue';
import integracaoUIA2 from './SistIntegracaoEmbarquesProducao/integracaoUIA2.vue';
import _ from 'vuetify/es5/components/*';

@Component({
  components: {
    headerBtn,
    headerButtonAdmin,
    apontamentos,
    expedicao,
    qualidade,
    ctrlProducao,
    etiquetas,
    agendamentoCargas,
    apontamentoParadas,
    adminHome,
    controleOvos,
    integracaoUIA2
  }
})
export default class Home extends Vue {
  @Action reloadUser
  @Action setUserFeaturesRouterAdmin

  @Getter isSysAdmin
  @Getter userFeatures
  @Getter userType
  @Getter filialName

  private system: any = null;
  private systemName: any = null;

  public async getSystem(system: any): Promise<void> {
    this.system = system;
  }

  @Watch('system')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    switch(this.filialName){
    case 'UIA':
      switch(value){
      case 0:
        this.systemName = 'Home';
        break;
      case 1:
        this.systemName = 'Sistema de Apontamento de Produção';
        break;
      case 2:
        this.systemName = 'Sistema de Controle da Expedição';
        break;
      case 3:
        this.systemName = 'Sistema de Controle da Produção';
        break;
      case 4:
        this.systemName = 'Sistema de Controle da Qualidade';
        break;
      case 5:
        this.systemName = 'Sistema de Impressão de Etiquetas';
        break;
      case 6:
        this.systemName = 'Sistema de Apontamento de Paradas';
        break; 
      case 7:
        this.systemName = 'Sistema de Agendamento de Cargas';  
        break;
      }
      break;
    case 'UIAII':
      switch(value){  
      case 9:
        this.systemName = 'Sistema de Integração de Embarques e Produção';
        break;
      case 10:
        this.systemName = 'Sistema de Controle de Qualidade';
        break;
      }
      break;
    case 'UIAIII':
      break;
    case 'UPO':
      switch(value){ 
      case 8:
        this.systemName = 'Sistema de Controle de Coleta de Ovos';
        break;
      }
      break;
    }
  }

  beforeMount() {
    if (window.localStorage.token) {
      this.reloadUser({
        token: window.localStorage.token
      });
    }
  }
}

</script>

<style scoped>
.title-header {
  padding-left: 25px;
  font-size: 24px;
  color: white;
}

.title-header-admin {
  padding-left: 60px;
  font-size: 22px;
  color: white;
}
</style>
