<template>
      <v-row >
      <v-col cols="2" sm="2" md="5" style="padding-left:40px;">
        <small>Selecione a(s) Câmara(s):</small>
        <v-checkbox v-model="selectChambers" label="Todas" dense style="transform: scale(0.95);margin:0;" :disabled="allChambers === null"></v-checkbox>
        <div class="content-checkbox" v-if="!isLoadingChambers" > 
          <div v-for="items in allChambers" :key="items.cod_camara">
            <v-checkbox
              v-model="idChambers"
              :label="items.cod_camara"
              :value="items.cod_camara"
              class="item-checkbox"
              dense
            ></v-checkbox>
          </div>
          <small v-if="allChambers === null" style="color: red;">Não foi possível obter a lista de câmaras.</small>
        </div>
        <div class="loading" v-if="isLoadingChambers">
          <v-progress-circular
              :size="50"
              indeterminate
              color="primary"
            ></v-progress-circular>
        </div>
        <small>Matrícula <small>(Opcional)</small></small>
        <v-text-field dense outlined v-model="codSicop" placeholder="ex: 12345" type="number" min="0" @input="SendCodSicop(codSicop)" hide-details style="transform: scale(0.8);"></v-text-field>
      </v-col>
      <v-col cols="4" sm="3" md="7" style="padding-left:10px;">
        <small>Tipo de Movimentação</small>
        <v-radio-group v-model="rgTypeMove" dense row style="margin:0;transform: scale(0.9);">
          <v-radio label="Por Câmara" value="1"></v-radio>
          <v-radio label="Por Operador" value="2" ></v-radio>
        </v-radio-group>
         <hr>
        <small>Tipo de Operação</small>
        <v-radio-group v-model="rgTypeOperation" row dense style="margin:0;transform: scale(0.9);">
          <v-radio label="Armazenado" value="1" ></v-radio>
          <v-radio label="Expedido" value="2" ></v-radio>
          <v-radio label="Todos" value="3" ></v-radio>
        </v-radio-group>
         <hr>
        <small>Turno</small>
        <v-radio-group v-model="rgShift" :disabled="codSicop !== null && codSicop !== ''" row dense  style="margin:0;transform: scale(0.9);">
          <v-radio label="1º" value="1"></v-radio>
          <v-radio label="2º" value="2" ></v-radio>
          <v-radio label="3º" value="3" ></v-radio>
          <v-radio label="Todos" value="4"></v-radio>
        </v-radio-group>
         <hr>
      </v-col>
      <v-col cols="2" sm="2" md="5" style="padding-left:60px;padding-top:0;">
       <small class="text-title">Data Inicial</small>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;transform: scale(0.8);"></v-text-field>
          </template>
          <v-date-picker v-model="date" @input="setDate(date)" locale="pt" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="2" sm="2" md="5" style="padding-left:40px;padding-top:0;">
        <small class="text-title">Data Final <small>(Para Períodos e Datas Únicas)</small></small>
        <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted2)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;transform: scale(0.8);"></v-text-field>
          </template>
          <v-date-picker v-model="date2" @input="setDate(date2)" locale="pt" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
      </v-col>
      <v-btn absolute rounded text bottom left color="primary" @click="CloseModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" :disabled="!canPrint()" style="text-transform: none;">
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
export default class MovimentoCamaraOperador extends Vue {
  @Action getChambersByFilial
  @Action reportMovimentoCamaraOperador
  @Action noShowReport

  @Getter showReport
  @Getter filialName
  @Getter allChambers
 
  @Prop() clearFields!: any; 

  private codSicop: any | number = null;
  private idChambers: Array<any> = [];
  private selectChambers: boolean = false;
  private rgTypeMove: any = '1';
  private rgShift: any = '4';
  private rgTypeOperation: any = '3';
  private menu: boolean = false;
  private menu2: boolean = false;
  private dateToSend: any = null;
  private date = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  private date2 = new Date().toISOString().substr(0, 10);
  private dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10))
  private dateMax = new Date().toISOString().substr(0, 10);
  private isLoadingChambers: boolean = false;

  @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.idChambers.splice(0, this.idChambers.length);
    this.selectChambers = false;
    this.rgTypeOperation = '3';
    this.rgTypeMove = '1';
    this.rgShift = '4';
    this.codSicop = null;
    this.date = new Date().toISOString().substr(0, 10);
    this.date2 = new Date().toISOString().substr(0, 10);
    this.$emit('resetClearFields');
  }

  @Watch('selectChambers')
  public async onPropertyChangedsChamber(value: any, oldValue: any): Promise < void > {
    if(value === true){
      this.allChambers.forEach(chambers =>{
        this.idChambers.push(chambers.cod_camara);
      });
    }else{
      this.idChambers.splice(0, this.idChambers.length);
    }
  }

  @Watch('date')
  public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);  }

  @Watch('date2')
  public async onPropertyChangedsDate2(value: any, oldValue: any): Promise < void > {
    this.dateFormatted2 = this.formatDate(value);
  }

  public setDate(data: any): void {
    this.dateToSend = data;
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

  public canPrint(): boolean {
    if(this.idChambers !== null){
      return true;
    } else { 
      return false;
    }
  }

  public async Print(): Promise<void> {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    switch(this.rgTypeMove){
    case '1':
      await this.ReportMovimentoCamaraOperador();
      break;
    case '2':
      await this.ReportMovimentoOperador();
      break;
    }
  }
  
  public async ReportMovimentoCamaraOperador(): Promise<void> {
    // Fazer as Validações aqui para cada item enviado 
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
    
    // Movimentação por Câmaras
    // Tipo de Operação
    switch(this.rgTypeOperation){
    case '1':
      if(this.rgShift <= 3){
        if (this.haveRegistration() === true){
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.codSicop, idReport: 2, reportModule: 2 });
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
      if(this.rgShift <= 3){
        if (this.haveRegistration() === true){
          await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.codSicop, idReport: 21, reportModule: 2 });
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
        await this.reportMovimentoCamaraOperador({ chambers: this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.codSicop, idReport: 23, reportModule: 2 });
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
  }

  public async ReportMovimentoOperador(): Promise<void> {
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
    switch(this.rgTypeOperation){
    case '1':
      if (this.haveRegistration() === true){
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.codSicop, idReport: 2, reportModule: 2 });
      } else {
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 28, reportModule: 2 }); 
      }
      break;
    case '2':
      if (this.haveRegistration() === true){
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.codSicop, idReport: 21, reportModule: 2 });
        
      } else {
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, idReport: 29, reportModule: 2 }); 
      }
      break;
    case '3':
      if (this.haveRegistration() === true){
        await this.reportMovimentoCamaraOperador({ chambers:this.idChambers, initialDate: initDate, endDate: finalDate, registration: this.codSicop, idReport: 23, reportModule: 2 });
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

  public getShiftToSend(): any{
    switch(this.rgShift){
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
    if (this.codSicop !== null){
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

  public async closeModal(): Promise<void> {
    this.$emit('closeModal');
  }

  async mounted() {
    if (this.allChambers === null) {
      this.isLoadingChambers = true;
      await this.getChambersByFilial({filial: this.filialName });
      this.isLoadingChambers = false;
    }
  }
}
</script>
<style scoped>
.loading{
  display:flex;
  justify-content:center;
}

.content-checkbox{
  display:flex;
  flex-direction: row;
  width: 80%;
  flex-wrap: wrap;
}

.content-checkbox .item-checkbox{
  display: flex;
  flex-direction: row;
  padding-left: 10px;
  transform: scale(0.95);
  margin:0;
}

</style>