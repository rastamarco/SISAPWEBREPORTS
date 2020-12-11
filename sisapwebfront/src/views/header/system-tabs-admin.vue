<template>
<v-tabs centered background-color="primary" v-model="tabs">
  <v-tab v-for="link in links" :key="link" style="text-transform: none;">
    {{ link }}
  </v-tab>
</v-tabs>
</template>

<script lang="ts">
import {
  Component,
  Vue,
  Watch
} from 'vue-property-decorator';
@Component({})
export default class TabsSystems extends Vue {
  private links = [
    'Home', 'Apontamento', 'Expedição', 'Ctrl. de Qualidade', 'Ctrl. de Produção', 'Imp. Etiquetas', 
    'SIF', 'Apont. Paradas'];
  private tabs: any = null;
  
  @Watch('tabs')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.$emit('getSystem', value);
  }

}
</script>
