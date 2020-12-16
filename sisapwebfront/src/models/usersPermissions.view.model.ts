/* eslint-disable @typescript-eslint/class-name-casing */
/* eslint-disable @typescript-eslint/no-explicit-any */
export default class usersPermissionsViewModel {
  private _isApontamento?: boolean | any = false;
  private _isQualidade?: boolean | any = false;
  private _isExpedicao?: boolean | any = false;
  private _isProducao?: boolean | any = false;
  private _isEtiquetas?: boolean | any = false;
  private _isAgendamentoCarga?: boolean | any = false;
  private _isApontamentoParada?: boolean | any = false;
  private _isControleOvos?: boolean | any = false;
  private _isIntegracaoUIA2?: boolean | any = false;

  get isApontamento(): boolean {
    return this._isApontamento;
  }
  set isApontamento(isApontamento: boolean){
    this._isApontamento = isApontamento;
  }
  get isQualidade(): boolean{
    return this._isQualidade;
  }
  set isQualidade(isQualidade: boolean){
    this._isQualidade = isQualidade;
  }
  get isExpedicao(): boolean{
    return this._isExpedicao;
  }
  set isExpedicao(isExpedicao: boolean){
    this._isExpedicao = isExpedicao;
  }
  get isProducao(): boolean{
    return this._isProducao;
  }
  set isProducao(isProducao: boolean){
    this._isProducao = isProducao;
  }
  get isEtiquetas(): boolean{
    return this._isEtiquetas;
  }
  set isEtiquetas(isEtiquetas: boolean){
    this._isEtiquetas= isEtiquetas;
  }
  get isAgendamentoCarga(): boolean{
    return this._isAgendamentoCarga;
  }
  set isAgendamentoCarga(isAgendamentoCarga: boolean){
    this._isAgendamentoCarga = isAgendamentoCarga;
  }
  get isApontamentoParada(): boolean{
    return this._isApontamentoParada;
  }
  set isApontamentoParada(isApontamentoParada: boolean){
    this._isApontamentoParada = isApontamentoParada;
  }
  get isControleOvos(): boolean{
    return this._isControleOvos;
  }
  set isControleOvos(isControleOvos: boolean){
    this._isControleOvos = isControleOvos;
  }
  get isIntegracaoUIA2(): boolean{
    return this._isIntegracaoUIA2;
  }
  set isIntegracaoUIA2(isIntegracaoUIA2: boolean){
    this._isIntegracaoUIA2 = isIntegracaoUIA2;
  }
  
}