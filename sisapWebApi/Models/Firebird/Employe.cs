using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Models.Firebird
{
    public class Employe
    {
        public int COD_MATRICULA { get; set; }
        public string COD_FILIAL { get; set; }
        public int COD_TURNO { get; set; }
        public string NO_FUNCIONARIO { get; set; }
        public string DESC_FUNCAO { get; set; }
        public string NO_SENHA { get; set; }
        public string ATIVO { get; set; }
        public int COD_SETOR { get; set; }
        public string CARTAQ_SEGURANCA { get; set; }
    }
}
