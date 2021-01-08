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
      <!-- Localização de Produtos -->
      <localizacaoProduto v-on="{getNrPallet, getCodSicop, GetEmptyPositions,  resetClearFields}" v-bind="{clearFields}" v-if="idBox === 4" />
      <!-- Histórico Camara Pallet -->
      <historicoCamaraPallet v-on="{getIdChambers, getEndDate, getCodSicop, getInitialDate,  resetClearFields, GetStatus}" v-bind="{clearFields}" v-if="idBox === 5" />

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
import localizacaoProduto from '../Relatorios/Parametros/localizacaoProdutos.vue';
import historicoCamaraPallet from '../Relatorios/Parametros/historicoCamaraPallet.vue';

@Component ({
  components: {
    formacaoPallet,
    movimentoCamaraOperador,
    mapaCamara,
    localizacaoProduto,
    historicoCamaraPallet
  }
})
export default class InputModalsExp extends Vue {
  @Prop() nameBox!: any
  @Prop() idBox!: any;

  @Action reportFormacaoPallets
  @Action setSelectedIdReport
  @Action reportMovimentoCamaraOperador
  @Action reportCamara
  @Action noShowReport
  @Action ReportLocalizacaoProdutos
  @Action ReportHistoricoCamaraPallet
  @Action ReportPosicaoCamaraVazia

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
  private position: boolean = false;
  private Status: any = null;

  //#region ------------- Get Parameters 
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

  public GetEmptyPositions(position: any): void{
    this.position = position;
  }

  public GetStatus(status: any): void {
    this.Status = status;
  }

  @Watch('idBox')
  public async onPropertyChangedsIdBox(value: any, oldValue: any): Promise < void > {
    console.log(value);
    switch(value){
    case 6:
      this.closeModal();
      await this.ReportPosicaoVazia();
      break;
    }
  }

  //#endregion --------------------------------------------------

  //#region ------------- Prints, Reset Fields ....
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
    case 3:
      if(this.idChambers !== null){
        return true;
      } else { 
        return false;
      }
    case 4:
      if((this.nrPallet !== null && this.nrPallet !== '') || (this.CodSicop !== null && this.CodSicop !== '')){
        return true;
      } else { 
        return false;
      }
    case 5:
      if(this.Status !==  null && this.idChambers !== null){
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
    case 3:
      await this.ReportMapaCamaras();
      break; 
    case 4:
      await this.ReportLocalizaProdutos();
      break;
    case 5: 
      await this.ReportCamaraPallet();
      break;  
    }
  }
  //#endregion ---------------------------------

  //#region ------------- Report Methods
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
        if(this.Shift <= 3){
          if (this.haveRegistration() === true){
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 2, reportModule: 2 });
          } else {
            const shifts = this.getShiftToSend();
            console.log(shifts);
            const sendIdReport: any = null;
            if (shifts === '1' || shifts === '2' || shifts === '0'){ 
              await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: this.getIdReportChambersArmz(shifts), reportModule: 2 }); 
            }
          }
        } else { 
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 26, reportModule: 2 });
        }
        break;
      case '2':
        if(this.Shift <= 3){
          if (this.haveRegistration() === true){
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 21, reportModule: 2 });
          } else {
            const shifts = this.getShiftToSend();
            const sendIdReport: any = null;
            if (shifts === '1' || shifts === '2' || shifts === '0'){  
              await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: this.getIdReportChambersExp(shifts), reportModule: 2 });
            }
          }
        } else { 
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 27, reportModule: 2 });
        }
        break;
      case '3':
        if (this.haveRegistration() === true){
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.CodSicop, idReport: 23, reportModule: 2 });
        } else {
          const shifts = this.getShiftToSend();
          const sendIdReport: any = null;
          if (shifts === '1' || shifts === '2' || shifts === '0'){ 
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: this.getIdReportChambers(shifts), reportModule: 2 });
          } else {
            await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 22, reportModule: 2 });
          }   
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
        const shifts = this.getShiftToSend();
        const sendIdReport: any = null;
        if (shifts === '1' || shifts === '2' || shifts === '0'){ 
          // Mandar com turno
          await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, shift: shifts, idReport: this.getIdReportOperador(shifts), reportModule: 2 }); 
        } else { 
          await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 3, reportModule: 2 }); 
        }
         
      }
      break;
    }
    this.closeModal();
  }

  public async ReportMapaCamaras(): Promise<void>{
    await this.reportCamara({chambers: this.idChambers, reportModule: 2, idReport: 41});
    this.closeModal();
  }

  public async ReportLocalizaProdutos(): Promise<void>{
    if (this.nrPallet === null){
      await this.ReportLocalizacaoProdutos({codSicop: this.CodSicop, nrPallet: this.nrPallet, reportModule: 2, idReport: 42});
    } else { 
      await this.ReportLocalizacaoProdutos({codSicop: this.CodSicop, nrPallet: this.nrPallet, reportModule: 2, idReport: 43});
    }
    this.closeModal();
  }

  public async ReportCamaraPallet(): Promise<void> {
    if(this.CodSicop === null){
      switch(this.Status){
      case 'Armazenado(s)':
        await this.ReportHistoricoCamaraPallet({idChamber: this.idChambers, initialDate: this.InitialDate, endDate: this.EndDate, filialName: this.filialName, reportModule: 2, idReport: 45});
        break;
      case 'Expedido(s)':
        await this.ReportHistoricoCamaraPallet({idChamber: this.idChambers, initialDate: this.InitialDate, endDate: this.EndDate, filialName: this.filialName, reportModule: 2, idReport: 46});
        break;
      case 'Excluído(s)':
        await this.ReportHistoricoCamaraPallet({idChamber: this.idChambers, initialDate: this.InitialDate, endDate: this.EndDate, filialName: this.filialName, reportModule: 2, idReport: 47});
        break;
      }
    } else {
      // o relatório com produto;
      switch(this.Status){
      case 'Armazenado(s)':
        await this.ReportHistoricoCamaraPallet({idChamber: this.idChambers, initialDate: this.InitialDate, endDate: this.EndDate, filialName: this.filialName, codSicop: this.CodSicop, reportModule: 2, idReport: 48});
        break;
      case 'Expedido(s)':
        await this.ReportHistoricoCamaraPallet({idChamber: this.idChambers, initialDate: this.InitialDate, endDate: this.EndDate, filialName: this.filialName, codSicop: this.CodSicop, reportModule: 2, idReport: 49});
        break;
      case 'Excluído(s)':
        await this.ReportHistoricoCamaraPallet({idChamber: this.idChambers, initialDate: this.InitialDate, endDate: this.EndDate, filialName: this.filialName, codSicop: this.CodSicop, reportModule: 2, idReport: 50});
        break;
      }
    }
    
 
    // await this.ReportHistoricoCamaraPallet({codSicop: this.CodSicop, nrPallet: this.nrPallet, reportModule: 2, idReport: 45});
    this.closeModal();    
  }

  public async ReportPosicaoVazia(): Promise<void> {
    await this.ReportPosicaoCamaraVazia({filialName: this.filialName, idReport: 51, reportModule: 2 });
  }
  //#endregion --------------------------------------------------

  //#region ------------- Print Informations
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

  public getIdReportOperador(shifts): any{
    switch(shifts){
    case '1': 
      return 31;
    case '2':
      return 32;
    case '0': 
      return 33;
    }
  }

  public getIdReportChambers(shifts): any{
    switch(shifts){
    case '1': 
      return 34;
    case '2':
      return 35;
    case '0': 
      return 36;
    }
  }

  public getIdReportChambersArmz(shifts): any{
    switch(shifts){
    case '1': 
      return 24;
    case '2':
      return 37;
    case '0': 
      return 38;
    }
  }

  public getIdReportChambersExp(shifts): any{
    switch(shifts){
    case '1': 
      return 25;
    case '2':
      return 39;
    case '0': 
      return 4;
    }
  }

  //#endregion --------------------------------
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
  width:70%;
}

.title .close-box {
  padding-right: 16px;
  display:flex;
  justify-content: flex-end;
  font-size: 22px;
  width:30%;
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
