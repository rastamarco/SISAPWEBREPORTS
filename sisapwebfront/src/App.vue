<template>
<v-app id="app" style="background: #EEEEEE">
  <!-- <v-app-bar app color="primary" dark>
    <div class="d-flex align-center">
      </div>
    <v-spacer></v-spacer>
    <span class="mr-2">Versão 2.0.0</span>
    </v-app-bar> -->
  <router-view />
</v-app>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import Login from './views/Login.vue';

@Component({
  components: {
    Login,
  }
})
export default class App extends Vue {

}
</script>
