using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class EmentaSemanal
    {
        public DateTime Data { get; set; }
        public int Receitaid { get; set; }
        public int Utilizadorid { get; set; }
        [JsonIgnore]
        public Receita Receita { get; set; }
        [JsonIgnore]
        public Utilizador Utilizador { get; set; }
    }
}
