<template>
  <v-row>
      <v-col cols="12" sm="6" md="12" class="input-parameters">
          <span style="font-size: 14px;"> Selecione a data: </span>
          <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
              readonly outlined hide-details dense v-bind="attrs" v-on="on" class="inputs"></v-text-field>
            </template>
            <v-date-picker v-model="date" locale="pt" min="1950-01-01" @input="SendDate(date)" :max="dateMax" ></v-date-picker>
          </v-menu>
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
export default class ApontamentoRoboCMS extends Vue {
  @Prop() clearFields!: any; 

  private date: any = new Date().toISOString().substr(0, 10) ;
  private menu: boolean = false;
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  
  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.date = new Date().toISOString().substr(0, 10);
    this.$emit('resetClearFields');
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


  public SendDate(date: any): void {
    this.menu = false;
    this.$emit('getDate', date);
  }

  public initialParameters(): void{
    this.SendDate(this.date);
  }

  mounted(){
    this.initialParameters();
  }
}
</script>
<style scoped>
.input-parameters{
  display:flex;
  justify-content:center;
  flex-direction: column;
  align-items: center;
  padding: 70px; 
}

.input-parameters .inputs{
  max-width: 50%;
  padding-top: 5px;
}
</style>