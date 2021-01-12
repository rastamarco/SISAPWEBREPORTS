<template>
  <v-row>
      <v-col cols="12" sm="10" md="10" style="padding-top:15%;padding-left:20%;">
        <span>Informe o número do Pallet: </span>
        <v-text-field dense outlined v-model="nrPallet" placeholder="ex: 01234569" type="number" min="0" style="padding-top: 10px;"> </v-text-field>
        <span style="color:black;opacity: 0.6;">* Para esse relatório só é necessário o nº do pallet. </span>
      </v-col>
    <v-btn absolute rounded text bottom left color="primary" @click="CloseModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" :disabled="!canPrint()" style="text-transform: none;">
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
export default class FormacaoPallet extends Vue {
  @Prop() clearFields!: any; 
  
  @Action noShowReport
  @Action reportFormacaoPallets

  @Getter showReport

  private nrPallet: any = null;
  
  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.nrPallet = null;
  }
  
  public canPrint(): boolean {
    if(this.nrPallet !== null){
      return true;
    } else 
      return false;
  }

  public async Print(): Promise<void> {
    await this.reportFormacaoPallets({ 
      idReport: 1,
      nrPallet: this.nrPallet,
      reportModule: 2
    });
    this.CloseModal();
  }

  public async CloseModal(): Promise<void> {
    this.$emit('closeModal');
  }

}
</script>