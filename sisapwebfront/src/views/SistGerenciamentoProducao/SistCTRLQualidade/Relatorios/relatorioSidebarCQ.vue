<template>
<div>
  <v-list dense>
    <v-list-group v-for="item in itemsReportsByUser" :key="item.title" v-model="item.active" :prepend-icon="item.icon" no-action>
      <template v-slot:activator>
        <v-list-item-content>
          <v-list-item-title v-text="item.title" ></v-list-item-title>
        </v-list-item-content>
      </template>
      <v-dialog v-model="modalReports" persistent width="40%">
        <template v-slot:activator="{ on, attrs }">
          <v-list-item v-for="child in item.items" :key="child.title" @click="selectReport(child)" v-on="on" v-bind="attrs">
            <v-list-item-content>
              <v-list-item-title v-text="child.title"></v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </template>
        <!-- Aqui via todos os templates de input de parametros para relatórios --> 
        <InputModals v-bind="{ nameBox, idBox }" v-on="{ closeModal }" />
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

import InputModals from './inputModalCQ.vue';

@Component({
  components: {
    InputModals
  }
})
export default class QualidadeRelatorios extends Vue {
  @Action setSelectedIdReport
  @Action reloadUser
  
  @Getter loginUser
  @Getter isSysAdmin
  @Getter filialName
  /* eslint-disable indent */
  private modalReports: boolean = false;
  private nameBox: any = null;
  private idBox: any = null;

  private itemsReports: Array < any > = [{
    icon: 'mdi-file-chart',
    title: 'Relatórios',
    items: [{
        id: 1,
        title: 'Apontamento da Produção',
        show: ''
      },
      {
        id: 2,
        title: 'Qualidade Fomento Mensal',
        show: 'UIAIII'
      }
    ]
  }, ]

  private itemsReportsByUser: Array<any> = [];
  
  public async selectReport(item: any): Promise<void> {
    switch (item.id) {
      case 1:
        // UIA 
        this.nameBox = 'Impressão de Apontamento de Produção';
        this.idBox = 1;
      break;
      case 2:
        this.nameBox = 'Qualidade do Fomento Mensal';
        this.idBox = 2;
      break;
      default:
        break;
    }
  }

  public closeModal(): void {
    this.modalReports = false;
  }

  public async  listReportsByUserPermission(): Promise<void> {
      const listReports: Array<any>=[];
      switch(this.isSysAdmin){
        case true: 
          this.itemsReportsByUser = this.itemsReports;
          break;
        case false: 
          this.itemsReportsByUser.push({ icon: 'mdi-file-chart', title: 'Relatórios', items: null});
          this.itemsReports[0].items.forEach(reportItems => {
            if(reportItems.show ===  this.loginUser || reportItems.show === '' || this.filialName !== 'UIAIII'){ 
              listReports.push(reportItems);
            }
          });
          this.itemsReportsByUser[0].items = listReports;
        break;
      }
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
