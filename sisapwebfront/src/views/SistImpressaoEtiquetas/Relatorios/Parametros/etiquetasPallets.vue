<template>
  <v-row>
      <v-col cols="12" sm="6" md="12" class="inputs-text">
          <div class="texts">
            <span style="font-size: 14px; opacity: 0.7;">Fornecedores</span>
            <v-autocomplete v-model="provider" filled rounded 
              :items="allProviders" 
              item-text="desc_Fornecedor" 
              item-value="cod_Fornecedor" 
              dense 
              label="Fornecedores " style="padding-top:5px;" hide-details no-data-text="Fornecedor não Encontrado!"></v-autocomplete>
              <span style="font-size: 10px; opacity: 0.7;color: red;" v-if="loadingProviders === false">Não foi possível obter a lista de Fornecedores</span>
          </div>
          <div class="texts">
          <span style="font-size: 14px; opacity: 0.7;padding-top:3px;">Nº da Placa</span>
          <v-text-field v-model="nrPlaca" filled  rounded v-mask="'XXX-####'" type="text" placeholder="###-####" hide-details  ></v-text-field>
          </div>
          <div class="texts">
            <span style="font-size: 14px; opacity: 0.7;padding-top:3px;">Nº da Nota</span>
            <v-text-field v-model="nrNote" filled rounded placeholder="############" hide-details></v-text-field>
          </div>
      </v-col>
      <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
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
export default class EtiquetasPallets extends Vue {
  @Prop() clearFields!: any; 

  @Action getAllProviders
  @Action noShowReport
  @Action reportEtiquetaPallet

  @Getter allProviders
  @Getter loadingProviders
  @Getter filialName
  @Getter showReport

  private nrPlaca: string = '';
  private provider: any = null;
  private nrNote: any = null;
  private providerName: any = null;
  private idProvider: any = null;

  public Clear(): void{
    this.nrPlaca = '';
    this.provider = null;
    this.nrNote = null;
  }
  
  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.Clear();
    this.$emit('resetClearFields');
  }

  @Watch('nrPlaca')
  public async onPropertyChangedNrPlaca(value: any, oldValue: any): Promise < void > {
    this.nrPlaca = value.toUpperCase();
  }

  @Watch('provider')
  public async onPropertyChangedProvider(value: any, oldValue: any): Promise < void > {
    for(let i = 0; i< this.allProviders.length; i++) {
      if(this.allProviders[i].cod_Fornecedor === value){
        this.providerName = this.allProviders[i].desc_Fornecedor;
      }
    }
    this.getProvider(value, this.providerName);
  }

  public getProvider(idProvider: any, providerName: any): void{
    this.idProvider = idProvider;
    this.providerName = providerName;
  }

  public toUpper(placa: any): any {
    return placa.toUpperCase();
  }

  public closeModal(): void {
    this.$emit('closeModal');
  }

  public canPrint(): boolean {
    if (this.nrPlaca.length === 8 && this.idProvider !== null && this.nrNote !== null){
      return true;
    } else { 
      return false;
    }
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    await this.EtiquetasPallets();
    this.closeModal();
  }

  public async EtiquetasPallets(): Promise<void>{
    await this.reportEtiquetaPallet({
      idProvider: this.idProvider, providerName: this.providerName, 
      nrPlaca: this.nrPlaca, nrNote: this.nrNote, idReport: 1, reportModule: 5 });
  }

  async mounted(){
    if (this.allProviders === null){
      await this.getAllProviders({filialName: this.filialName});
    }
    
  }
  
}
</script>
<style scoped>

.inputs-text{
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding:0;
}

.inputs-text .texts{
  display: flex;
  flex-direction: column;
  width:50%;
  height:100%;
}
</style>