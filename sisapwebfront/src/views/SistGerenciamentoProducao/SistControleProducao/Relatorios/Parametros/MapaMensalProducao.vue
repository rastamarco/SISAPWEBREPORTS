<template>
  <v-row>
      <v-col cols="12" sm="6" md="12" class="input-parameters">
          <span style="font-size: 14px;"> Selecione o Mês: </span>
          <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" class="inputs"></v-text-field>
            </template>
          <v-date-picker v-model="date" type="month" locale="pt" min="1950-01-01" :max="dateMax" >
            <v-spacer></v-spacer>
              <v-btn text color="primary" @click="menu = false"> Cancel</v-btn>
              <v-btn text color="primary" @click="SendDate(date)">OK</v-btn>
          </v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" sm="6" md="12" class="input-parameters">
        <span style="font-size: 14px;">Linha de Produção</span>
        <v-radio-group v-model="rgLineProd" dense row class="inputs">
          <v-radio label="Linha 1" value="1" dense></v-radio>
          <v-radio label="Linha 2" value="2" dense></v-radio>
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
export default class MapaMensalProducao extends Vue {
  @Prop() clearFields!: any; 
  
  @Action reportMapaMensal
  @Action noShowReport

  @Getter filialName
  @Getter showReport

  private date: any = new Date().toISOString().substr(0, 7) ;
  private menu: boolean = false;
  private dateMax = new Date().toISOString().substr(0, 7);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 7))
  private rgLineProd: any = 1;
  private line: any= null;
  private year: any = null;
  private month: any = null;

  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.date = new Date().toISOString().substr(0, 7);
    this.rgLineProd = 1;
    this.$emit('resetClearFields');
  }

  @Watch('rgLineProd')
  public async onPropertyChangedsRgLineProd(value: any, oldValue: any): Promise < void > {
    this.getLineProd(value);
  }

  @Watch('date')
  public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);
  }

  public getLineProd(line: any): void{
    switch(this.filialName){
    case 'UIC':
      switch(line){
      case 1:
        this.line = 'Coz/Ass/Emp';
        break;
      case 2: 
        this.line = 'L2UIC';
        break;
      }
      break;
    case 'UIA':
      switch(line){
      case 1:
        this.line = 'Aves';
        break;
      case 2: 
        this.line = 'L2';
        break;
      }
      break;
    }
  }

  public formatDate(date: string): any {
    if (!date) return null;
    const [year, month, day] = date.split('-');
    return `${month}/${year}`;
  }

  public parseDate(date: any): any {
    if (!date) return null;
    const [month, year] = date.split('/');
    return `${year}-${month.padStart(2, '0')}`;
  }

  public SendDate(date: any): void {
    this.menu = false;
    this.getMonthYear(date);
  }

  public getMonthYear(date: any): void{  
    this.year = date.split('-')[0];
    this.month = date.split('-')[1];
  }

  public closeModal(): void{
    this.$emit('closeModal');
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    await this.MapaMensal();
    this.closeModal();
  }

  public async MapaMensal(): Promise<void>{
    const initialDate = this.year+'-'+this.month+'-01';
    const endDate = this.year+'-'+this.month+'-30';
    await this.reportMapaMensal({ initialDate: initialDate, endDate: endDate, localUser: this.filialName, line: this.line, reportModule: 4, idReport: 1 });
  }

}
</script>
<style scoped>
.input-parameters{
  display:flex;
  justify-content:center;
  flex-direction: column;
  align-items: center;
  padding-top: 25px;
}

.input-parameters .inputs{
  max-width: 50%;
  padding-top: 5px;
}
</style>