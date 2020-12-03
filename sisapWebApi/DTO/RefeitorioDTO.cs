﻿using AutoMapper;
using System;

namespace sisapWebApi.DTO
{
    public class RefeitorioDTO
    {
            public long Identidade { get; set; }
            public long IDPacote { get; set; }
            public string NomeRefeitorio { get; set; }
            public string CodigoRefeitorio { get; set; }
            public string CodigoOrigem { get; set; }
            public Double Peso { get; set; }
            public DateTime DataProducao { get; set; }
            public DateTime DataValidade { get; set; }
            public DateTime DATAImpressao { get; set; }
    }
}
