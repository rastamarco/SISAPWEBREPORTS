using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sisapWebApi.Models
{
    public class User
    {
        public long codUsuario { get; set; }
        public string nomeUsuario { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string tipoUsuario { get; set; }
        public string codFilial { get; set; }
        public string grupoUsuario { get; set; }
    }
}
