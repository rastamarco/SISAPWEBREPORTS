<template>
<v-card width="100%" height="325">
  <div class="title">
    <span class="title-box">{{ nameBox }} </span>
  </div>
  <v-card-actions>
    <v-row>
      <v-col cols="12" sm="6" md="4">
        <v-menu v-model="menu" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y min-width="290px">
          <template v-slot:activator="{ on, attrs }">
            <small class="text-title">Data</small>
            <v-text-field id="teste" v-model="dateFormatted" @blur="date = parseDate(dateFormatted)" prepend-icon="mdi-calendar" readonly outlined hide-details dense v-bind="attrs" v-on="on" class="date-input"></v-text-field>
          </template>
          <v-date-picker v-model="date" @input="setDate(date)" locale="pt" min="1950-01-01" :max="dateMax"></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" sm="6" md="4">
        <small class="text-title">Turno</small>
        <v-radio-group v-model="turnoGroup" style="padding-left: 30px;">
          <v-radio v-for="n in 3" :key="n" :label="`${n}º Turno`" :value="n"></v-radio>
        </v-radio-group>
      </v-col>
      <v-col cols="12" sm="6" md="4">
        <small class="text-title">Período</small>
        <v-radio-group v-model="periodoGroup" style="padding-left: 30px;">
          <v-radio v-for="n in 2" :key="n" :label="`${n}º Período`" :value="n"></v-radio>
        </v-radio-group>
      </v-col>
    </v-row>
    <v-btn absolute rounded text bottom left color="primary" @click="closeModal()" style="text-transform: none;">
      Cancelar
    </v-btn>
    <v-btn absolute rounded bottom right color="primary" @click="Print()" :disabled="!canPrint()" style="text-transform: none;">
      <v-icon>mdi-printer</v-icon>
      Imprimir
    </v-btn>
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
  Action,
} from 'vuex-class';
@Component
export default class RelatorioModals extends Vue {
  @Prop() nameBox!: any

  @Action reportApontamentoResfriado

  private menu: boolean = false;
  private date = new Date().toISOString().substr(0, 10);
  private dateMax = new Date().toISOString().substr(0, 10);
  private dateFormatted = this.formatDate(new Date().toISOString().substr(0, 10))
  private dateToSend: any = null;
  private turnoGroup: any = null;
  private periodoGroup: any = null;

  public clearFields(): void {
    this.date = new Date().toISOString().substr(0, 10);
    this.turnoGroup = null;
    this.periodoGroup = null;
  }

  public closeModal(): void {
    this.clearFields();
    this.$emit('closeModal');
  }

  public setDate(data: any): void {
    this.dateToSend = data;
    this.menu = false;
  }

  public formatDate(date: string): any {
    if (!date) return null;
    const [year, month, day] = date.split('-');
    return `${day}/${month}/${year}`;
  }

  public parseDate(date: any): any {
    if (!date) return null;

    const [month, day, year] = date.split('/');
    return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
  }

  public canPrint(): boolean {
    if ((this.periodoGroup === 1 || this.periodoGroup === 2) && (this.turnoGroup >= 1 || this.turnoGroup <= 3)) {
      return true;
    } else {
      return false;
    }
  }
  @Watch('date')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.dateFormatted = this.formatDate(value);
  }
  public async Print(): Promise < void > {
    if (this.dateToSend === null) {
      this.dateToSend = this.date;
    }
    await this.reportApontamentoResfriado({
      date: this.dateToSend,
      period: this.periodoGroup,
      turno: this.turnoGroup
    });
    this.closeModal();
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
