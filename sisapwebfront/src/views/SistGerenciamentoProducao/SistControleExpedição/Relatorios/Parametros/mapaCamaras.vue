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
export default class MapaCamaras extends Vue {
  @Action getChambersByFilial

  @Getter filialName
  @Getter allChambers
 
  @Prop() clearFields!: any; 

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
    if(value.length !== 0) {
      this.$emit('getIdChambers', value);
    }else { 
      this.$emit('getIdChambers', null);
    }
  }

   @Watch('clearFields')
  public async onPropertyChangedsClearFields(value: any, oldValue: any): Promise < void > {
    this.idChambers.splice(0, this.idChambers.length);
    this.$emit('resetClearFields');
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