using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class UtilizadorPasso
    {
        public int Utilizadorid { get; set; }
        public int Passoid { get; set; }
        public int PassoReceitaid { get; set; }
        public string Comentario { get; set; }
        [JsonIgnore]
        public Passo Passo { get; set; }
        [JsonIgnore]
        public Utilizador Utilizador { get; set; }
    }
}
