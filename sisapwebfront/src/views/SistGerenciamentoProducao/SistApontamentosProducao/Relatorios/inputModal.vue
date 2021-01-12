<template>
<v-card width="100%" height="335">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
    <div class="close-box">
      <v-icon color="white" @click="closeModal()">mdi-close</v-icon>
    </div>
  </div>
  <v-card-actions>
      <!-- Todos os Parametros vão aqui --> 
      <ApontamentoProducao v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 1" />
      <ApontamentoRefeitorio v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 2" /> 

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
import ApontamentoProducao from '../Relatorios/Parametros/ApontamentoProducao.vue';
import ApontamentoRefeitorio from '../Relatorios/Parametros/ApontamentoRefeitorio.vue';

import {
  Action, Getter
} from 'vuex-class';

@Component({
  components: {
    ApontamentoProducao,
    ApontamentoRefeitorio
  }
})
export default class InputModals extends Vue {
  
  @Prop() nameBox!: any
  @Prop() idBox!: any;

  @Action setSelectedIdReport
  @Action noShowReport

  @Getter showReport

  private clearFields: boolean = false;
  
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
