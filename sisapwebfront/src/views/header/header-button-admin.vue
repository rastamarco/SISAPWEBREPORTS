<template>
<v-menu v-model="menu" :close-on-content-click="false" :nudge-width="200" offset-y max-height="600" max-width="400">
  <template v-slot:activator="{ on, attrs }">
    <v-btn color="white" v-bind="attrs" v-on="on" fab small absolute left>
      <v-icon color="primary">mdi-menu</v-icon>
    </v-btn>
  </template>
  <v-card class="card-content" >
    <v-list dense>
      <v-list-item dense>
        <v-list-item-avatar>
          <v-img :src="require('./../../assets/logo-lar.png')" contain />
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title v-if="isSysAdmin"><b>Menu do Administrador</b> </v-list-item-title>
          <v-list-item-title v-if="userType === 'Consultoria'"><b>Consultoria</b> </v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
    <v-divider></v-divider>
    <v-list dense>
      <v-list-item @click="ChangeSystem(0)" dense>
        <v-list-item-action>
          <v-icon color="primary">mdi-home</v-icon>
        </v-list-item-action>
        <v-list-item-title>Painel Inicial</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(1)" dense v-if="filialName === 'UIA' || filialName === 'UIC' || filialName === 'SIF' || filialName === 'GBA' || filialName === 'UPP'">
        <v-list-item-action>
          <v-icon color="primary">mdi-elevator</v-icon>
        </v-list-item-action>
        <v-list-item-title>Apontamentos de Produção</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(2)" dense v-if="filialName === 'UIA' || filialName === 'UIC' || filialName === 'SIF' || filialName === 'GBA' || filialName === 'UPP'">
        <v-list-item-action>
          <v-icon color="primary">mdi-bus-double-decker</v-icon>
        </v-list-item-action>
        <v-list-item-title>Controle da Expedição</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(3)" dense v-if="filialName === 'UIA' || filialName === 'UIAIII' || filialName === 'UIC' || filialName === 'SIF' || filialName === 'GBA' || filialName === 'UPP'">
        <v-list-item-action>
          <v-icon color="primary">mdi-factory</v-icon>
        </v-list-item-action>
        <v-list-item-title>Controle da Produção</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(4)" dense v-if="filialName === 'UIA' || filialName === 'UIAII' || filialName === 'UIAIII' || filialName === 'UIC' || filialName === 'SIF' || filialName === 'GBA' || filialName === 'UPP'">
        <v-list-item-action>
          <v-icon color="primary">mdi-registered-trademark</v-icon>
        </v-list-item-action>
        <v-list-item-title>Controle de Qualidade</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(5)" dense v-if="filialName === 'UIA' || filialName === 'UIC' || filialName === 'SIF' || filialName === 'GBA' || filialName === 'UPP'">
        <v-list-item-action>
          <v-icon color="primary">mdi-ticket-percent</v-icon>
        </v-list-item-action>
        <v-list-item-title>Impressão de Etiquetas</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(6)" dense v-if="filialName === 'UIA' || filialName === 'UIC' || filialName === 'SIF' || filialName === 'GBA' || filialName === 'UPP'">
        <v-list-item-action>
          <v-icon color="primary">mdi-stop-circle</v-icon>
        </v-list-item-action>
        <v-list-item-title>Apontamento de Paradas</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(7)" dense v-if="filialName === 'UIA' || filialName === 'UIC' || filialName === 'SIF' || filialName === 'GBA' || filialName === 'UPP'">
        <v-list-item-action>
          <v-icon color="primary">mdi-pencil-box</v-icon>
        </v-list-item-action>
        <v-list-item-title>Agendamento de Cargas</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(8)" dense v-if="filialName === 'UDM'">
        <v-list-item-action>
          <v-icon color="primary">mdi-egg</v-icon>
        </v-list-item-action>
        <v-list-item-title>Controle de Ovos</v-list-item-title>
      </v-list-item>
      <v-list-item @click="ChangeSystem(9)" dense v-if="filialName === 'UIAII'">
        <v-list-item-action>
          <v-icon color="primary">mdi-factory</v-icon>
        </v-list-item-action>
        <v-list-item-title>Integração Emb. e Produção</v-list-item-title>
      </v-list-item>
    </v-list>
  </v-card>
  </v-menu>
</template>
<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import {
  Getter
} from 'vuex-class';

@Component
export default class HeaderButtonAdmin extends Vue {
  @Getter isSysAdmin
  @Getter userType
  @Getter filialName

  private menu: boolean = false;

  public ChangeSystem(system: any): void{
    this.$emit('getSystem', system);
    this.menu = false;
  }

  mounted(){
    this.ChangeSystem(0);
  }
}
</script>
<style scoped>
.card-content{
  display:flex;
  flex-direction: column;
  justify-content:center;
  align-items: center;
}
</style>