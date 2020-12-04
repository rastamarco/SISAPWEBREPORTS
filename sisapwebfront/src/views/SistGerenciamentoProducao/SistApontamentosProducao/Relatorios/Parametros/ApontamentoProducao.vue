<template>
  <v-row>
    <v-col cols="12" sm="6" md="4">
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <span class="text-title">Data</span>
            <v-text-field id="teste" v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" readonly outlined hide-details dense v-bind="attrs" v-on="on" class="date-input"></v-text-field>
          </template>
          <v-date-picker v-model="date" @input="setDate(date)" locale="pt" min="1950-01-01" :max="dateMax"></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" sm="6" md="4">
        <span class="text-title">Turno </span>
        <v-radio-group v-model="turnoGroup" style="padding-left: 30px;">
          <v-radio v-for="n in 4" :key="n" :label="getShifts(n)" :value="n"></v-radio>
        </v-radio-group>
      </v-col>
      <v-col cols="12" sm="6" md="4" >
        <span class="text-title" v-if="showPeriodApontamento()" >Período</span>
        <v-radio-group v-model="periodoGroup" style="padding-left: 30px;" v-if="showPeriodApontamento()">
          <v-radio v-for="n in 3" :key="n" :label="getPeriods(n)" :value="n"></v-radio>
        </v-radio-group>
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
export default class ApontamentoProducao extends Vue {

  @Prop() clearFields!: any;

  @Getter filialName
  @Getter loginUser
  @Getter userFeatures

  private menu: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  private dateToSend: any = null;
  private turnoGroup: any = 1;
  private periodoGroup: any = 1;


  public clear(): void {
    this.date = new Date().toISOString().substr(0, 10);
    this.turnoGroup = 1;
    this.periodoGroup = 1;
    this.InitialParameters();
  }

  @Watch('clearFields')
  public async onPropertyChangedClearFields(value: any, oldValue: any): Promise < void > {
    this.clear();
    this.$emit('resetClearFields');
  }

  public getShifts(n: any): any{
    switch(n){
    case 1: 
      return '1º Turno';
    case 2:
      return '2º Turno';
    case 3: 
      return '3º Turno';
    case 4:
      return 'Todos';  
    }
  }

  public getPeriods(n: any): any{
    switch(n){
    case 1: 
      return '1º Período';
    case 2:
      return '2º Período';
    case 3: 
      return 'Todos';
    }
  }

  public setDate(data: any): void {
    this.dateToSend = data;
    this.$emit('getDate', this.dateToSend);
    this.menu = false;
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

  @Watch('date')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);
    this.setDate(value);
  }

  @Watch('turnoGroup')
  public async onPropertyChangedTurnoGroup(value: any, oldValue: any): Promise <void>{
    this.$emit('getShift', value);
  }

  @Watch('periodoGroup')
  public async onPropertyChangedPeriodoGroup(value: any, oldValue: any): Promise <void>{
    this.$emit('getPeriod', value);
  }

  public showPeriodApontamento(): boolean{
    if (this.filialName === 'UIC' && this.userFeatures.isApontamento === true) {
      return false;
    } else { 
      return true;
    }
  }

  public InitialParameters(): void{
    this.setDate(this.date);
    this.$emit('getShift', this.turnoGroup);
    this.$emit('getPeriod', this.periodoGroup);
  }

  async mounted(){
    this.InitialParameters();
  }

}

</script>