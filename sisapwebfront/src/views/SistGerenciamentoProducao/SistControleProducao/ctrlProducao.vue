<template>
<v-row>
  <v-col cols="12" sm="3" class="sideReport">
    <v-sheet rounded="lg" class="items">
      <!-- <Cadastros /> -->
      <Relatorios class="reports"/>
      <!-- <Etiquetas /> -->
    </v-sheet>
  </v-col>
  <v-col cols="12" sm="9">
    <v-sheet  rounded="lg" height="84vh">
      <div class="initial" v-if="!showReport">
        <div class="system-text">
          <SystemTitles />
        </div>
        <div class="system-img">
          <v-img :src="require('../../../assets/logo-lar.png')" class="my-3" contain height="230" />
        </div>
        <div class="footer-initial">
        </div>
      </div>
      <Reports v-if="showReport" style="height:100%;"/>
    </v-sheet>
  </v-col>
</v-row>
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

import Relatorios from './Relatorios/relatorioSidebarCtrlProducao.vue';

import Reports from './../../reports/Reports.vue';
import SystemTitles from './../../header/system-tittles.vue';

@Component({
  components: {
    Relatorios,
    Reports,
    SystemTitles
  }
})
/* eslint-disable indent */
export default class CtrlProducao extends Vue {
  @Action reloadUser

  @Getter showReport

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
.initial {
  width: 100%;
  height: 400px;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.initial .system-text {
  width: 100%;
  height: 20%;
  display: flex;
  justify-content: center;
}

.initial .system-img {
  width: 100%;
  height: 80%;
  padding-left: 50px;
  padding-top: 50px;
}

.sideReport{
  height: 87vh;
}

.sideReport .items {
  height:100%;
  overflow-y: auto; 
}

.sideReport .items .reports{
  margin-right: 5%;
  margin-left: 5%;
}

.sideReport .items::-webkit-scrollbar-track {
   background-color: #F4F4F4;
}
.sideReport .items::-webkit-scrollbar {
   width: 5px;
    background: #F4F4F4;
}
.sideReport .items::-webkit-scrollbar-thumb {
   background: silver;
}
</style>
