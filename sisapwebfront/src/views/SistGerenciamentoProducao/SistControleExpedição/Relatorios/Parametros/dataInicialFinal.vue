<template>
      <v-row >
      <v-col cols="12" sm="6" md="12" class="content">
        <div class="input">
        <span style="padding-top: 15px;margin-left:25px;">Data Inicial</span>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;width:225px;margin-left: 30px;"></v-text-field>
          </template>
          <v-date-picker v-model="date" locale="pt" @input="menu = false" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
        </div>
        <div class="input">
        <span style="padding-top: 15px;">Data Final</span>
        <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted2)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;width: 230px;"></v-text-field>
          </template>
          <v-date-picker v-model="date2" locale="pt" @input="menu2 = false" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
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
export default class DataInicialDataFinal extends Vue {
  @Prop() clearFields!: any; 

  @Action noShowReport
  @Action reportCargasEmbarcadas
  @Action ReportPalletsEmCamaraC
  
  @Getter showReport
  @Getter filialName
  @Getter box

  private menu: boolean = false;
  private menu2: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
  private date2 = new Date().toISOString().substr(0, 10);
  private dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10));
  private dateMax = new Date().toISOString().substr(0, 10);

  public Clear(): void{
    this.date = new Date().toISOString().substr(0, 10);
    this.date2 = new Date().toISOString().substr(0, 10);
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

   @Watch('date2')
   public async onPropertyChangedsDateEnd(value: any, oldValue: any): Promise < void > {
     this.dateFormatted2 = this.formatDate(value);
   }

   public setDate(): void {
     this.menu = false;
     this.menu2 = false;
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
     switch (this.box){
     case 12: 
       await this.ReportCargasEmbarcadas();
       break;
     case 14: 
       await this.ReportPalletsCamaraC();
       break;
     }
     this.closeModal();
   }

   public async ReportCargasEmbarcadas(): Promise<void> {
     let initDate = '';
     let finalDate = '';
     if(this.date === this.date2) {
       initDate = this.date;
       finalDate = this.date;
     } else if(this.date > this.date2) {
       this.$swal('Ops!', 'A data Final é menor que a data Inicial.','warning');
       return;
     } else if(this.date < this.date2) {
       initDate = this.date;
       finalDate = this.date2;
     }
     await this.reportCargasEmbarcadas({ initialDate: initDate, endDate: finalDate, idReport: 61, reportModule: 2});
   }

   public async ReportPalletsCamaraC(): Promise < void > {
     await this.ReportPalletsEmCamaraC({ initialDate: this.date, endDate: this.date2, idReport: 64, reportModule: 2 });
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
  align-items: center;
  justify-content:center;
}

.content .input{
  display: flex;
  flex-direction: column;
  width:55%;
  align-items: flex-start;
  padding-left: 5%;
  padding-top:15%;
}
</style>  