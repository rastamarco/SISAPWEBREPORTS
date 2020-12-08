<template>
      <v-col cols="12" sm="10" md="10" style="padding-left:22%;" >
          <v-date-picker v-model="date" type="month" @input="SendDate(date)" locale="pt" 
            min="1950-01-01" :max="dateMax" no-title></v-date-picker>
      </v-col>
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
export default class FomentoMensal extends Vue {
  @Prop() clearFields!: any; 

  private date: any = new Date().toISOString().substr(0, 7) ;
  private menu: boolean = false;
  private dateMax = new Date().toISOString().substr(0, 10);
  
  @Watch('clearFields')
  public async onPropertyChangeds(value: any, oldValue: any): Promise < void > {
    this.date = new Date().toISOString().substr(0, 7);
    this.$emit('resetClearFields');
  }

  public SendDate(date: any): void {
    this.$emit('getMonthYear', date);
  }
}
</script>