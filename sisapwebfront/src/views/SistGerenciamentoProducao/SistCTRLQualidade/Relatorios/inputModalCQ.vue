<template>
<v-card width="100%" height="355">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
    <div class="close-box">
      <v-icon color="white" @click="closeModal()">mdi-close</v-icon>
    </div>
  </div>
  <v-card-actions>
      <!-- Todos os Parametros vão aqui --> 
      <ApontamentoProducao v-on="{getShift, getPeriod, getDate, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 1" />
      <QualidadeFomento v-on="{getMonthYear, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 2" />  

    <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" :disabled="!canPrint()" style="text-transform: none;">
      <v-icon>mdi-printer</v-icon>
      Imprimir
    </v-btn> 
  </v-card-actions>
</v-card>
</template>

<script lang="ts">
import {
  Component,
  Vue,
  Prop,
  Watch
} from 'vue-property-decorator';
import ApontamentoProducao from '../../SistApontamentosProducao/Relatorios/Parametros/ApontamentoProducao.vue';
import QualidadeFomento from '../Relatorios/Parametros/FomentoMensal.vue';

import {
  Action, Getter
} from 'vuex-class';
import _ from 'vuetify/es5/components/*';
@Component({
  components: {
    ApontamentoProducao,
    QualidadeFomento
  }
})
export default class InputModalCQ extends Vue {
  
  @Prop() nameBox!: any
  @Prop() idBox!: any;

  
  @Action setSelectedIdReport
  @Action noShowReport
  @Action reportApontamentoProducao
  @Action reportQualidadeFomento

  @Getter filialName
  @Getter loginUser
  @Getter userFeatures
  @Getter showReport

  private shift: any = null;
  private period: any = null;
  private date: any = null;
  private dateEnd: any = null;
  private clearFields: boolean = false;
  private InitialHour: any = null;
  private EndHour: any = null;
  private isPeriod: any = null;
  private month: any = null;
  private year: any = null;

  public getShift(shift: any): void {
    this.shift = shift;
  }

  public getPeriod(period: any): void{
    this.period = period;
  }

  public getDate(date: any, isPeriod: any): void{
    this.date = date;
    this.isPeriod = isPeriod;
  }

  public getDateEnd(date: any, isPeriod: any): void{
    this.dateEnd = date;
    this.isPeriod = isPeriod;
  }
  
  public clearInputs(): void{
    this.shift = null;
    this.period = null;
    this.date = null;
    this.dateEnd = null;
    this.InitialHour = null;
    this.EndHour = null;
    this.isPeriod = null;
    this.month = null;
    this.year = null; 
  }

  public async closeModal(): Promise<void> {
    this.clearFields = true;
    await this.clearInputs();
    this.$emit('closeModal');
  }

  public getMonthYear(date: any): void{  
    this.year = date.split('-')[0];
    this.month = date.split('-')[1];
  }
  
  public canPrint(): boolean {
    switch(this.idBox){
    case 1:
      return true;
    case 2: 
      if (this.date > this.dateEnd){
        this.$swal('Ops!', 'A Data Inicial informada é maior que a Data Final, corrija-as e tente novamente', 'error');
        this.clearFields = true;
        return false;
      } else { 
        return true;
      }
    default:
      return false;   
    }
    
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    switch(this.idBox){
    case 1:
      await this.ApontamentoProducao();
      break;
    case 2: 
      await this.QualidadeFomentoMensal();
      break; 
    }
    this.closeModal();
  }

  public async ApontamentoProducao(): Promise<void>{
    switch(this.filialName){
    case 'UIC':
      if (this.shift === 4){
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
          shift: this.shift,
          idReport: 14,
          reportModule: 1
        });
      }
      break;
    default:
      if (this.shift < 4 && this.period < 3){
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          shift: this.shift,
          period: this.period,
          idReport: 1,
          reportModule: 1
        });
      }else if (this.shift === 4 && this.period === 3){
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          idReport: 11,
          reportModule: 1
        });
      }else if (this.shift === 4 && this.period < 3){ 
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          period: this.period,
          idReport: 12,
          reportModule: 1
        });
      }else if (this.shift < 4 && this.period === 3){ 
        await this.reportApontamentoProducao({
          localUser: this.filialName, 
          date: this.date,
          shift: this.shift,
          idReport: 13,
          reportModule: 1
        });
      }
      break;
    }
  }
 
  public async QualidadeFomentoMensal(): Promise<void>{
    const initialDate = this.year+'-'+this.month+'-01';
    const endDate = this.year+'-'+this.month+'-30';
    await this.reportQualidadeFomento({ month: this.month, year: this.year, initialDate: initialDate, endDate: endDate ,reportModule: 3, idReport: 1 });
  }

  public async getShiftHours(): Promise<any>{
    switch(this.shift){
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

  public resetClearFields(): void{
    this.clearFields = false;
  }

}
</script>

<style scoped>
.title {
  display: flex;
  flex-direction: row;
  height: 40px;
  background: #20A8D8;
  padding-top: 4px;
}

.title .title-box {
  color: white;
  padding-left: 10px;
  font-size: 16px;
  width:60%;
}

.title .close-box {
  padding-right: 16px;
  display:flex;
  justify-content: flex-end;
  font-size: 22px;
  width:40%;
}

.date-input {
  padding-left: 15px;
  margin-top: 10px;
}

.text-title {
  padding-left: 10px;
  font-weight: bold;
  font-size: 17px;
}
</style>
