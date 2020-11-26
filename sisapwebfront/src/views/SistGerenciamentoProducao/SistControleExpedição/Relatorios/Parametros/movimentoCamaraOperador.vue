<template>
      <v-row >
      <v-col cols="2" sm="2" md="5" style="padding-left:40px;">
        <small>Selecione a(s) Câmara(s):</small>
        <v-checkbox v-model="selectChambers" label="Todas" dense style="transform: scale(0.95);margin:0;"></v-checkbox>
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
        </div>
        <div class="loading" v-if="isLoadingChambers">
          <v-progress-circular
              :size="50"
              indeterminate
              color="primary"
            ></v-progress-circular>
        </div>
        <small>Matrícula</small>
        <v-text-field dense outlined v-model="codSicop" type="number" min="0" @input="SendCodSicop(codSicop)" hide-details></v-text-field>
      </v-col>
      <v-col cols="4" sm="3" md="7" style="padding-left:10px;">
        <small>Tipo de Movimentação</small>
        <v-radio-group v-model="rgTypeMove" dense row style="margin:0;">
          <v-radio label="Por Câmara" value="1"></v-radio>
          <v-radio label="Por Operador" value="2" ></v-radio>
        </v-radio-group>
         <hr>
        <small>Tipo de Operação</small>
        <v-radio-group v-model="rgTypeOperation" row dense style="margin:0;">
          <v-radio label="Armazenado" value="1"></v-radio>
          <v-radio label="Expedido" value="2" ></v-radio>
          <v-radio label="Todos" value="3" ></v-radio>
        </v-radio-group>
         <hr>
        <small>Turno</small>
        <v-radio-group v-model="rgShift" row dense style="margin:0;">
          <v-radio label="1º" value="1"></v-radio>
          <v-radio label="2º" value="2" ></v-radio>
          <v-radio label="3º" value="3" ></v-radio>
          <v-radio label="Todos" value=4 ></v-radio>
        </v-radio-group>
         <hr>
      </v-col>
      <v-col cols="2" sm="2" md="5" style="padding-left:60px;padding-top:0;">
       <small class="text-title">Data Inicial</small>
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;"></v-text-field>
          </template>
          <v-date-picker v-model="date" @input="setDate(date)" locale="pt" min="1950-01-01" :max="dateMax" ></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="2" sm="2" md="5" style="padding-left:40px;padding-top:0;">
        <small class="text-title">Data Final</small>
        <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field  v-model="dateFormatted2" @blur="date2 = parseDate(dateFormatted2)" prepend-icon="mdi-calendar" 
            readonly outlined hide-details dense v-bind="attrs" v-on="on" style="padding-top:5px;"></v-text-field>
          </template>
          <v-date-picker v-model="date2" @input="setDate(date2)" locale="pt" min="1950-01-01" :max="dateMax" ></v-date-picker>
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
export default class MovimentoCamaraOperador extends Vue {
  @Action getChambersByFilial

  @Getter filialName
  @Getter allChambers
 
  @Prop() clearFields!: any; 

  private codSicop: any = null;
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
    this.dateFormatted = this.formatDate(value);
    this.$emit('getInitialDate', value);
  }

  @Watch('date2')
  public async onPropertyChangedsDate2(value: any, oldValue: any): Promise < void > {
    this.dateFormatted2 = this.formatDate(value);
    this.$emit('getEndDate', value);
  }

  @Watch('rgShift')
  public async onPropertyChangedsShift(value: any, oldValue: any): Promise < void > {
    this.$emit('getShift', value);
  }

  @Watch('rgTypeMove')
  public async onPropertyChangedsMovement(value: any, oldValue: any): Promise < void > {
    this.$emit('getMovement', value);
  }

  @Watch('rgTypeOperation')
  public async onPropertyChangedsOperation(value: any, oldValue: any): Promise < void > {
    this.$emit('getOperation', value);
  }

  @Watch('idChambers')
  public async onPropertyChangedsChambers(value: any, oldValue: any): Promise < void > {
    this.$emit('getIdChambers', value); 
  }

  public SendCodSicop(cod: any): void{
    this.$emit('getCodSicop', cod);
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

  public InitialParameters(): void{
    this.$emit('getOperation', '3');
    this.$emit('getMovement', '1');
    this.$emit('getShift', '4');
    this.$emit('getInitialDate', this.date);
    this.$emit('getEndDate', this.date2);
  }
  async mounted() {
    if (this.allChambers === null) {
      this.isLoadingChambers = true;
      await this.getChambersByFilial({filial: this.filialName });
      this.isLoadingChambers = false;
    }
    this.InitialParameters();
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