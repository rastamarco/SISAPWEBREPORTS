<template>
<div class="reports">
  <v-list dense>
    <v-list-group v-for="item in itemsReportsByUser" :key="item.title" v-model="item.active" :prepend-icon="item.icon" >
      <template v-slot:activator>
        <v-list-item-content >
          <v-list-item-title v-text="item.title"></v-list-item-title>
        </v-list-item-content>
      </template>
      <v-dialog v-model="modalReports" persistent width="40%" >
        <template v-slot:activator="{ on, attrs }">
          <v-list-item v-for="child in item.items" :key="child.title" @click="selectReport(child)" v-on="on" v-bind="attrs">
            <v-list-item-content>
              <v-list-item-title v-text="child.title"></v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </template>
        <!-- Aqui vai todos os templates de input de parametros para relatórios --> 
        <InputModals v-bind="{ nameBox, idBox }" v-on="{ closeModal }"/>
      </v-dialog>
    </v-list-group>
  </v-list>
</div>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import {
  Action, Getter
} from 'vuex-class';

import InputModals from './inputModalExp.vue';
@Component({
  components: {
    InputModals
  }
})
export default class RelatorioSidebarExp extends Vue {
  @Action setSelectedIdReport
  @Action SetIdBox
  @Action reloadUser
  
  @Getter loginUser
  @Getter isSysAdmin
  /* eslint-disable indent */
  private modalReports: boolean = false;
  private nameBox: any = null;
  private idBox: any = null;

  private itemsReports: Array < any > = [{
    icon: 'mdi-file-chart',
    title: 'Relatórios',
    items: [{
        id: 1,
        title: 'Formação de Pallet',
        show: ''
      },
      {
        id: 2,
        title: 'Operador/Câmara',
        show: ''
      },
      {
        id: 3,
        title: 'Mapa de Câmaras',
        show: ''
      },
      {
        id: 4,
        title: 'Localização de Produtos',
        show: ''
      },
      {
        id: 5,
        title: 'Histórico Câmara/Pallets',
        show: ''
      },
      {
        id: 6,
        title: 'Posições Vazias nas Câmaras',
        show: ''
      },
      {
        id: 7,
        title: 'Produtos Estocado nas Câmaras',
        show: ''
      },
      {
        id: 8,
        title: 'Produtos Embarcados',
        show: ''
      },
      {
        id: 9,
        title: 'Embarques por Período',
        show: ''
      },
      {
        id: 10,
        title: 'Paradas, Embarques e Desembarques',
        show: ''
      },
      {
        id: 11,
        title: 'Entrada/Saída Túnel de Congelamento',
        show: ''
      },
      {
        id: 12,
        title: 'Cargas Embarcadas',
        show: ''
      },
      {
        id: 13,
        title: 'Reagendamento de Cargas',
        show: ''
      },
      {
        id: 14,
        title: 'Pallets na Câmara C',
        show: ''
      },
      {
        id: 15,
        title: 'Países Destino',
        show: ''
      },
      {
        id: 16,
        title: 'Produtos por SIF',
        show: ''
      },
    ]
  }, ]
  
  private itemsReportsByUser: Array<any> = [];

  public async selectReport(item: any): Promise<void> {
    switch (item.id) {
      case 1:
        // UIA 
        this.nameBox = 'Relatório de Formação de Pallet';
        this.idBox = 1;
        break;
      case 2:
        this.nameBox = 'Relatório de Movimentação Operador/Câmara';
        this.idBox = 2;
        break;
      case 3:
        this.nameBox = 'Relatório de Mapa de Câmaras';
        this.idBox = 3;
        break;
      case 4:
        this.nameBox = 'Relatório de Extrato de Localização de Produtos';
        this.idBox = 4;
        break;
      case 5:
        this.nameBox = 'Relatório de Histórico de Câmara/Pallets';
        this.idBox = 5;
        break;
      case 6:
        // Não possui Tela de Parametros
        this.nameBox = 'Relatório de Posições Vazias nas Câmaras';
        this.idBox = 6;
        break;
      case 7:
        this.nameBox = 'Relatório de Pesos por Produtos nas Câmaras';
        this.idBox = 7;
        break;
      case 8:
        this.nameBox = 'Relatório de Produtos Embarcados';
        this.idBox = 8;
        break;
      case 9:
        this.nameBox = 'Relatório de Embarques por Período';
        this.idBox = 9;
        break;
      case 10:
        this.nameBox = 'Relatório de Paradas, Emb. e Desembarques';
        this.idBox = 10;
        break;
      case 11:
        this.nameBox = 'Relatório Entrada/Saida de Túnel de Congelamento';
        this.idBox = 11;
        break;
      case 12:
        this.nameBox = 'Relatório de Cargas Embarcadas';
        this.idBox = 12;
        break;
      case 13:
        this.nameBox = 'Reagendamento de Cargas';
        this.idBox = 13;
        break;
      case 14:
        // Não Possui tela de Parametros no Delphi mais coloquie pra rodar melhor filtrando pela DATA
        this.nameBox = 'Relação de Pallets Estocados na Câmara C';
        this.idBox = 14;
        break;
      case 15:
        // Não Possui tela de Parametros
        this.nameBox = 'Relatório de Países Destinos (Relação de Habilitações)';
        this.idBox = 15;
        break;
      case 16:
        // Não Possui tela de Parametros
        this.nameBox = 'Relação de Produtos por Nº de SIF';
        this.idBox = 16;
        break;
      default:
        break;
      
    }
    this.SetIdBox({id : this.idBox});
  }

   public async  listReportsByUserPermission(): Promise<void> {
      const listReports: Array<any>=[];
      switch(this.isSysAdmin){
        case true: 
          this.itemsReportsByUser = this.itemsReports;
          break;
        default: 
          this.itemsReportsByUser.push({ icon: 'mdi-file-chart', title: 'Relatórios', items: null});
          this.itemsReports[0].items.forEach(reportItems => {
            if(reportItems.show ===  this.loginUser || reportItems.show === ''){ 
              listReports.push(reportItems);
            }
          });
          this.itemsReportsByUser[0].items = listReports;
        break;
      }
  }
  
  public closeModal(): void {
    this.modalReports = false;
  }

  async mounted(){
    if(this.loginUser === null || this.isSysAdmin === null){
       await this.reloadUser({
        token: window.localStorage.token
      });
      this.listReportsByUserPermission();
    }
    if(this.itemsReportsByUser.length === 0){
      this.listReportsByUserPermission();
    }
  }
}
</script>