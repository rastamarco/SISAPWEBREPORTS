<template>
<v-card width="100%" height="435">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
    <div class="close-box">
      <v-icon color="white" @click="closeModal()">mdi-close</v-icon>
    </div>
  </div>
  <v-card-actions>
     <v-row>
      <!-- Componentes para parametros Siga a sequencia que tudo vai dar certo! -->
      <!-- Relatório de Formação de Pallet --> 
      <formacaoPallet v-on="{getNrPallet}" v-bind="{clearFields}" v-if="idBox === 1"/>
      <!-- Relatório de Movimento de Câmara/Operador -->
      <movimentoCamaraOperador  v-on="{getShift, getMovement, getOperation, getCodSicop, getIdChambers, getInitialDate, getEndDate, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 2" />
      <!-- Mapa de Câmaras -->
      <mapaCamara v-on="{getIdChambers, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 3" />
    </v-row>
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
import {
  Action, Getter
} from 'vuex-class';

import formacaoPallet from '../Relatorios/Parametros/formacaoPallet.vue';
import movimentoCamaraOperador from '../Relatorios/Parametros/movimentoCamaraOperador.vue';
import mapaCamara from '../Relatorios/Parametros/mapaCamaras.vue';

@Component ({
  components: {
    formacaoPallet,
    movimentoCamaraOperador,
    mapaCamara
  }
})
export default class InputModalsExp extends Vue {
  @Prop() nameBox!: any
  @Prop() idBox!: any;

  @Action reportFormacaoPallets
  @Action setSelectedIdReport
  @Action reportMovimentoCamaraOperador
  @Action noShowReport
  
  @Getter filialName
  @Getter userFeatures
  @Getter showReport

  private dateToSend: any = null;
  private turnoGroup: any = 1;
  private periodoGroup: any = 1;
  private nrPallet: any = null;
  private clearFields: boolean = false;
  private idChambers: any = null;
  private InitialDate: any = null;
  private EndDate: any = null;
  private CodSicop: any = null;
  private Operation: any = null;
  private Movement: any = null;
  private Shift: any = null;
  
  public getNrPallet(nrPallet: any): void {
    this.nrPallet = nrPallet;
  }

  public getIdChambers(idChambers: any): void{
    this.idChambers = idChambers;
  }

  public getCodSicop(codSicop: any): void{
    this.CodSicop = codSicop;
  }

  public getOperation(operation: any): void{
    this.Operation = operation;
  }

  public getMovement(movement: any): void{
    this.Movement = movement;
  }

  public getShift(shift: any): void{
    this.Shift = shift;
  }

  public getInitialDate(initialDate: any): void{
    this.InitialDate = initialDate;
  }

  public getEndDate(endDate: any): void{
    this.EndDate = endDate;
  }

  public canPrint(): boolean {
    switch(this.idBox){
    case 1:
      if(this.nrPallet !== null && this.nrPallet !== ''){
        return true;
      } else { 
        return false;
      }
    case 2:
      if(this.idChambers !== null){
        return true;
      } else { 
        return false;
      }
    default: 
      return false;
    }
  }

  public addDay(date: string): any {
    if (!date) return null;
    const [year, month, day] = date.split('-');
    return `${year}-${month}-${parseInt(day)+1}`;
  }

  public resetClearFields(): void{
    this.clearFields = false;
  }

  public async closeModal(): Promise<void> {
    this.clearFields = true;
    this.$emit('closeModal');
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    switch(this.idBox){
    case 1:
      await this.ReportFormacaoPallet(this.nrPallet);
      break;
    case 2:
      await this.ReportMovimentoCamaraOperador();
      break; 
    }
  }

  public async ReportFormacaoPallet(nrPallet: any): Promise<void> {
    await this.reportFormacaoPallets({ 
      idReport: 1,
      nrPallet: nrPallet,
      reportModule: 2
    });
    this.closeModal();
  } 

  public async ReportMovimentoCamaraOperador(): Promise<void> {
    // Fazer as Validações aqui para cada item enviado 
    let initDate = '';
    let finalDate = '';
    if(this.InitialDate === this.EndDate) {
      initDate = this.InitialDate;
      finalDate = this.InitialDate;
    } else if(this.InitialDate > this.EndDate) {
      this.$swal('Ops!', 'A data Final é menor que a data Inicial.','warning');
      return;
    } else if(this.InitialDate < this.EndDate) {
      initDate = this.InitialDate;
      finalDate = this.EndDate;
    }
    switch(this.Movement){
    case '1':
      // Movimentação por Câmaras
      // Tipo de Operação
      switch(this.Operation){
      case '1':
        if(this.Shift < 3){
          if (this.haveRegistration() === true){
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 2, reportModule: 2 });
          } else {
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, shift: this.getShiftToSend(), idReport: 24, reportModule: 2 }); 
          }
        } else { 
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 26, reportModule: 2 });
        }
        break;
      case '2':
        if(this.Shift < 3){
          if (this.haveRegistration() === true){
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 21, reportModule: 2 });
          } else { 
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, shift: this.getShiftToSend(), idReport: 25, reportModule: 2 });
          }
        } else { 
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 27, reportModule: 2 });
        }
        break;
      case '3':
        if (this.haveRegistration() === true){
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 23, reportModule: 2 });
        } else { 
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 22, reportModule: 2 });
        }
        break;
      }
      this.closeModal();
      break;
    case '2':
      // Movimentação por Operador
      this.ReportMovimentoOperador();
      break;
    } 
  }

  public async ReportMovimentoOperador(): Promise<void> {
    let initDate = '';
    let finalDate = '';
    if(this.InitialDate === this.EndDate) {
      initDate = this.InitialDate;
      finalDate = this.InitialDate;
    } else if(this.InitialDate > this.EndDate) {
      this.$swal('Ops!', 'A data Final é menor que a data Inicial.','warning');
      return;
    } else if(this.InitialDate < this.EndDate) {
      initDate = this.InitialDate;
      finalDate = this.EndDate;
    }
    switch(this.Operation){
    case '1':
      if (this.haveRegistration() === true){
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 2, reportModule: 2 });
      } else {
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 28, reportModule: 2 }); 
      }
      break;
    case '2':
      if (this.haveRegistration() === true){
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 21, reportModule: 2 });
        
      } else {
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 29, reportModule: 2 }); 
      }
      break;
    case '3':
      if (this.haveRegistration() === true){
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 23, reportModule: 2 });
      } else {
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 3, reportModule: 2 }); 
      }
      break;
    }
    this.closeModal();
  }

  public getShiftToSend(): any{
    switch(this.Shift){
    case '1': 
      return '1';
    case '2': 
      return '2';
    case '3': 
      return '0';
    case '4': 
      return '';
    }
  }

  public haveRegistration(): boolean{
    if (this.CodSicop !== null){
      return true;
    } else { 
      return false;
    }
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
