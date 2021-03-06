﻿namespace sisapWebApi.Reports.ReportModels
{
    public class ReportQuery
    {
        // Formato do Arquivo, pdf, xlsx (Caso precise tem outros também como PNG, basta converter
        public string Format { get; set; }
        // Aqui verifica se é Inline=True ou não, se for mostra o arquivo no IFRAME se não volta com download
        public bool Inline { get; set; }
        
        // Redirecionamento dos módulos
        public int reportModule { get; set; }

        // Obtem o nome da Filial para redirecionar para os relatórios para as devidas unidades
        public string filialName { get; set; }

        // Variável que armazena os parametros enviados na selação do relatório
        public string Parameter { get; set; }
    }
}
