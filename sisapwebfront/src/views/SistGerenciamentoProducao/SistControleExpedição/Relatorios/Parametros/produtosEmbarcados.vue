<template>
      <v-row >
      <v-col cols="12" sm="6" md="12" class="content">
        <br>
        <div class="input">
          <span>Nr. Carga <small>(Somente Números)</small></span>  
          <v-text-field v-model="nrCarga" outlined dense placeholder="ex: 01234569" type="number" min="0" max="12" hide-details style="padding-top: 5px;"></v-text-field>
        </div>
        <br>
        <div class="input">
          <span>Nr. Conteiner</span>  
          <v-text-field v-model="nrConteiner" outlined placeholder="ex: AWST123456-7" v-mask="'AAAA######-#'" dense  hide-details style="padding-top: 5px;"></v-text-field>
        </div>
        <div class="input">
        <span style="padding-top: 15px;">Data de Embarque</span>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;transform: scale(0.8);width: 200px;"></v-text-field>
          </template>
          <v-date-picker v-model="date" locale="pt" @input="menu = false" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
        </div>
      </v-col>
      <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
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
export default class ProdutosEmbarcados extends Vue {
  @Prop() clearFields!: any; 
  
  @Action ReportProdutosEmbarcados

  private nrCarga: any = null;
  private nrConteiner: any = null;
  private menu: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateToSend: any = null;

  public Clear(): void{
    this.dateFormatted = new Date().toISOString().substr(0, 10);
    this.date = new Date().toISOString().substr(0, 10);
    this.nrCarga = null;
    this.nrConteiner = null;
  }

   @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.Clear();
    this.$emit('resetClearFields');
  }

   @Watch('date')
   public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
     this.dateFormatted = this.formatDate(value);
   }

   public setDate(data: any): void {
     this.dateToSend = data;
     this.menu = false;
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

   public closeModal(): void{
     this.$emit('closeModal');
   }

   public canPrint(): boolean {
     if(this.nrCarga !== null  && this.nrConteiner != null){
       return true;
     } else { 
       return false;
     }
   }

   public async Print(): Promise < void > {
     await this.ReportProdutosEmbarcados({nrCarga: this.nrCarga, nrConteiner: this.nrConteiner, initialDate: this.date, idReport: 53, reportModule: 2});
     this.closeModal();
   }

} 
</script>
<style scoped>
.content{
  display: flex;
  flex-direction: column;
  align-items: center;
}

.content .input{
  display: flex;
  flex-direction: column;
  width:55%;
}
</style>