<template>
  <v-row>
    <v-col cols="12" sm="6" md="4" >
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <span style="padding-left: 20px;">Data</span>
            <v-text-field id="teste" v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" readonly outlined 
              hide-details dense v-bind="attrs" v-on="on" style="padding-left: 20px;padding-top: 5px;" ></v-text-field>
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
export default class ApontamentoProducao extends Vue {

  @Prop() clearFields!: any;

  @Action reportApontamentoProducao
  @Action noShowReport

  @Getter showReport
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

  public showPeriodApontamento(): boolean{
    if (this.filialName === 'UIC' && this.userFeatures.isApontamento === true) {
      return false;
    } else { 
      return true;
    }
  }

  public closeModal(): void {
    this.$emit('closeModal');
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    await this.ApontamentoProducao();
    this.closeModal();
  }

  public async ApontamentoProducao(): Promise<void>{
    switch(this.filialName){
    case 'UIC':
      if (this.turnoGroup === 4){
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          idReport: 15,
          reportModule: 1
        });
      } else {
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          shift: this.turnoGroup,
          idReport: 14,
          reportModule: 1
        });
      }
      break;
    default:
      if (this.turnoGroup < 4 && this.periodoGroup < 3){
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          shift: this.turnoGroup,
          period: this.periodoGroup,
          idReport: 1,
          reportModule: 1
        });
      }else if (this.turnoGroup === 4 && this.periodoGroup === 3){
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          idReport: 11,
          reportModule: 1
        });
      }else if (this.turnoGroup === 4 && this.periodoGroup < 3){ 
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          period: this.periodoGroup,
          idReport: 12,
          reportModule: 1
        });
      }else if (this.turnoGroup < 4 && this.periodoGroup === 3){ 
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          shift: this.turnoGroup,
          idReport: 13,
          reportModule: 1
        });
      }
      break;
    }
  }
}
</script>