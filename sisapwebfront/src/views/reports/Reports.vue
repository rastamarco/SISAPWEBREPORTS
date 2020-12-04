<template>
<div>
  <div class="action-buttons">
    <div class="save">
      <v-btn text color="primary" dark class="btn-download" @click="DownloadDocument()">
        <v-icon>mdi-download</v-icon>
        Download
      </v-btn>
    </div>
    <div class="export-excel">
      <v-btn text color="primary" dark class="btn-download" @click="fileExcel()">
        <v-icon>mdi-file-excel</v-icon>
        Gerar Arquivo Excel
      </v-btn>
    </div>
    <div class="close-printer">
      <v-btn text right color="primary" dark class="btn-download" @click="ClosePrinter()">
        <v-icon>mdi-close</v-icon>
        Fechar
      </v-btn>
    </div>
  </div>
  <iframe v-bind:src="backendPath+idReport+pathReport+moduleReport+reportModule+parameters+params" width="100%" height="560" class="frame" type="application/pdf" />
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
  Action, Getter
} from 'vuex-class';
@Component
export default class Reports extends Vue {
  /* eslint-disable indent */
  @Action noShowReport
  @Action setSelectedIdReport

  @Getter idReport
  @Getter params
  @Getter reportModule 

  private backendPath: any = `${process.env.VUE_APP_API_URL}/api/reportgeneration/`;
  private pathReport: any = '?format=pdf&inline=true';
  private pathDownload: any = '?format=pdf&attachment=true';
  private pathDownloadXLSX: any = '?format=xlsx&attachment=true';
  private moduleReport: any = '&reportmodule=';
  private parameters: any = '&parameter=';
  private itemsDownload: Array < any > = [{
      title: 'Download PDF',
      type: '1?format=pdf'
    },
    {
      title: 'Download Excel',
      type: '1?format=xlsx'
    },
  ];

  public fileExcel(): void {
    window.open(this.backendPath + this.idReport + this.pathDownloadXLSX+this.moduleReport+this.reportModule+this.parameters+this.params);
  }

  public DownloadDocument(): void {
    window.open(this.backendPath + this.idReport + this.pathDownload+this.moduleReport+this.reportModule+this.parameters+this.params);
  }
  
  public async ClosePrinter(): Promise < void > {
    await this.noShowReport({ show: false }); 
    await this.setSelectedIdReport({ id: null });
  }
}
</script>

<style scoped>
.frame {
  padding-left: 15px;
  padding-right: 15px;
  max-height: 100%;
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
