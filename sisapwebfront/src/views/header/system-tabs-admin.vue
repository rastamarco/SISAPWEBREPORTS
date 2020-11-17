<template>
<v-tabs centered background-color="primary">
  <v-tab v-for="link in links" :key="link" style="text-transform: none;">
    {{ link }}
  </v-tab>
</v-tabs>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
@Component({})
export default class TabsSystems extends Vue {
  private links = ['Apontamento', 'Expedição', 'C. de Qualidade', 'C. de Ovos', 'C. de Produção',
    'RH', 'SIF', 'Usuários', 'Filiais', 'Cartão Ponto', 'Manutenção', 'Docas Emb.'
  ];
}
</script>
