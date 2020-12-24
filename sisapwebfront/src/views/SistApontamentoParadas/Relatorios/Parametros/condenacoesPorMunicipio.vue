<template>
  <v-row>
      <v-col cols="12" sm="6" md="10" style="padding-left: 10%;">
        <div class="box"> 
            <span class="text-title" >Selecione ou digite o nome do Município:</span>
            <v-checkbox v-model="all" label="Todos" dense class="checks" hide-details></v-checkbox>
        </div>
         <v-autocomplete v-model="city" filled  
              :items="allCities" 
              item-text="desc_Municipio" 
              item-value="cod_Municipio" 
              dense 
              label="Municípios " hide-details no-data-text="Município não Encontrado!" :disabled="all === true" ></v-autocomplete>
              <span style="font-size: 10px; opacity: 0.7;" v-if="loadingCities === false">Não foi possível obter a lista de Municípios</span>
      </v-col> 
      <v-col cols="12" sm="6" md="5" style="padding-left: 10%;">
        <span class="text-title">Data Inicial </span>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top: 8px;"></v-text-field>
            </template>
            <v-date-picker v-model="date" locale="pt" min="1950-01-01" @input="SendDate(date)" :max="dateMax" ></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" sm="6" md="5" style="padding-left: 10%;">
        <span class="text-title" >Data Final</span>
        <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted2)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top: 8px;"></v-text-field>
            </template>
            <v-date-picker v-model="date2" locale="pt" min="1950-01-01" @input="SendDate2(date2)" :max="dateMax" ></v-date-picker>
        </v-menu>
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
export default class CondenacoesPorMunicipio extends Vue {
  @Prop() clearFields!: any; 

  @Action getAllCities

  @Getter allCities
  @Getter loadingCities
  @Getter filialName

  private date: any = new Date().toISOString().substr(0, 10) ;
  private menu: boolean = false;
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  private date2: any = new Date().toISOString().substr(0, 10) ;
  private menu2: boolean = false;
  private dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10))
  private city: any = null;
  private cityName: any = null;
  private all: boolean = false;

  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.date = new Date().toISOString().substr(0, 10);
    this.date2 = new Date().toISOString().substr(0, 10);
    await this.initialParameters();
    this.$emit('resetClearFields');
    
  }

 @Watch('all')
  public async onPropertyChangedAll(value: any, oldValue: any): Promise < void > {
    this.$emit('allSelected', value);
  }

  @Watch('date')
 public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
   this.dateFormatted = this.formatDate(value);
 }

  @Watch('date2')
  public async onPropertyChangedsDate2(value: any, oldValue: any): Promise < void > {
    this.dateFormatted2 = this.formatDate(value);
  }

  @Watch('city')
  public async onPropertyChangedsCity(value: any, oldValue: any): Promise < void > {
    this.$emit('getCity', value);
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

  public SendDate(date: any): void {
    this.menu = false;
    this.$emit('getInitialDate', date);
  }

  public SendDate2(date: any): void {
    this.menu2 = false;
    this.$emit('getEndDate', date);
  }

  public initialParameters(): void{
    this.SendDate(this.date);
    this.SendDate2(this.date2);
  }

  async mounted(){
    await this.initialParameters();
    if (this.allCities === null){
      await this.getAllCities({filialName: this.filialName });
    }
    
  }
}
</script>
<style scoped>
.box{
  display:flex;
  flex-direction: row;
  justify-content:center;
  align-items:center;
  width:100%;
}

.box .text-title{
  width:80%;
}

.box .checks{
  width:20%;;
  margin:0;
}
</style>