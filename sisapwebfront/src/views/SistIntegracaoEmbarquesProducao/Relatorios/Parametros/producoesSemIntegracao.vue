<template>
  <v-row>
    
    <v-btn absolute rounded text bottom left color="primary"  style="text-transform: none;">
      Cancelar
      </v-btn>
    <v-btn absolute rounded bottom right color="primary"   style="text-transform: none;">
      <v-icon>mdi-printer</v-icon>
      Imprimir
    </v-btn> 
  </v-row>
</template>

<script lang="ts">
import {
  Component,
  Vue,
  Prop,
  Watch
} from 'vue-property-decorator';

import {
  Action, Getter
} from 'vuex-class';
@Component
export default class ProducoesSemIntegracao extends Vue {
} 
</script>