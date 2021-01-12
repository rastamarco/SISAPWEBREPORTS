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
      <v-col cols="12" sm="5" md="6" style="padding-left: 10%;padding-top:10%;">
        <span class="text-title" >Número do Romaneio: <small>(Opcional)</small></span>
          <v-text-field  v-model="romaneio" outlined hide-details dense style="padding-top: 10px;"></v-text-field>
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
export default class RomaneioOvos extends Vue {
  @Prop() clearFields!: any; 
  
  @Action noShowReport
  @Action reportFichaOvos
  
  @Getter showReport

  private date: any = new Date().toISOString().substr(0, 10) ;
  private menu: boolean = false;
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  private romaneio: any = null;
  
  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.date = new Date().toISOString().substr(0, 10);
    this.romaneio = null;
    await this.$emit('resetClearFields');
  }

  @Watch('date')
  public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);
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

  public closeModal(): void {
    this.$emit('closeModal');
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    await this.ControleFichaOvos();
    this.closeModal();
  }


  public async ControleFichaOvos(): Promise<void>{
    if (this.romaneio !== null) {
      await this.reportFichaOvos({romaneio: this.romaneio, initialDate: this.date, idReport: 14, reportModule: 8 });
    } else { 
      await this.reportFichaOvos({ initialDate: this.date, idReport: 15, reportModule: 8 });
    }
  }

}
</script>