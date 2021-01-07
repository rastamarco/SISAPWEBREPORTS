<template>
<v-container>
  <v-row class="text-center">
    <v-col cols="12">
      <v-img :src="require('../assets/logo-larnet.png')" class="my-3" contain height="40" />
    </v-col>

    <v-col>
      <h1 class="display-1 font-weight-bold mb-2">
        Portal SISAP
      </h1>
      <h6>Entre com suas credenciais de Acesso ao Sistema</h6>
    </v-col>
    <v-col class="box-card-login" cols="12">
    <v-form @submit.prevent="submit">
      <v-card width="500" height="320" elevation="2" class="card-items" >
        <span class="text-title"> Local </span>
        <v-select dense :items="items" v-model="local" outlined style="padding-top:10px;" append-icon="mdi-map-marker"></v-select>
        <span class="text-title"> Usuário </span>
        <v-text-field dense outlined v-model="user" placeholder="ex: qualidade" style="padding-top:10px;" append-icon="mdi-account"> </v-text-field>
        <span class="text-title"> Senha </span>
        <v-text-field dense outlined v-model="password" :type="show ? 'text' : 'password'" placeholder="*******" style="padding-top: 5px;" :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'" @click:append="show = !show"> </v-text-field>
        <v-btn color="primary" type="submit" style="text-transform: none;width: 90px;margin-left: 190px;">Login</v-btn>
      </v-card>
      </v-form>
    
      <v-progress-circular v-if="loading" indeterminate color="primary" style="position: absolute;margin-top:100px;" :size="100" :width="5">
      </v-progress-circular>
    </v-col>
    <v-col class="mb-5" cols="12">
    </v-col>
    <v-col class="mb-5" cols="12">
      <h5 style="font-size: 11px;font-weight: bold;">Cooperativa Agroindustrial Lar</h5>
      <h5 style="font-size: 11px;">SISAP Web</h5>
      <h6 style="font-size: 11px;">Versão 2.0.0</h6>
    </v-col>
  </v-row>
</v-container>
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

@Component({})
export default class Login extends Vue {
  @Action doLogin

  @Getter isAuthenticated
  
  private items: any = null;
  private local: any = null;
  private user = null;
  private password = null;
  private show = false;
  private loading = false;
  private url: any = null;
  
  public async submit(): Promise < void > {
    this.loading = true;
    await this.doLogin({
      codFilial: this.local,
      login: this.user,
      senha: this.password
    });
    this.loading = false;
    if (!this.isAuthenticated) {
      this.$swal('Ops!', 'Algum erro ocorreu no seu Login, reveja suas credenciais e refaça o login, se o erro persistir contate a equipe de Suporte.', 'error');
    } else {
      const tokenEncoded: any = window.localStorage.getItem('token');
      const tokenDecoded = JSON.parse(atob(tokenEncoded));
      this.$root.$data.$token = tokenDecoded;
      this.$router.push('PortalSISAP');
    }
    this.clearFields();
  }

  public clearFields(): void {
    this.password = null;
    this.user = null;
    this.local = this.items[0];
  }

  public GetItemsByFilial(url: any): void{
    const filial = url.split('//');
    switch (filial[1]){
    case '10.10.11.32/':
      this.items = ['UIA', 'UIC', 'GBA', 'UPP', 'SIF'];
      this.local = this.items[0];
      break;
    case '10.10.128.100/':
      this.items = ['UIAII'];
      this.local = this.items[0];
      break;
    case '192.168.1.246/':
      this.items = ['UIAIII'];
      this.local = this.items[0];
      break;
    case '172.16.5.66/':
      this.items = ['UDM'];
      this.local = this.items[0];
      break;
    case 'localhost:8080/':
      this.items = ['UIA', 'UIC', 'GBA', 'UPP', 'SIF', 'UIAII', 'UIAIII', 'UDM'];
      this.local = this.items[0];
      break;
    }
  }

  mounted() {
    this.GetItemsByFilial(window.location.href);
  }
}
</script>

<style scoped>
.box-card-login {
  display: flex;
  flex-direction: row;
  justify-content: center;
}

.box-card-login .card-items {
  display: flex;
  flex-direction: column;
  padding: 15px;
}

.box-card-login .card-items .text-title {
  text-align: left;
  padding-left: 5px;
  color: black;
  opacity: 0.6;
  height: 25px;
}
</style>
