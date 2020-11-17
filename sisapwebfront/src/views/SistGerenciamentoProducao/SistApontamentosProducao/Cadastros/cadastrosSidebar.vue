<template>
<v-list dense>
  <v-list-group v-for="item in items" :key="item.title" v-model="item.active" :prepend-icon="item.icon" no-action dense>
    <template v-slot:activator>
      <v-list-item-content>
        <v-list-item-title v-text="item.title"></v-list-item-title>
      </v-list-item-content>
    </template>
    <v-list-item v-for="child in item.items" :key="child.title" @click="selectReport(child)">
      <v-list-item-content>
        <v-list-item-title v-text="child.title"></v-list-item-title>
      </v-list-item-content>
    </v-list-item>
  </v-list-group>
</v-list>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
// import {
//   Action,
// } from 'vuex-class';
@Component
export default class ApontamentosCadastros extends Vue {
  private items: Array < any > = [{
    icon: 'mdi-file-document-box-plus-outline',
    title: 'Cadastros',
    /* eslint-disable indent */
    items: [{
        title: 'Produtos'
      },
      {
        title: 'Exportar p/ WMS'
      },
      {
        title: 'Funcionário'
      }
    ]
  }, ]

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  public async selectReport(item: any): Promise < void > {
    console.log(item);
  }
}
</script>
