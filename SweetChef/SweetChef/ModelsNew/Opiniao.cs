using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Opiniao
    {
        public int Receitaid { get; set; }
        public int Utilizadorid { get; set; }
        public bool Favorito { get; set; }
        public short? Rating { get; set; }
        public bool Blacklist { get; set; }
        [JsonIgnore]
        public Receita Receita { get; set; }
        [JsonIgnore]
        public Utilizador Utilizador { get; set; }
    }
}
