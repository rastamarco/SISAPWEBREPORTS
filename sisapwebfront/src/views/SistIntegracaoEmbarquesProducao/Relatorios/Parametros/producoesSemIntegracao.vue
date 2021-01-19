<template>
      <v-row >
      <v-col cols="12" sm="6" md="12" class="content">
        <div class="input">
        <span style="padding-top:10px;margin-left:25px;">Data</span>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;width:225px;margin-left: 30px;"></v-text-field>
          </template>
          <v-date-picker v-model="date" locale="pt" @input="menu = false" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
        </div>
        <div class="input">
          <span style="padding-top: 10px;">Turno</span>
          <v-radio-group v-model="rgShift"  column dense  style="transform: scale(0.9);">
            <v-radio label="1º Turno" value="1"></v-radio>
            <v-radio label="2º Turno" value="2" ></v-radio>
            <v-radio label="Todos" value="3" ></v-radio>
          </v-radio-group>
        </div>
      </v-col>
      <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" style="text-transform: none;">
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
  @Prop() clearFields!: any; 

  @Action noShowReport
  @Action reportProdSemIntegracao
  
  @Getter showReport
  @Getter filialName
  @Getter box

  private menu: boolean = false;
  private menu2: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
  private dateMax = new Date().toISOString().substr(0, 10);
  private rgShift: any = '3';
  private rgPeriod: any = '3'; 

  public Clear(): void{
    this.date = new Date().toISOString().substr(0, 10);
    this.rgShift = '3';
    this.rgPeriod = '3';
  }

   @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.Clear();
    this.$emit('resetClearFields');
  }

   @Watch('date')
   public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
     this.dateFormatted = this.formatDate(value);
   }

   public setDate(): void {
     this.menu = false;
   }

   public addDay(date: string): any {
     if (!date) return null;
     const [year, month, day] = date.split('-');
     return `${year}/${month}/${parseInt(day)+1}`;
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

   public async Print(): Promise < void > {
     if(this.showReport === true){
       await this.noShowReport({show: false});
     }
     await this.ReportProdutosSemIntegracao();
     this.closeModal();
   }

   public async ReportProdutosSemIntegracao(): Promise < void > {
     let initDate = '';
     let finalDate = '';
     initDate = this.date;
     finalDate = this.addDay(this.date);
     switch(this.rgShift){
     case '3':
       await this.reportProdSemIntegracao({ initialDate: initDate, filialName: this.filialName, idReport: 1, reportModule: 2});
       break;
     default:
       await this.reportProdSemIntegracao({ initialDate: initDate, shift: this.rgShift, filialName: this.filialName, idReport: 2, reportModule: 2});
       break;
     }
     
   }

   public closeModal(): void {
     this.Clear();
     this.$emit('closeModal');
   }

} 
</script>
<style scoped>
.content{
  display: flex;
  flex-direction: row;
  align-items: flex-start;
  justify-content:center;
}

.content .input{
  display: flex;
  flex-direction: column;
  width:55%;
  padding-left: 8%;
  padding-top: 10%;
}
</style>  