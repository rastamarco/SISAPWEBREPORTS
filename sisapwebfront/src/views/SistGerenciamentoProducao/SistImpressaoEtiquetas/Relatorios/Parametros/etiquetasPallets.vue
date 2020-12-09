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
              label="Fornecedores" style="padding-top:5px;" hide-details no-data-text="Fornecedor não Encontrado!"></v-autocomplete>
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

  @Getter allProviders
  
  private nrPlaca: any = null;
  private provider: any = null;
  private nrNote: any = null;

  public Clear(): void{
    this.nrPlaca = null;
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
    this.$emit('getNrPlaca', value);
  }

  @Watch('provider')
  public async onPropertyChangedProvider(value: any, oldValue: any): Promise < void > {
    this.$emit('getProvider', value);
  }

  @Watch('nrNote')
  public async onPropertyChangedNrNote(value: any, oldValue: any): Promise < void > {
    this.$emit('getNote', value);
  }

  public toUpper(placa: any): any {
    return placa.toUpperCase();
  }

  async mounted(){
    await this.getAllProviders();
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