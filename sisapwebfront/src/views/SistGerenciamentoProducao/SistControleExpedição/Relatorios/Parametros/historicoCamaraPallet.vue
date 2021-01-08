<template>
  <v-row >
      <v-col cols="12" sm="6" md="5" class="content">
        <span style="font-size: 17px;">Selecione a Câmara:</span>
        <div class="content-chamber" v-if="!isLoadingChambers" >  
          <v-select dense :items="allChambers" v-model="idChambers" item-value="cod_camara" placeholder="Selecione" 
            item-text="cod_camara" outlined style="padding-top:5px;" append-icon="mdi-format-columns"></v-select>
          <small v-if="allChambers === null" style="color: red;">Não foi possível obter a lista de Câmaras.</small>
        </div>
        <div class="loading" v-if="isLoadingChambers">
          <v-progress-circular
              :size="50"
              indeterminate
              color="primary"
            ></v-progress-circular>
        </div>
        <span style="font-size: 17px;">Informe o Produto: <br><small>(Em branco = Vazio)</small></span>
        <div class="content-chamber">  
           <v-text-field outlined hide-details v-model="codSicop" placeholder="ex: 12345" dense style="padding-top: 7px;" type="number" min="0" @blur="GetProductName(codSicop)"></v-text-field>
        </div>
      </v-col>
      <v-col cols="12" sm="6" md="5" class="content">
        <span style="font-size: 17px;">Situação: </span>
        <div class="content-chamber-left">  
          <v-select dense :items="itemStatus" v-model="status" placeholder="Selecione" outlined style="padding-top:5px;" append-icon="mdi-circle-double"></v-select>
        </div>
        <div class="content-loading">
          <v-progress-circular v-if="isGettingProduct" :size="25" indeterminate color="primary" class="input-loading"></v-progress-circular>
          <small  class="input-loading" v-if="!isGettingProduct">{{productDescription}}</small>
        </div>
      </v-col>
      <v-col cols="12" sm="6" md="5" class="content-date">
        <span style="font-size: 17px;">Data Inicial: </span>
        <div class="content-chamber">  
          <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px" >
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;transform: scale(0.8);"></v-text-field>
            </template>
            <v-date-picker v-model="date" @input="setDate()" locale="pt" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
        </div>
      </v-col>
      <v-col cols="12" sm="6" md="5" class="content-date">
        <span style="font-size: 17px;">Data Final: </span>
        <div class="content-chamber">  
         <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px" >
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted2)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;transform: scale(0.8);" ></v-text-field>
            </template>
          <v-date-picker v-model="date2" @input="setDate()" locale="pt" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
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
export default class HistoricoCamaraPallet extends Vue {
  @Prop() clearFields!: any;

  @Action getProductName
  @Action getChambersByFilial

  @Getter filialName
  @Getter allChambers
  @Getter productName

  private idChambers: any = null;
  private isLoadingChambers: boolean = false;
  private itemStatus: any = ['Armazenado(s)', 'Expedido(s)', 'Excluído(s)'];
  private status: any = null;
  private codSicop: any = null;
  private menu: boolean = false;
  private menu2: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  private date2 = new Date().toISOString().substr(0, 10);
  private dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10))
  private dateMax = new Date().toISOString().substr(0, 10);
  private ignore: boolean = false;
  private productDescription: any = null;
  private isGettingProduct: boolean = false;

  public clear(): void{
    this.idChambers = null;
    this.status = null;
    this.codSicop = null;
    this.status = null;
    this.ignore = false;
    this.date = new Date().toISOString().substr(0, 10);
    this.dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
    this.date2 = new Date().toISOString().substr(0, 10);
    this.dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10));
    this.productDescription = null;
    this.isGettingProduct = false;
  }

  @Watch('idChambers')
  public async onPropertyChangedsChambers(value: any, oldValue: any): Promise < void > {
    this.$emit('getIdChambers', value);
  }

  @Watch('codSicop')
  public async onPropertyChangedsSicop(value: any, oldValue: any): Promise < void > {
    if(value && value !== null){
      this.isGettingProduct = true;
    }
    this.$emit('getCodSicop', value);
  }

  @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.clear();
    this.$emit('resetClearFields');
  }

   @Watch('date')
  public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);
    this.$emit('getInitialDate', value);
  }

  @Watch('date2')
   public async onPropertyChangedsDate2(value: any, oldValue: any): Promise < void > {
     this.dateFormatted2 = this.formatDate(value);
     this.$emit('getEndDate', value);
   }

  @Watch('status')
  public async onPropertyChangedsStatus(value: any, oldValue: any): Promise < void > {
    this.$emit('GetStatus', value);
  }

  public setDate(): void {
    this.menu = false;
    this.menu2 = false;
  }

  public formatDate(date: string): any {
    if (!date) return null;
    const [year, month, day] = date.split('-');
    return `${day}/${month}/${year}`;
  }

  public parseDate(date: any): any {
    if (!date) return null;

    const [day, month, year] = date.split('/');
    return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
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

  public InitialParameters(): void{
    this.$emit('getInitialDate', this.date);
    this.$emit('getEndDate', this.date);
  }

  async mounted() {
    if (this.allChambers === null) {
      this.isLoadingChambers = true;
      await this.getChambersByFilial({filial: this.filialName });
      this.isLoadingChambers = false;
    }
    this.InitialParameters();
  }
} 
</script>
<style scoped>
.content{
  display: flex;
  flex-direction: column;
  margin-left: 5%;
}

.content .content-chamber{
  display:flex;
  flex-direction: row;
  width: 90%;
  flex-wrap: wrap;
}

.content .content-chamber-left{
  display:flex;
  flex-direction: row;
  width: 90%;
  flex-wrap: wrap;
  height: 70%;
}


.content-date{
  display: flex;
  flex-direction: column;
  margin-left: 5%;
}

.content .content-loading{
  color: #20A8D8;
}

</style>