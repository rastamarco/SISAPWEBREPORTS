using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Models.Firebird
{
    public class Product
    {
        public int COD_SICOP { get; set; }
        public string COD_BARRA { get; set; }
        public Double QT_EMBALAGEM { get; set; }
        public string DESC_EMBALAGEM { get; set; }
        public string DESC_RESUMIDA { get; set; }
        public string COD_BARRACX { get; set; }
        public string DESC_ETIQUETA { get; set; }
        public string TP_DATA { get; set; }
        public int COD_GRUPO { get; set; }
        public int  COD_SUBGRUPO { get; set; }
        public int NR_SEQUEN { get; set; }
        public int NR_REGSIF { get; set; }
        public string ID_ATIVO { get; set; }
        public DateTime HO_ALTERA { get; set; }
        public DateTime  DT_ALTMONITORA { get; set; }
        public int ID_QTALTMONITORA { get; set; }
        public string ID_MIUDOS { get; set; }
        public int QT_CXPALLET { get; set; }
        public string  TP_MERCADO { get; set; }
        public int COD_MATPRIMA { get; set; }
        public string TP_RESFRIAMENTO { get; set; }
        public string TP_PALLET { get; set; }
        public string ID_CLASSE { get; set; }
        public int COD_DESTINO { get; set; }
        public Double QT_PESOEMBALAGEM { get; set; }
        public int COD_AGRUPADOR { get; set; }
        public Double QT_PESOPRODEMBALAGEM { get; set; }
        public Double QT_PESOTOLERANCIAPCT { get; set; }
        public int COD_PRODACAMADOOUTAREA { get; set; }
        public int COD_PRODCONVERSAO { get; set; }
        public int COD_PRODUTOMAE { get; set; }
        public string ID_GLOBALGAP { get; set; }
        public string ID_ENVELOPADO { get; set; }
        public int VALIDADE { get; set; }
        public string NO_FAMILIAARMAZ { get; set; }
        public string DESC_PESOEMB { get; set; }
        public string COD_PRODFORNECEDOR { get; set; }
        public Decimal QT_TARAEMB { get; set; }
        public Decimal QT_TARACX { get; set; }
        public Decimal QT_PESOCX { get; set; }
        public string DESC_TEMPTRANSPORTE { get; set; }
        public int COD_FORNECEDOR { get; set; }
        public string TP_APTOPALLET { get; set; }
        public int QT_PECASCAIXA { get; set; }
        public Decimal QT_TARASSTRECH { get; set; }
        public Decimal QT_TARAPALLET { get; set; }
        public string COD_GS1FORNECEDOR { get; set; }
        public string COD_SIFFORNECEDOR { get; set; }
        public string COD_LETRA { get; set; }
        public string COD_NUMERO { get; set; }
        public int VALIDADE_MESES { get; set; }
        public int FORMATO_DATA { get; set; }
        public string VALIDA_LOTE { get; set; }
        public int ALTURA_CAIXA { get; set; }
        public int CAIXAS_NIVEL { get; set; }
        public int QT_NIVEIS { get; set; }
        public string TP_ETQCAIXA { get; set; }
        public string TP_BOLETIN { get; set; }
        public string ID_PESOPALLETEND { get; set; }
        public string ID_ESAJU { get; set; }
        public int ETQ_EAN13_UIC { get; set; }
        public string GERA_BOLETIM_END { get; set; }
        public DateTime DT_INIBOLETIM_END { get; set; }
        public string COD_FILIAL { get; set; }

    }
}
