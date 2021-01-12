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
      <AgendamentoSemEmbarques v-on="{closeModal, resetClearFields}" v-bind="{clearFields}"  v-if="idBox === 1"/>

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
