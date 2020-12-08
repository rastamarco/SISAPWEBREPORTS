<template>
<div class="grey lighten-3">
  <v-app-bar app color="primary">
    <systemTabs v-if="isSysAdmin" />
    <h2 class="title-header" v-if="!isSysAdmin"> Portal SISAP </h2>
    <headerBtn />
    <v-spacer></v-spacer>
  </v-app-bar>
  <v-container  style="margin-top: 65px;">
    <apontamentos v-if="userFeatures.isApontamento" /> 
    <expedicao v-if="userFeatures.isExpedicao" />
    <qualidade v-if="userFeatures.isQualidade" />
    <ctrlProducao v-if="userFeatures.isProducao" />
    <etiquetas v-if="userFeatures.isEtiquetas" />
  </v-container>
</div>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import {
  Action,
  Getter
} from 'vuex-class';
import headerBtn from './header/header-button.vue';
import systemTabs from './header/system-tabs-admin.vue';
import apontamentos from './SistGerenciamentoProducao/SistApontamentosProducao/apontamentos.vue';
import expedicao from './SistGerenciamentoProducao/SistControleExpedição/expedicao.vue';
import qualidade from './SistGerenciamentoProducao/SistCTRLQualidade/qualidade.vue';
import ctrlProducao from './SistGerenciamentoProducao/SistControleProducao/ctrlProducao.vue';
import etiquetas from './SistGerenciamentoProducao/SistImpressaoEtiquetas/etiquetas.vue';

@Component({
  components: {
    headerBtn,
    systemTabs,
    apontamentos,
    expedicao,
    qualidade,
    ctrlProducao,
    etiquetas
  }
})
export default class Home extends Vue {
  @Action reloadUser

  @Getter isSysAdmin
  @Getter userFeatures

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
  font-size: 28px;
  color: white;
}
</style>
