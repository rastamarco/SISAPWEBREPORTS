<template>
<v-card width="100%" height="435" style="overflow:hidden;">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
    <div class="close-box">
      <v-icon color="white" @click="closeModal()">mdi-close</v-icon>
    </div>
  </div>
  <v-card-actions>
     <v-row>
      <!-- Componentes para parametros Siga a sequencia que tudo vai dar certo! -->
      <!-- Relatório de Formação de Pallet --> 
      <formacaoPallet v-on="{resetClearFields, closeModal}" v-bind="{clearFields}" v-if="idBox === 1"/>
      <!-- Relatório de Movimento de Câmara/Operador -->
      <movimentoCamaraOperador  v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 2" />
      <!-- Mapa de Câmaras || Peso de Produtos Estocados nas Câmaras-->
      <camaras v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 3 || idBox === 7" />
      <!-- Localização de Produtos -->
      <localizacaoProduto v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 4" />
      <!-- Histórico Camara Pallet -->
      <historicoCamaraPallet v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 5" />
      <!-- Produtos Embarcados --> 
      <produtosEmbarcados v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 8" />
      <!-- Embarques Períodos --> 
      <embarquesDesembarques v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 9 || idBox === 10"  />
      <!-- Entrada Saída do Túnel de Congelamento --> 
      <entradaSaidaTunelCongelamento v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 11" />
      <!-- Cargas embarcadas e Países Destinos --> 
      <dataFinalDataInicial v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 12 || idBox === 14"/>
      <!-- Reagendamento de Cargas --> 
      <reagendamentoCargas v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 13" />
      <!-- Produtos por SIF --> 
      <produtosPorSif v-on="{closeModal, resetClearFields}" v-bind="{clearFields}" v-if="idBox === 16" />
    </v-row>
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

import formacaoPallet from '../Relatorios/Parametros/formacaoPallet.vue';
import movimentoCamaraOperador from '../Relatorios/Parametros/movimentoCamaraOperador.vue';
import camaras from '../Relatorios/Parametros/camaras.vue';
import localizacaoProduto from '../Relatorios/Parametros/localizacaoProdutos.vue';
import historicoCamaraPallet from '../Relatorios/Parametros/historicoCamaraPallet.vue';
import produtosEmbarcados from '../Relatorios/Parametros/produtosEmbarcados.vue';
import embarquesDesembarques from '../Relatorios/Parametros/embarquesDesembarques.vue';
import dataFinalDataInicial from '../Relatorios/Parametros/dataInicialFinal.vue';
import reagendamentoCargas from '../Relatorios/Parametros/reagendamentoCargas.vue';
import produtosPorSif from '../Relatorios/Parametros/produtosPorSIF.vue';
import entradaSaidaTunelCongelamento from '../Relatorios/Parametros/entradaSaidaTunelCongelamento.vue';

@Component ({
  components: {
    formacaoPallet,
    movimentoCamaraOperador,
    camaras,
    localizacaoProduto,
    historicoCamaraPallet,
    produtosEmbarcados,
    embarquesDesembarques,
    dataFinalDataInicial,
    reagendamentoCargas,
    produtosPorSif,
    entradaSaidaTunelCongelamento
  }
})
export default class InputModalsExp extends Vue {
  @Prop() nameBox!: any;
  @Prop() idBox!: any;

  @Action setSelectedIdReport
  @Action noShowReport
  @Action ReportPosicaoCamaraVazia
  @Action ReportPaisesDestinoHabilitacao
  @Action SetIdBox

  @Getter filialName
  @Getter userFeatures
  @Getter showReport
  @Getter box

  private clearFields: boolean = false;

  @Watch('box')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    switch(value){
    case 6: 
      this.closeModal();
      await this.ReportPosicaoVazia();
      break;
    case 15: 
      this.closeModal();
      await this.ReportPaisesDestino();
      break;
    } 
  }

  public resetClearFields(): void{
    this.clearFields = false;
  }

  public async closeModal(): Promise<void> {
    this.clearFields = true;
    this.$emit('closeModal');
  }

  public async ReportPosicaoVazia(): Promise<void> {
    this.closeModal();
    await this.ReportPosicaoCamaraVazia({filialName: this.filialName, idReport: 51, reportModule: 2 });
  }

  public async ReportPaisesDestino(): Promise<void> {
    this.closeModal();
    await this.ReportPaisesDestinoHabilitacao({filialName: this.filialName, idReport: 65, reportModule: 2 });
  }

  async mounted(){
    // Forçando aparecer a leitura do idBox no inicio para  abrir o relatório direto caso for o id 6 que não precisa de parametros
    switch(this.box){
    case 6: 
      await this.ReportPosicaoVazia();
      this.closeModal();
      break;
    }
    await this.SetIdBox({id: null}); 
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
  width:70%;
}

.title .close-box {
  padding-right: 16px;
  display:flex;
  justify-content: flex-end;
  font-size: 22px;
  width:30%;
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
