<template>
  <v-row>
    <v-col cols="12" sm="6" md="5" style="padding-left:30px;">
        <span class="text-title">Data Inicial</span>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" readonly outlined hide-details dense v-bind="attrs" v-on="on" class="date-input"></v-text-field>
          </template>
          <v-date-picker v-model="date" @input="setDate(date)" locale="pt" min="1950-01-01" :max="dateMax"></v-date-picker>
        </v-menu>
        <v-checkbox v-model="isPeriod" label="Período" dense></v-checkbox>
        <span >Data Final (Opcional)</span>
        <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px" >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field v-model="date2Formatted" @blur="date2 = parseDate(date2Formatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on"  :disabled="!isPeriod" style="padding-top: 1px;"></v-text-field>
          </template>
          <v-date-picker v-model="date2" @input="setDate2(date2)" locale="pt" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" sm="6" md="6">
        <span class="text-title">Turno </span>
        <v-radio-group v-model="turnoGroup" style="padding-left: 30px;">
          <v-radio v-for="n in 4" :key="n" :label="getShifts(n)" :value="n"></v-radio>
        </v-radio-group>
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
import _ from 'vuetify/es5/components/*';

import {
  Action, Getter
} from 'vuex-class';

@Component
export default class ApontamentoRefeitorio extends Vue {

  @Prop() clearFields!: any;

  @Action reportApontamentoRefeitorio
  @Action noShowReport

  @Getter showReport
  @Getter filialName
  @Getter loginUser
  @Getter userFeatures

  private menu: boolean = false;
  private menu2: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private date2 = new Date().toISOString().substr(0, 10);
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
  private date2Formatted = this.formatDate(new Date().toISOString().substr(0, 10));
  private dateToSend: any = null;
  private dateToSend2: any = null;
  private turnoGroup: any = 1;
  private isPeriod: boolean = false;
  private InitialHour: any = null;
  private EndHour: any = null;

  public clear(): void {
    this.date = new Date().toISOString().substr(0, 10);
    this.date2 = new Date().toISOString().substr(0, 10);
    this.turnoGroup = 1;
    this.isPeriod = false;
    this.dateToSend = null;
    this.dateToSend2 = null;
  }

  @Watch('clearFields')
  public async onPropertyChangedClearFields(value: any, oldValue: any): Promise < void > {
    this.clear();
    this.$emit('resetClearFields');
  }

  public getShifts(n: any): any{
    switch(n){
    case 1: 
      return '1º Turno - (06:00 às 15:30)';
    case 2:
      return '2º Turno - (15:31 às 01:00)';
    case 3: 
      return '3º Turno - (01:00 às 05:59)';
    case 4:
      return 'Todos';  
    }
    
  }

  public setDate(data: any): void {
    let newDate;
    if (this.turnoGroup === 3){
      newDate = this.addDay(data);
    }else{
      newDate = data;
    }
    this.getDate(newDate, this.isPeriod);
    this.menu = false;
  }

  public setDate2(data: any): void {
    let newDate;
    if (this.turnoGroup === 3){
      newDate = this.addDay(data);
    }else{
      newDate = data;
    }
    this.getDateEnd(newDate, this.isPeriod);
    this.menu2 = false;
  }

  public getDate(date: any, isPeriod: any): void{
    this.dateToSend = date;
    this.isPeriod = isPeriod;
  }

  public getDateEnd(date: any, isPeriod: any): void{
    this.dateToSend2 = date;
    this.isPeriod = isPeriod;
  }

  public formatDate(date: string): any {
    if (!date) return null;
    const [year, month, day] = date.split('-');
    return `${day}/${month}/${year}`;
  }

  public addDay(date: string): any {
    if (!date) return null;
    const [year, month, day] = date.split('-');
    return `${year}/${month}/${parseInt(day)+1}`;
  }

  public parseDate(date: any): any {
    if (!date) return null;
    const [day, month, year] = date.split('/');
    return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
  }

  @Watch('date')
  public async onPropertyChangedsInitialDate(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);
    this.setDate(value);
  }

  @Watch('date2')
  public async onPropertyChangedFinalDate(value: any, oldValue: any): Promise < void > {
    this.date2Formatted = this.formatDate(value);
    this.setDate(value);
  }

  @Watch('turnoGroup')
  public async onPropertyChangedTurnoGroup(value: any, oldValue: any): Promise <void>{
    this.setDate(this.date);
    this.setDate2(this.date2);
  }

  public closeModal(): void{
    this.$emit('closeModal');
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    await this.ApontamentoRefeitorio();
    this.closeModal();
  }

  public async ApontamentoRefeitorio(): Promise<void>{
    await this.getShiftHours();
    let newShift;
    if(this.turnoGroup === 4)
      newShift = 'Todos';
    else
      newShift = this.turnoGroup;
    
    switch(this.isPeriod){
    case false: 
      await this.reportApontamentoRefeitorio({
        InitialDate: this.dateToSend+' 00:00:00',
        EndDate: this.dateToSend+' 23:59:59',
        InitialHour: this.InitialHour,
        EndHour:this.EndHour,
        shift: newShift, 
        idReport: 16, 
        reportModule: 1 
      });
      break;
    case true:
      if(this.date < this.date2) {
        await this.reportApontamentoRefeitorio({
          InitialDate: this.dateToSend+' 00:00:00', 
          EndDate: this.dateToSend2+' 23:59:59',
          InitialHour: this.InitialHour,
          EndHour:this.EndHour, 
          shift: newShift, 
          idReport: 16, 
          reportModule: 1 });
      } else {  
        this.$swal('Ops!', 'A Data Inicial informada é maior que a Data Final, corrija-as e tente novamente', 'error');
      }
    }
  }

  public async getShiftHours(): Promise<any>{
    switch(this.turnoGroup){
    case 1: 
      this.InitialHour = '06:00:00';
      this.EndHour = '15:30:59';
      break;
    case 2: 
      this.InitialHour = '15:31:00';
      this.EndHour = '23:59:59';
      break;
    case 3:
      this.InitialHour = '00:00:00';
      this.EndHour = '05:59:59';
      break;
    case 4:
      this.InitialHour = '00:00:00';
      this.EndHour = '23:59:59';
      break;
    }
  }

  mounted() {
    this.dateToSend = this.date;
    this.dateToSend2 = this.date; 
  }
}

</script>