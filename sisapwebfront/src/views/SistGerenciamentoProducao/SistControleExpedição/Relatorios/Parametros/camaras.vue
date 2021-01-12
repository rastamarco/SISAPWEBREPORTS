<template>
      <v-row >
      <v-col cols="12" sm="6" md="12" class="content">
        <span style="font-size: 17px;">Selecione a Câmara:</span>
        <div class="content-checkbox" v-if="!isLoadingChambers" >  
          <v-select dense :items="allChambers" v-model="idChambers" placeholder="Selecione" item-value="cod_camara" item-text="cod_camara" outlined style="padding-top:10px;" append-icon="mdi-format-columns"></v-select>
          <small v-if="allChambers === null" style="color: red;">Não foi possível obter a lista de Câmaras.</small>
        </div>
        <div class="loading" v-if="isLoadingChambers">
          <v-progress-circular
              :size="50"
              indeterminate
              color="primary"
            ></v-progress-circular>
        </div>
      </v-col>
      <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" :disabled="!canPrint()" style="text-transform: none;">
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
export default class Camaras extends Vue {
  @Action getChambersByFilial
  @Action reportCamara
  @Action ReportPesoProdutoCamara
  @Action SetIdBox
  @Action noShowReport

  @Getter showReport
  @Getter filialName
  @Getter allChambers
  @Getter box

  @Prop() clearFields!: any; 
  @Prop() idBox!: any

  private idChambers: Array<any> = [];
  private isLoadingChambers: boolean = false;
  private selectChambers: boolean = false;

  @Watch('selectChambers')
  public async onPropertyChangedsChamber(value: any, oldValue: any): Promise < void > {
    if(value === true){
      this.allChambers.forEach(chambers =>{
        this.idChambers.push(chambers.cod_camara);
      });
    } else {
      if (this.idChambers.length === this.allChambers.length){
        this.idChambers.splice(0, this.idChambers.length);
      } else {
        this.selectChambers = false; 
      } 
    }
  }

  @Watch('idChambers')
  public async onPropertyChangedsChambers(value: any, oldValue: any): Promise < void > {
    for(let i = 0; i < this.allChambers.length; i++){
      if (this.idChambers.length !== this.allChambers.length){
        this.selectChambers = false; 
      } 
    }
  }

   @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.idChambers.splice(0, this.idChambers.length);
    this.$emit('resetClearFields');
  }

   public canPrint(): boolean {
     if(this.idChambers.length > 0){
       return true;
     } else 
       return false;
   }

   public async Print(): Promise<void> {
     if(this.showReport === true){
       await this.noShowReport({show: false});
     }
     if(this.box === 3){
       await this.reportCamara({chambers: this.idChambers, reportModule: 2, idReport: 41});
     }else { 
       await this.ReportPesoProdutoCamara({idChamber: this.idChambers, idReport: 52, reportModule: 2});
     }
     await this.SetIdBox({id: null});
     this.closeModal();
   }

   public async closeModal(): Promise<void> {
     this.$emit('closeModal');
   }

   async mounted() {
     if (this.allChambers === null) {
       this.isLoadingChambers = true;
       await this.getChambersByFilial({filial: this.filialName });
       this.isLoadingChambers = false;
     }
   }
}
</script>
<style scoped>
.content{
  display:flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding-top: 15%;
}

.loading{
  display:flex;
  justify-content:center;
}


.content .content-checkbox{
  display:flex;
  flex-direction: row;
  width: 30%;
  flex-wrap: wrap;
}

.content-checkbox .item-checkbox{
  display: flex;
  flex-direction: row;
  padding-left: 15px;
  margin:0;
}

</style>