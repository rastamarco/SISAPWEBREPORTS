<template>
    <v-row>
      <v-col cols="12" sm="10" md="10" style="padding-left:22%;" >
          <v-date-picker v-model="date" type="month" @input="getMonthYear(date)" locale="pt" 
            min="1950-01-01" :max="dateMax" no-title></v-date-picker>
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
export default class FomentoMensal extends Vue {
  @Prop() clearFields!: any; 

  @Action reportQualidadeFomento
  @Action noShowReport

  @Getter showReport

  private date: any = new Date().toISOString().substr(0, 7) ;
  private menu: boolean = false;
  private dateMax = new Date().toISOString().substr(0, 10);
  private month: any = null;
  private year: any = null;

  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.date = new Date().toISOString().substr(0, 7);
    this.$emit('resetClearFields');
  }

  public getMonthYear(date: any): void{  
    this.year = date.split('-')[0];
    this.month = date.split('-')[1];
  }
  
  public closeModal(): void{
    this.$emit('closeModal');
  }

  public async Print(): Promise< void >{
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    await this.QualidadeFomentoMensal();
    this.closeModal();
  }

  public async QualidadeFomentoMensal(): Promise<void>{
    
    const initialDate = this.year+'-'+this.month+'-01';
    const endDate = this.year+'-'+this.month+'-30';
    await this.reportQualidadeFomento({ month: this.month, year: this.year, initialDate: initialDate, endDate: endDate ,reportModule: 3, idReport: 1 });
  }
}
</script>