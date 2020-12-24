<template>
<v-card width="100%" height="355">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
    <div class="close-box">
      <v-icon color="white" @click="closeModal()">mdi-close</v-icon>
    </div>
  </div>
  <v-card-actions>
      <!-- Todos os Parametros vão aqui --> 
      <AgendamentoSemEmbarques v-on="{getInitialDate, getEndDate, resetClearFields}" v-bind="{clearFields}"  v-if="idBox === 1"/>

    <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" :disabled="!canPrint()" style="text-transform: none;">
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
import _ from 'vuetify/es5/components/*';

import AgendamentoSemEmbarques from '../Relatorios/Parametros/agendamentosSemEmbarqueCargas.vue';

@Component({
  components: {
    AgendamentoSemEmbarques
  }
})
export default class InputModalAgendamentoCarga extends Vue {
  
  @Prop() nameBox!: any
  @Prop() idBox!: any;

 
  @Action setSelectedIdReport
  @Action noShowReport
  @Action reportAgendamentoSemEmbarque

  @Getter filialName
  @Getter loginUser
  @Getter userFeatures
  @Getter showReport

  private clearFields: boolean = false;
  private initialDate: any = null;
  private endDate: any = null;

  public async closeModal(): Promise<void> {
    this.clearFields = true;
    this.$emit('closeModal');
  }

  public getInitialDate(date: any): void {
    this.initialDate = date;
  } 

  public getEndDate(date: any): void {
    this.endDate = date;
  }

  public canPrint(): boolean {
    switch(this.idBox){
    case 1:
      return true;
    default:
      return false;   
    }
  }

  public async Print(): Promise < void > {
    if(this.showReport === true){
      await this.noShowReport({show: false});
    }
    switch(this.idBox){
    case 1:
      await this.AgendamentoSemEmbarques();
      break;
    }
    this.closeModal();
  }

  public async AgendamentoSemEmbarques(): Promise<void>{
    await this.reportAgendamentoSemEmbarque({initialDate: this.initialDate, endDate: this.endDate, idReport: 1, reportModule:6 });
  }
 
  public resetClearFields(): void{
    this.clearFields = false;
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
