<template>
      <v-row >
      <v-col cols="12" sm="6" md="12" class="content">
        <div class="input">
        <span style="padding-top: 15px;margin-left:25px;">Data Inicial</span>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;transform: scale(0.8);width:225px;margin-left: 30px;"></v-text-field>
          </template>
          <v-date-picker v-model="date" locale="pt" @input="menu = false" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
        </div>
        <div class="input">
        <span style="padding-top: 15px;">Data Final</span>
        <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;transform: scale(0.8);width: 230px;"></v-text-field>
          </template>
          <v-date-picker v-model="date2" locale="pt" @input="menu2 = false" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
        </div>
      </v-col>
      <v-col cols="12" sm="6" md="12" class="content">
        <div class="input">
          <span>Nr. Conferente</span>  
          <v-text-field v-model="nrConferente" outlined placeholder="ex: 123456" dense  hide-details style="padding-top: 6px;margin-left:20px;"></v-text-field>
        </div>
      </v-col>
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
export default class RelatorioEmbarquesPeriodo extends Vue {
  @Prop() clearFields!: any; 
  
  private nrConferente: any = null;
  private menu: boolean = false;
  private menu2: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10));
  private date2 = new Date().toISOString().substr(0, 10);
  private dateFormatted2 = this.formatDate(new Date().toISOString().substr(0, 10));
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateToSend: any = null;

  public Clear(): void{
    this.dateFormatted = new Date().toISOString().substr(0, 10);
    this.date = new Date().toISOString().substr(0, 10);
    this.dateFormatted2 = new Date().toISOString().substr(0, 10);
    this.date2 = new Date().toISOString().substr(0, 10);
    this.nrConferente = null;
  }

   @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.Clear();
    await this.InitialParameters();
    this.$emit('resetClearFields');
  }

   @Watch('date')
   public async onPropertyChangedsDate(value: any, oldValue: any): Promise < void > {
     this.dateFormatted = this.formatDate(value);
     this.$emit('getInitialDate', value);
   }

   @Watch('date2')
   public async onPropertyChangedsDateEnd(value: any, oldValue: any): Promise < void > {
     this.dateFormatted2 = this.formatDate(value);
     this.$emit('getEndDate', value);
   }

   @Watch('nrConferente')
   public async onPropertyChangedsConferente(value: any, oldValue: any): Promise < void > {
     this.$emit('getNrConferente', value);
   }

   public setDate(): void {
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

   public InitialParameters(): void{
     this.$emit('getInitialDate', this.date);
     this.$emit('getEndDate', this.date2);
   }

   mounted(){
     this.InitialParameters();
   }

} 
</script>
<style scoped>
.content{
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content:center;
}

.content .input{
  display: flex;
  flex-direction: column;
  width:55%;
  align-items: flex-start;
  padding-left: 5%;
  padding-top:5%;
}
</style>