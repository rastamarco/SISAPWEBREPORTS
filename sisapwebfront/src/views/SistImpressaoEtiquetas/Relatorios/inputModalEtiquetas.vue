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
      <EtiquetasPallets v-on="{getNote, getNrPlaca, getProvider, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 2" />
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
import ApontamentoProducao from '../../SistGerenciamentoProducao/SistApontamentosProducao/Relatorios/Parametros/ApontamentoProducao.vue';
import EtiquetasPallets from '../Relatorios/Parametros/etiquetasPallets.vue';

import {
  Action, Getter
} from 'vuex-class';
import _ from 'vuetify/es5/components/*';
@Component({
  components: {
    ApontamentoProducao,
    EtiquetasPallets 
  }
})
export default class InputModalEtiquetas extends Vue {
  
  @Prop() nameBox!: any
  @Prop() idBox!: any;

 
  @Action setSelectedIdReport
  @Action noShowReport
  @Action reportApontamentoProducao
  @Action reportEtiquetaPallet

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
  private line: any = null;
  private note: any = null;
  private nrPlaca: string = '';
  private idProvider: any = null;
  private providerName: any = null;

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

  public getMonthYear(date: any): void{  
    this.year = date.split('-')[0];
    this.month = date.split('-')[1];
  }

  public getNote(note: any): void { 
    this.note = note;
  }

  public getNrPlaca(nrPlaca: any): void{
    this.nrPlaca = nrPlaca;
  }

  public getProvider(idProvider: any, providerName: any): void{
    this.idProvider = idProvider;
    this.providerName = providerName;
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
      if (this.nrPlaca.length === 8 && this.idProvider !== null && this.note !== null){
        return true;
      } else { 
        return false;
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
      await this.EtiquetasPallets();
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

  public async EtiquetasPallets(): Promise<void>{
    await this.reportEtiquetaPallet({
      idProvider: this.idProvider, providerName: this.providerName, 
      nrPlaca: this.nrPlaca, nrNote: this.note, idReport: 5, reportModule: 5 });
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
