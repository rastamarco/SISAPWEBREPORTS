<template>
  <v-row>
      <v-col cols="12" sm="6" md="5" style="padding-left: 10%;padding-top:10%;">
        <span class="text-title">Data Inicial </span>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top: 10px;"></v-text-field>
            </template>
            <v-date-picker v-model="date" locale="pt" min="1950-01-01" @input="SendDate()" :max="dateMax" ></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" sm="6" md="5" style="padding-left: 10%;padding-top:10%;">
        <span class="text-title" >Data Final</span>
        <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted2)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top: 10px;"></v-text-field>
            </template>
            <v-date-picker v-model="date2" locale="pt" min="1950-01-01" @input="SendDate2()" :max="dateMax" ></v-date-picker>
        </v-menu>
      </v-col>
      <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()"  style="text-transform: none;">
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
export default class AgendamentosSemEmbarqueCargas extends Vue {
  @Prop() clearFields!: any; 

  @Action noShowReport
  @Action reportAgendamentoSemEmbarque

  @Getter showReport

  private date: any = new Date().toISOString().substr(0, 10) ;
  private menu: boolean = false;
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  private date2: any = new Date().toISOString().substr(0, 10) ;
  private menu2: boolean = false;
  private dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10))
  
  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.date = new Date().toISOString().substr(0, 10);
    this.date2 = new Date().toISOString().substr(0, 10);
    this.$emit('resetClearFields');
  }

  @Watch('date')
  public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);
  }

  @Watch('date2')
  public async onPropertyChangedsDate2(value: any, oldValue: any): Promise < void > {
    this.dateFormatted2 = this.formatDate(value);
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

  public SendDate(): void {
    this.menu = false;
  }

  public SendDate2(): void {
    this.menu2 = false;
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    await this.AgendamentoSemEmbarques();
    this.closeModal();
  }

  public closeModal(): void {
    this.$emit('closeModal');
  }

  public async AgendamentoSemEmbarques(): Promise<void>{
    await this.reportAgendamentoSemEmbarque({initialDate: this.date, endDate: this.date2, idReport: 1, reportModule:6 });
  }
 
}
</script>