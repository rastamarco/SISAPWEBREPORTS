<template>
<v-menu v-model="menu" :close-on-content-click="false" :nudge-width="200" offset-y max-height="300" max-width="400">
  <template v-slot:activator="{ on, attrs }">
    <v-btn color="white" v-bind="attrs" v-on="on" fab small absolute right>
      <v-icon color="primary">mdi-account</v-icon>
    </v-btn>
  </template>
  <v-card>
    <v-list>
      <v-list-item>
        <v-list-item-avatar>
          <img src="https://www.lar.ind.br/logo_lar.png" alt="John">
        </v-list-item-avatar>
        <v-list-item-content>
          <v-list-item-title><b>{{filialName}} - {{userType}}</b> </v-list-item-title>
          <v-list-item-subtitle>{{nameUser}}</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
    </v-list>
    <v-divider></v-divider>
    <v-list>
      <v-list-item disabled>
        <v-list-item-action>
          <v-icon color="primary">mdi-help-circle</v-icon>
        </v-list-item-action>
        <v-list-item-title>Ajuda</v-list-item-title>
      </v-list-item>
      <about />
      <v-list-item @click="logout()">
        <v-list-item-action>
          <v-icon color="primary">mdi-logout</v-icon>
        </v-list-item-action>
        <v-list-item-title>Logout</v-list-item-title>
      </v-list-item>
    </v-list>
  </v-card>
</v-menu>
</template>

<script lang="ts">
import {
  Component,
  Vue
} from 'vue-property-decorator';
import {
  Getter
} from 'vuex-class';

import about from './about.vue';

@Component({
  components: {
    about
  }
})
export default class HeaderButton extends Vue {
  @Getter filialName
  @Getter nameUser
  @Getter userType

  private menu = false;
  public logout(): void {
    window.localStorage.removeItem('token');
    this.$router.push('/');
  }
}
</script>
