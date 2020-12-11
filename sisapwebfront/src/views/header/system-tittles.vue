<template>
<v-row>
  <v-col cols="12" sm="12">
    <h1 class="titles" v-if="userFeatures.isApontamento"> Sistema de Apontamento de Produção </h1>
    <h1 class="titles" v-if="userFeatures.isQualidade"> Sistema de Controle da Qualidade</h1>
    <h1 class="titles" v-if="userFeatures.isExpedicao"> Sistema de Controle da Expedição</h1>
    <h1 class="titles" v-if="userFeatures.isProducao"> Sistema de Controle da Produção </h1>
    <h1 class="titles" v-if="userFeatures.isEtiquetas"> Sistema de Impressão de Etiquetas </h1>
    <h1 class="titles" v-if="userFeatures.isAgendamentoCarga"> Sistema de Agendamento de Cargas </h1>
    <h1 class="titles" v-if="userFeatures.isApontamentoParada"> Sistema de Apontamento de Paradas </h1>
    <h1 class="titles" v-if="isSysAdmin"> Painel do Administrador </h1>
  </v-col>
</v-row>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import {
  Getter
} from 'vuex-class';
@Component({})
export default class SystemTabsOthers extends Vue {
  @Getter userFeatures
  @Getter isSysAdmin
}
</script>

<style scoped>
.titles {
  color: black;
  opacity: 0.7;
  font-size: 23px;
  padding-left: 40px;
}
</style>
