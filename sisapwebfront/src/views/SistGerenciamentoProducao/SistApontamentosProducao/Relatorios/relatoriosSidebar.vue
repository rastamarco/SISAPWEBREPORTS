<template>
<div>
  <v-list dense>
    <v-list-group v-for="item in items" :key="item.title" v-model="item.active" :prepend-icon="item.icon" no-action>
      <template v-slot:activator>
        <v-list-item-content>
          <v-list-item-title v-text="item.title"></v-list-item-title>
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
        <InputModals v-bind="{ nameBox }" v-on="{ closeModal }" />
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
import InputModals from './inputModal.vue';
@Component({
  components: {
    InputModals
  }
})
export default class ApontamentosRelatorios extends Vue {
  /* eslint-disable indent */
  private modalReports: boolean = false;
  private nameBox: any = null;
  private items: Array < any > = [{
    icon: 'mdi-file-chart',
    title: 'Relatórios',
    items: [{
        title: 'Apontamento da Produção'
      },
      {
        title: 'Apontamento de Resfriados'
      },
      {
        title: 'Apontamento de Refeitório'
      },
    ]
  }, ]

  public selectReport(item: any): void {

    switch (item.title) {
      case 'Apontamento de Resfriados':
        this.nameBox = 'Impressão de Apontamento de Resfriados';
        break;
      default:
        break;
    }
  }

  public closeModal(): void {
    this.modalReports = false;
  }
}
</script>
