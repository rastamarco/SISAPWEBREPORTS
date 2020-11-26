<template>
      <v-col cols="12" sm="10" md="10" style="padding-top:15%;padding-left:20%;">
        <small>Informe o número do Pallet: </small>
        <v-text-field dense outlined v-model="nrPallet" type="number" min="0" @input="SendNRPallet(nrPallet)"></v-text-field>
      </v-col>
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
export default class FormacaoPallet extends Vue {
  @Prop() clearFields!: any; 

  private nrPallet: any = null;
  
  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.nrPallet = null;
  }
  
  public SendNRPallet(nrPallet: any): void {
    this.$emit('getNrPallet', nrPallet);
  }
}
</script>