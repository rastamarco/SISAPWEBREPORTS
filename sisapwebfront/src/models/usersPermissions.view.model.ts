/* eslint-disable @typescript-eslint/class-name-casing */
/* eslint-disable @typescript-eslint/no-explicit-any */
export default class usersPermissionsViewModel {
  private _isApontamento?: boolean | any = false;
  private _isQualidade?: boolean | any = false;
  private _isExpedicao?: boolean | any = false;
  private _isProducao?: boolean | any = false;

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
}