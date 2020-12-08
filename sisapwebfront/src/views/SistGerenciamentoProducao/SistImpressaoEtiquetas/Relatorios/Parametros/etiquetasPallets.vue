<template>
  <v-row>
      <v-col cols="12" sm="6" md="4" class="inputs-text">
            <span style="font-size: 14px; opacity: 0.7;">Fornecedores</span>
            <span style="font-size: 14px; opacity: 0.7;">Nº da Placa</span>
            <span style="font-size: 14px; opacity: 0.7;">Nº da Nota</span>
      </v-col>
      <v-col cols="12" sm="6" md="7" class="inputs">
          <v-autocomplete v-model="fornecedor" filled  :items="items" dense label="Fornecedores" style="cursor: pointer;padding-top:20px;" hide-details></v-autocomplete>
          <v-text-field v-model="nrPlaca" filled  v-mask="'###-####'" placeholder="###-####"  hide-details ></v-text-field>
          <v-text-field v-model="nrNota" filled  placeholder="#######"  hide-details></v-text-field>
      </v-col>
      <!-- <v-col cols="12" sm="6" md="12" class="inputs">
         <span style="font-size: 14px; opacity: 0.7;padding-top:8px;">Nº da Placa</span>
         <v-text-field v-model="nrNota" filled  placeholder="#######"  hide-details></v-text-field>
      </v-col> -->
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
export default class EtiquetasPallets extends Vue {
  @Prop() clearFields!: any; 

  private items: any =  ['foo', 'bar', 'fizz', 'buzz'];
  private nrPlaca: any = null;
  private fornecedor: any = null;
  private nrNota: any = null;
  
}
</script>
<style scoped>
.inputs{
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding:0;
}

.inputs-text{
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
</style>