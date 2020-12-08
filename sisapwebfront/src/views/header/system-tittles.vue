<template>
<v-row>
  <v-col cols="12" sm="12">
    <h1 class="titles" v-if="userFeatures.isApontamento"> Sistema de Apontamento de Produção </h1>
    <h1 class="titles" v-if="userFeatures.isQualidade"> Sistema de Controle da Qualidade</h1>
    <h1 class="titles" v-if="userFeatures.isExpedicao"> Sistema de Controle da Expedição</h1>
    <h1 class="titles" v-if="userFeatures.isProducao"> Sistema de Controle da Produção </h1>
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
