<template>
<v-card width="100%" height="350">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
    <div class="close-box">
      <v-icon color="white" @click="closeModal()">mdi-close</v-icon>
    </div>
  </div>
  <v-card-actions>
     <v-row>
      <formacaoPallet v-on="{getNrPallet}" v-if= "idBox === 1"/>
      <!-- <v-col cols="12" sm="6" md="4">
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <small class="text-title">Data</small>
            <v-text-field id="teste" v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" readonly outlined hide-details dense v-bind="attrs" v-on="on" class="date-input"></v-text-field>
          </template>
          <v-date-picker v-model="date" @input="setDate(date)" locale="pt" min="1950-01-01" :max="dateMax"></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" sm="6" md="4">
        <small class="text-title">Turno </small>
        <v-radio-group v-model="turnoGroup" style="padding-left: 30px;">
          <v-radio v-for="n in 4" :key="n" :label="getTurnos(n)" :value="n"></v-radio>
        </v-radio-group>
      </v-col>
      <v-col cols="12" sm="6" md="4" >
        <small class="text-title" v-if="showPeriodApontamento()" >Período</small>
        <v-radio-group v-model="periodoGroup" style="padding-left: 30px;" v-if="showPeriodApontamento()">
          <v-radio v-for="n in 3" :key="n" :label="getPeriodo(n)" :value="n"></v-radio>
        </v-radio-group>
      </v-col> -->
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

import formacaoPallet from './formacaoPallet.vue';

@Component ({
  components: {
    formacaoPallet
  }
})
export default class InputModalsExp extends Vue {
  @Prop() nameBox!: any
  @Prop() idBox!: any;

  @Action reportFormacaoPallets
  @Action setSelectedIdReport

  @Getter filialName
  @Getter userFeatures

  private dateToSend: any = null;
  private turnoGroup: any = 1;
  private periodoGroup: any = 1;
  private nrPallet: any = null;

  public getNrPallet(nrPallet: any): void {
    this.nrPallet = nrPallet;
  }
  
  public closeModal(): void {
    this.$emit('closeModal');
  }

  public async Print(): Promise < void > {
    switch(this.idBox){
    case 1:
      await this.ReportFormacaoPallet(this.nrPallet);
      break;
    case 2: 
      // Outro Report
      break; 
    }
    this.closeModal();
  }

  public async ReportFormacaoPallet(nrPallet: any): Promise<void> {
    await this.reportFormacaoPallets({ 
      idReport: 16,
      nrPallet: nrPallet
    });
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
