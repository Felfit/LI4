using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Execucao
    {
        public int Receitaid { get; set; }
        public int Utilizadorid { get; set; }
        public DateTime Data { get; set; }
        public int DuracaoTotal { get; set; }
        public int Satisfacao { get; set; }
        public int Dificuldade { get; set; }
        [JsonIgnore]
        public Receita Receita { get; set; }
        [JsonIgnore]
        public Utilizador Utilizador { get; set; }
    }
}
