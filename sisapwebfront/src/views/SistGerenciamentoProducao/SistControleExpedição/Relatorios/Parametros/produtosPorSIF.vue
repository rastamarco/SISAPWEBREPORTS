<template>
      <v-row >
      <v-col cols="12" sm="6" md="12" class="content">
        <div class="input">
          <span>Produto</span>  
          <v-radio-group v-model="rgSif"  row  dense  style="margin:0;transform: scale(0.9);">
            <v-radio label="Todos" value="1"></v-radio>
            <v-radio label="SIF"  value="2" ></v-radio>
        </v-radio-group>
        </div>
      </v-col>
      <v-col cols="12" sm="6" md="12" class="content">
        <div class="input2">
          <span>Nr. Sif <small>Opcional</small></span>  
          <v-text-field v-model="nrSif" outlined placeholder="ex: 123456789" dense  hide-details style="padding-top: 5px;"></v-text-field>
        </div>
      </v-col>
      <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" style="text-transform: none;">
      <v-icon>mdi-printer</v-icon>
      Imprimir
    </v-btn> 
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
export default class ProdutosPorSif extends Vue {
  @Prop() clearFields!: any; 

  @Action noShowReport
  @Action reportProdutoSif

  @Getter showReport
  @Getter filialName
  @Getter box

  private rgSif: any = '1';
  private nrSif: any = null;

  public Clear(): void{
    this.rgSif = '1';
    this.nrSif = null;
  }

   @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.Clear();
    this.$emit('resetClearFields');
  }

   public async Print(): Promise < void > {
     if(this.showReport === true){
       await this.noShowReport({show: false});
     }
     switch(this.rgSif){
     case '1':
       await this.reportProdutoSif({ idReport: 66, reportModule: 2});
       break;
     case '2':
       await this.reportProdutoSif({ codSicop: this.nrSif, idReport: 67, reportModule: 2});
       break;
     }
     this.closeModal();
   }

   public closeModal(): void {
     this.Clear();
     this.$emit('closeModal');
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
  padding-top:9%;
}

.content .input2{
  display: flex;
  flex-direction: column;
  width:55%;
  align-items: flex-start;
  padding-left: 5%;
  padding-top:1%;
}
</style>  