<template>
<div>
  <div class="action-buttons">
    <div class="save">
      <v-menu transition="slide-y-transition" bottom dense>
        <template v-slot:activator="{ on, attrs }">
          <v-btn text color="primary" dark v-bind="attrs" v-on="on" class="btn-download">
            <v-icon>mdi-download</v-icon>
            Salvar
          </v-btn>
        </template>
        <v-list dense>
          <v-list-item v-for="(item, i) in items" :key="i">
            <v-list-item-title @click="teste(backendPath+item.type)"> {{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </div>
    <div class="export-excel">
      <v-btn text color="primary" dark class="btn-download" @click="teste()">
        <v-icon>mdi-file-excel</v-icon>
        Gerar Excel
      </v-btn>
    </div>
    <div class="close-printer">
      <v-btn text right color="primary" dark class="btn-download" @click="ClosePrinter()">
        <v-icon>mdi-close</v-icon>
        Fechar
      </v-btn>
    </div>
  </div>
  <!-- 
  <a v-bind:href="backendPath+item.type" target="_blank"></a>
  <h2>Inline reports</h2>
  <input type="radio" id="master" value="1?format=html&inline=true" v-model="selectedReport">
  <label for="one">Master-Detail</label>
  <br>
  <input type="radio" id="bar" value="2?format=html&inline=true" v-model="selectedReport">
  <label for="one">Barcodes</label> 
  <br> -->
  <!-- <iframe v-bind:src="backendPath+selectedReport" width="100%" height="540" class="frame" frameborder="0" type="application/pdf"> -->
  <iframe v-bind:src="backendPath+selectedReport" width="100%" height="560" class="frame" type="application/pdf" />
</div>
</template>

<script lang="ts">
import {
  Component,
  Vue,
  Prop,
  Watch
} from 'vue-property-decorator';
import {
  Action,
} from 'vuex-class';
@Component
export default class Reports extends Vue {
  /* eslint-disable indent */
  @Action reportApontamentoResfriadoFalse

  private backendPath: any = 'http://localhost:54687/api/reportgeneration/';
  // private modalReports: boolean = false;
  private selectedReport: any = '1?format=pdf&inline=true';
  private items: Array < any > = [{
      title: 'Download PDF',
      type: '1?format=pdf'
    },
    {
      title: 'Download PNG',
      type: '1?format=png'
    },
    {
      title: 'Download Excel',
      type: '1?format=xlsx'
    },
  ];

  public teste(): void {
    window.open(this.backendPath + this.selectedReport);
  }

  public async ClosePrinter(): Promise < void > {
    await this.reportApontamentoResfriadoFalse({
      date: null,
      period: null,
      turno: null
    });
  }
  // public printDocument(): any {
  //   // const  iframe = document.querySelector('iframe');
  //   // console.log(iframe.contentDocument.domain);
  //   // console.log(document.getElementById('printf'));
  //   // window.frames['printf'].focus();
  //   // window.frames['printf'].print();
  //   const element: HTMLIFrameElement = document.getElementById('printf') as HTMLIFrameElement;
  //   const iframe = element.contentWindow;
  //   if (iframe !== null) {
  //     window.open(element.src);
  //   } else {
  //     this.$swal('Ops!', 'Algo deu Errado ao tentar imprimir esse arquivo! Aguarde um pouco e tente novamente, ou contate a equipe de Suporte.', 'error');
  //   }
  // }

  // public teste(url: any): void {
  //   this.modalReports = true;
  //   // window.open(this.backendPath + '1?format=pdf');
  //   // const fileURL = window.URL.createObjectURL(new Blob([responseGetDocument.data]));
  //   // const fileLink = document.createElement('a');
  //   // fileLink.href = fileURL;

  //   // fileLink.setAttribute('download', `${name}.${extension}`);
  //   // document.body.appendChild(fileLink);
  //   // const fileURLView = window.URL.createObjectURL(new Blob([responseGetDocument.data], {
  //   //   type: typeDocument
  //   // }));
  // }

}
</script>

<style scoped>
.frame {
  padding-left: 15px;
  padding-right: 15px;
}

.btn-download {
  margin: 10px;
  text-transform: none;
}

.action-buttons {
  width: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
  height: 40px;
}

.action-buttons .save {
  display: flex;
  width: 35%;
  justify-content: flex-start;
}

.action-buttons .export-excel {
  display: flex;
  width: 30%;
  justify-content: center;
}

.action-buttons .close-printer {
  display: flex;
  width: 35%;
  justify-content: flex-end;
}
</style>
