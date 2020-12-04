<template>
<v-card width="100%" height="335">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
    <div class="close-box">
      <v-icon color="white" @click="closeModal()">mdi-close</v-icon>
    </div>
  </div>
  <v-card-actions>
      <!-- Todos os Parametros vão aqui --> 
      <ApontamentoProducao v-on="{getShift, getPeriod, getDate, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 1" />
      <ApontamentoRefeitorio v-on="{getShift, getDate, getDateEnd, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 2" /> 

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
import ApontamentoProducao from '../Relatorios/Parametros/ApontamentoProducao.vue';
import ApontamentoRefeitorio from '../Relatorios/Parametros/ApontamentoRefeitorio.vue';

import {
  Action, Getter
} from 'vuex-class';
import _ from 'vuetify/es5/components/*';
@Component({
  components: {
    ApontamentoProducao,
    ApontamentoRefeitorio
  }
})
export default class InputModals extends Vue {
  
  @Prop() nameBox!: any
  @Prop() idBox!: any;

  @Action reportApontamentoProducao
  @Action reportApontamentoRefeitorio
  @Action setSelectedIdReport

  @Getter filialName
  @Getter loginUser
  @Getter userFeatures

  private shift: any = null;
  private period: any = null;
  private date: any = null;
  private dateEnd: any = null;
  private clearFields: boolean = false;
  private InitialHour: any = null;
  private EndHour: any = null;
  private isPeriod: any = null;

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
  
  public closeModal(): void {
    this.clearFields = true;
    this.$emit('closeModal');
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
    switch(this.idBox){
    case 1:
      await this.ApontamentoProducao();
      break;
    case 2: 
      await this.ApontamentoRefeitorio();
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

  public async ApontamentoRefeitorio(): Promise<void>{
    await this.getShiftHours();
    let newShift;
    if(this.shift === 4)
      newShift = 'Todos';
    else
      newShift = this.shift;
    
    switch(this.isPeriod){
    case false: 
      await this.reportApontamentoRefeitorio({
        InitialDate: this.date+' 00:00:00',
        EndDate: this.date+' 23:59:59',
        InitialHour: this.InitialHour,
        EndHour:this.EndHour, 
        shift: newShift, 
        idReport: 16, 
        reportModule: 1 
      });
      break;
    case true:
      if(this.date < this.dateEnd) {
        await this.reportApontamentoRefeitorio({
          InitialDate: this.date+' 00:00:00', 
          EndDate: this.dateEnd+' 23:59:59',
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
