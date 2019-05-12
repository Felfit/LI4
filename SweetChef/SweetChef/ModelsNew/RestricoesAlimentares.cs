using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class RestricoesAlimentares
    {
        public int Utilizadorid { get; set; }
        public int Ingredienteid { get; set; }
        [JsonIgnore]
        public Ingrediente Ingrediente { get; set; }
        [JsonIgnore]
        public Utilizador Utilizador { get; set; }
    }
}
