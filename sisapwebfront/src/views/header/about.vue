<template>
   <v-dialog v-model="dialog" @keyup.esc="closeModal(false)" width="35%" >
    <template v-slot:activator="{ on }">
    <v-list-item v-on="on"  @click="dialog = false">
      <v-list-item-action >
          <v-icon color="primary">mdi-comment-question</v-icon>
      </v-list-item-action>
      <v-list-item-title>Sobre</v-list-item-title>
    </v-list-item>
    </template>
    <v-card class="about-system">
        <div class="content">
              <div class="content-img">
                  <v-img src="https://www.lar.ind.br/logo_lar.png" contain alt="John"/>
              </div>
              <div class="content-infos">
                  <span style="font-size: 16px;font-weight: bold;padding-right: 50px;">SISAP Web <small>Versão 2.0.0</small></span>
                  <span style="font-size: 14px;">Sistema de Apontamento de Produção, Controle da Produção, Controle de Qualidade, Embarques, Paradas e Expedição da Produção.</span><br>
                  <span style="font-size: 14px;">
                    UIA - Unidade Industrial de Aves I, II, III e IV<br>
                    UIC - Unidade Industrial de Carnes<br>
                    UPO - Unidade Produtora de Ovos<br>
                    UIS - Unidade Industrial de Soja<br>
                  </span><br>
                  <span style="font-size: 15px;font-weight: bold;">Ultima Atualização: 11/12/2020 </span>
              </div>
              <v-btn rounded text absolute bottom right color="primary" style="text-transform: none;" @click="dialog = false">Fechar</v-btn>
        </div>
    </v-card>
   </v-dialog>
</template>
<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import {
  Getter
} from 'vuex-class';

@Component
export default class About extends Vue{
  private dialog: boolean = false;
}
</script>
<style scoped>
.about-system{
  display:flex;
  flex-direction:column;
  justify-content:center;
  height: 310px;
}

.about-system .content{
  display:flex;
  flex-direction:row;
  justify-content:center;
  height:100%;
}

.about-system .content .content-img{
  display:flex;
  flex-direction:column;
  justify-content:center;
  align-items: center;
  height:35%;
  width: 25%;
}

.about-system .content .content-infos{
  display:flex;
  flex-direction:column;
  justify-content:flex-start;
  padding-top: 25px;
  align-items: center;
  height: 85%;
  width: 70%;
}


</style>