<template>
<v-row>
  <v-col cols="12" sm="12">
    <v-sheet  rounded="lg" height="84vh">
      <div class="initial" v-if="!showReport">
        <div class="system-text">
          <SystemTitles />
        </div>
        <div class="system-img">
          <v-img :src="require('../../assets/logo-lar.png')" class="my-3" contain height="230" />
        </div>
        <div class="footer-initial">
        </div>
      </div>
      <!-- TODO -->
      <!-- <Reports v-if="showReport" /> -->
    </v-sheet>
  </v-col>
</v-row>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import {
  Action,
  Getter
} from 'vuex-class';

import SystemTitles from '../header/system-tittles.vue';

@Component({
  components: {
    SystemTitles
  }
})
/* eslint-disable indent */
export default class AdminHome extends Vue {
  @Action reloadUser

  @Getter showReport

  beforeMount() {
    if (window.localStorage.token) {
      this.reloadUser({
        token: window.localStorage.token
      });
    }
  }
}
</script>

<style scoped>
.initial {
  width: 100%;
  height: 400px;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.initial .system-text {
  width: 100%;
  height: 20%;
  display: flex;
  justify-content: center;
}

.initial .system-img {
  width: 100%;
  height: 80%;
  padding-left: 50px;
  padding-top: 50px;
}
</style>
