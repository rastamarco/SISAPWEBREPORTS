using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Models.Firebird
{
    public class Pallet
    {
        public int cod_pallet { get; set; }
        public DateTime dt_pallet { get; set; }
        public long nr_pallet { get; set; }
        public DateTime ho_pallet { get; set; }
        public Double qt_saldo { get; set; }
        public string id_ativo { get; set; }
        public string nr_lote { get; set; }
        public int cod_carregamento { get; set; }
        public string cod_rastreabilidade { get; set; }
        public int motivosequestro { get; set; }
        public string desc_observacoes { get; set; }
        public string id_camara { get; set; }
        public string cod_filial { get; set; }
        public DateTime ho_palletfim { get; set; }
        public DateTime dt_palletfim { get; set; }
        public int cod_fornecedor { get; set; }
        public int cod_matexclusao { get; set; }
        public DateTime dt_excluipallet { get; set; }
        public string tp_pallet { get; set; }
        public string tp_origem { get; set; }
        public int mat_operadoratoma { get; set; }
        public int mat_fiscal { get; set; }
        public int id_localformpallet { get; set; }
        public int id_excecaoformpallet { get; set; }
        public long id_palletauto { get; set; }
        public string ip_gerapallet { get; set; }
        public string ip_excluipallet { get; set; }
        public Decimal qt_peso { get; set; }
        public string id_boletim { get; set; }
        public DateTime dt_hoenvboletim { get; set; }
        public string tp_cms { get; set; }
        public string bloqueio_cq { get; set; }



    }
}
