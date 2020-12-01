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
      <movimentoCamaraOperador  v-on="{getShift, getMovement, getOperation, getCodSicop, getIdChambers, getInitialDate, getEndDate}" v-bind="{clearFields}" v-if="idBox === 2" />
    </v-row>
    <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" style="text-transform: none;">
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


@Component ({
  components: {
    formacaoPallet,
    movimentoCamaraOperador
  }
})
export default class InputModalsExp extends Vue {
  @Prop() nameBox!: any
  @Prop() idBox!: any;

  @Action reportFormacaoPallets
  @Action setSelectedIdReport
  @Action reportMovimentoCamaraOperador
  @Action reportMovimentoOperadorCamara

  @Getter filialName
  @Getter userFeatures

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

  public closeModal(): void {
    this.clearFields = true;
    this.$emit('closeModal');
  }

  public async Print(): Promise < void > {
    switch(this.idBox){
    case 1:
      await this.ReportFormacaoPallet(this.nrPallet);
      break;
    case 2:
      await this.ReportMovimentoCamaraOperador(this.idChambers, this.Shift, 
        this.Operation, this.Movement, this.InitialDate, this.EndDate, this.CodSicop);
      break; 
    }
    this.closeModal();
  }

  public async ReportFormacaoPallet(nrPallet: any): Promise<void> {
    await this.reportFormacaoPallets({ 
      idReport: 2,
      nrPallet: nrPallet,
      reportModule: 2
    });
    this.clearFields = true;
  } 

  public async ReportMovimentoCamaraOperador(idChambers: any, shift: any, operation: any, movement: any,
    initialDate: any, endDate: any, codSicop: any): Promise<void> {
    // Fazer as Validações aqui para cada item enviado 
    switch(movement){
    case '1':
      // Movimentação por Câmaras
      // Tipo de Operação
      switch(operation){
      case '1':
        await this.reportMovimentoCamaraOperador({ chambers: idChambers, initDate: initialDate, endDate: endDate, codsicop: codSicop, shift: this.getShiftToSend(), idReport: 3, reportModule: 2 });
        break;
      case '2':
        await this.reportMovimentoCamaraOperador({ chambers: idChambers, initDate: initialDate, endDate: endDate, codsicop: codSicop, shift: this.getShiftToSend(), idReport: 31, reportModule: 2 });
        break;
      case '3':
        await this.reportMovimentoCamaraOperador({ chambers: idChambers, initDate: initialDate, endDate: endDate, codsicop: codSicop, shift: this.getShiftToSend(), idReport: 32, reportModule: 2 });
        break;
      }
      break;
    case '2':
      // Movimentação por Operador
      await this.reportMovimentoOperadorCamara({ chambers: idChambers, initDate: initialDate, endDate: endDate, idReport: 4, reportModule: 2 });
      break;
    } 
    this.clearFields = true;
  }

  public getShiftToSend(): any{
    switch(this.Shift){
    case '1': 
      return '1';
    case '2': 
      return '2';
    case '3': 
      return '3';
    case '4': 
      return '';
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
