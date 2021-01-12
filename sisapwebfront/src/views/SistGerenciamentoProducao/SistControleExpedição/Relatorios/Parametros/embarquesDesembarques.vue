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
            <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;width: 230px;"></v-text-field>
          </template>
          <v-date-picker v-model="date2" locale="pt" @input="menu2 = false" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
        </div>
      </v-col>
      <v-col cols="12" sm="6" md="12" class="content">
        <div class="input">
          <span style="margin-left:10%;">Nr. Conferente</span>  
          <v-text-field v-model="codSicop" outlined placeholder="ex: 123456" dense  hide-details style="padding-top: 6px;margin-left:20%;"></v-text-field>
          <small style="margin-left:20%;color: red;font-size: 11px;" v-if="employeValid">Matrícula Inválida!<br> Informe uma matrícula correta</small>
        </div>
        <div class="input" v-if="idBox === 10">
          <span>Turno</span>  
          <v-radio-group v-model="rgShift"  row  dense  style="margin:0;transform: scale(0.9);">
          <v-radio label="1º Turno" value="1"></v-radio>
          <v-radio label="2º Turno"  value="2" ></v-radio>
          <v-radio label="3º Turno" value="3" ></v-radio>
          <v-radio label="Todos" value="4"></v-radio>
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
export default class RelatorioEmbarquesPeriodo extends Vue {
  @Prop() clearFields!: any; 

  @Action GetEmployeRegister
  @Action ReportEmbarquesPeriodo
  @Action ReportEmbarquesDesembarque
  @Action noShowReport

  @Getter showReport
  @Getter validEmploye
  @Getter filialName
  @Getter box

  private codSicop: any = null;
  private rgShift: any = '4';
  private menu: boolean = false;
  private menu2: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
  private date2 = new Date().toISOString().substr(0, 10);
  private dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10));
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateToSend: any = null;
  private employeValid: boolean = false;

  public Clear(): void{
    this.dateFormatted = new Date().toISOString().substr(0, 10);
    this.date = new Date().toISOString().substr(0, 10);
    this.dateFormatted2 = new Date().toISOString().substr(0, 10);
    this.date2 = new Date().toISOString().substr(0, 10);
    this.codSicop = null;
    this.rgShift = '4';
    this.employeValid = false;
  }

   @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.Clear();
    this.$emit('resetClearFields');
  }

   @Watch('date')
   public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
     this.dateFormatted = this.formatDate(value);
     this.$emit('getInitialDate', value);
   }

   @Watch('date2')
   public async onPropertyChangedsDateEnd(value: any, oldValue: any): Promise < void > {
     this.dateFormatted2 = this.formatDate(value);
     this.$emit('getEndDate', value);
   }

   @Watch('codSicop')
   public async onPropertyChangedsConferente(value: any, oldValue: any): Promise < void > {
     this.$emit('getCodSicop', value);
     if(value.length > 3){
       await this.GetEmployeRegister({filialName: this.filialName, codSicop: value});
       if(this.validEmploye.coD_MATRICULA === null || this.validEmploye.coD_MATRICULA === 0){
         this.employeValid = true;
       } else { 
         this.employeValid = false;
       }
     }
   }

   @Watch('rgShift')
   public async onPropertyChangedsRgShift(value: any, oldValue: any): Promise < void > {
     this.$emit('getShift', value);
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

   public async Print(): Promise < void > {
     if(this.showReport === true){
       await this.noShowReport({show: false});
     }
     switch (this.box){
     case 9: 
       await this.ReportEmbarquesPeriodos();
       break;
     case 10: 
       await this.ReportEmbarquesDesembarques();
     }
   }

   public async ReportEmbarquesPeriodos(): Promise<void> {
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
     if(this.codSicop === null){
       await this.ReportEmbarquesPeriodo({initialDate: initDate, endDate: finalDate, idReport: 54, reportModule: 2 });
     }else { 
       await this.ReportEmbarquesPeriodo({initialDate: initDate, endDate: finalDate, nrConferente: this.codSicop, idReport: 55, reportModule: 2 });
     }
     this.closeModal();
   }

   public async ReportEmbarquesDesembarques(): Promise<void> {
     // if(this.)
    
     await this.ReportEmbarquesDesembarque({});
   }

   public closeModal(): void {
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
  padding-top:5%;
}
</style>