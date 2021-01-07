<template>
      <v-row >
      <v-col cols="12" sm="6" md="12" class="content">
        <br>
        <span>Produto</span>
        <div class="product-input">  
          <v-text-field v-model="product" filled dense type="number" min="0" @blur="GetProductName(product)" hide-details class="input-text"></v-text-field>
          <v-progress-circular v-if="isGettingProduct" :size="25" indeterminate color="primary" class="input-loading"></v-progress-circular>
          <small  class="input-loading" v-if="!isGettingProduct">{{productDescription}}</small>
        </div>
        <br>
        <span>Número do Pallet</span>
        <div class="product-input">  
          <v-text-field v-model="nrPallet" filled dense type="number" min="0" @blur="GetPalletStatus(nrPallet)" hide-details class="input-text"></v-text-field>
          <v-progress-circular v-if="isGettingPallet" :size="25" indeterminate color="primary" class="input-loading"></v-progress-circular>
          <small  class="input-loading" v-if="!isGettingPallet">{{palletStatus}}</small>
        </div>
        <v-checkbox
              v-model="positions"
              label="Listar apenas posições disponíveis(Vazias)"
              dense>
        </v-checkbox>
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
export default class LocalizacaoProduto extends Vue {
  @Prop() clearFields!: any;

  @Action getProductName
  @Action getValidPallet

  @Getter productName
  @Getter validPallet
  @Getter filialName
  
  private product: any = null;
  private productDescription: any = null;
  private palletStatus: any = null;
  private isGettingProduct: boolean = false;
  private isGettingPallet: boolean = false;
  private positions: boolean = false;
  private nrPallet: any = null;
  private okPallet: boolean = false;
  private okProduct: boolean = false;

  public Clear(): void{
    this.nrPallet = null;
    this.product = null;
    this.positions = false;
    this.isGettingProduct = false;
    this.isGettingPallet = false;
    this.productDescription = null;
    this.palletStatus = null;
    this.positions = false;
    this.okPallet = false;
    this.okProduct = false;
  }

  @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.Clear();
    this.$emit('resetClearFields');
  }

  @Watch('product')
  public async onPropertyChangedsProduct(value: any, oldValue: any): Promise < void > {
    if(value && value !== null){
      this.isGettingProduct = true;
    }
  }

  @Watch('nrPallet')
  public async onPropertyChangedsPallet(value: any, oldValue: any): Promise < void > {
    if(value && value !== null){
      this.isGettingPallet = true;
    }
  }

  @Watch('positions')
  public async onPropertyChangedsPositions(value: any, oldValue: any): Promise < void > {
    this.$emit('GetEmptyPositions', true);
  }


  public async GetProductName(product: number): Promise<void>{
    if(product && product !== null){
      await this.getProductName({filialName: this.filialName, productId: product });
      this.isGettingProduct = false;
      if(this.productName === null || this.productName.cod === 0){
        this.productDescription = 'Produto Inválido, informe outro código';
      }else { 
        this.$emit('getCodSicop', product);
        this.productDescription = this.productName.embalagem;
      }
    }else { 
      this.productDescription = '';
    }
  }

  public async GetPalletStatus(pallet: number): Promise<void>{
    if(pallet && pallet !== null){
      await this.getValidPallet({filialName: this.filialName, nrPallet: pallet });
      this.isGettingPallet = false;
      if (this.validPallet === null || this.validPallet.npallet === 0 || this.validPallet.iativo === 'N'){
        this.palletStatus = 'Pallet Inválido ou Não Existe!';
      }else { 
        this.$emit('getNrPallet', pallet);
        this.palletStatus = 'Pallet Válido!';
      }
    }else { 
      this.palletStatus = '';
    }
  }

}
</script>
<style scoped>
.content{
  display: flex;
  flex-direction: column;
  margin-left: 5%;
}

.content .product-input{
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 95%;
}

.content .product-input .input-text{
  padding-top: 5px;
  max-width: 30%;
}

.content .product-input .input-loading{
  margin-left: 3%;
  max-width: 65%;
  color: #20A8D8;
}


</style>