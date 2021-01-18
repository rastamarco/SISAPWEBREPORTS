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
    <v-card class="about-system" style="overflow:hidden;">
        <div class="content">
          <v-row>
            <v-col cols="23" sm="3" md="3" class="img">
                  <v-img :src="require('./../../assets/logo-lar.png')" contain/>
            </v-col>
            <v-col cols="4" sm="3" md="9" class="title-initial">
                  <span style="font-size: 16px;font-weight: bold;text-align:center;">SISAP Web <small>Versão 2.0.0</small></span><br>
                  <span style="font-size: 14px;text-align:center;">Sistema de Apontamento de Produção, Controle da <br>Produção, Controle de Qualidade, Embarques, Paradas e Expedição da Produção.</span><br>
                  <span style="font-size: 14px;padding-left:15px;">
                    UIA - Unidade Industrial de Aves I, II, III e IV<br>
                    UIC - Unidade Industrial de Carnes<br>
                    UPO - Unidade Produtora de Ovos<br>
                    UIS - Unidade Industrial de Soja<br>
                  </span><br>
                  <span style="font-size: 15px;font-weight: bold;padding-left:15px;">Ultima Atualização: 11/12/2020 </span>
            </v-col>
          </v-row>
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
  height: 325px;
}

.about-system .content{
  display:flex;
  flex-direction:row;
  justify-content:center;
  height:100%;
}

.about-system .content .img{
  display:flex;
  align-items:center;
  justify-content:center;
  height:30%;
  margin-top:35px;
  padding-left:35px;
}

.about-system .content .title-initial{
  display:flex;
  flex-direction: column;
}

</style>