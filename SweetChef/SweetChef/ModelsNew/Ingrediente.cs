using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Ingrediente
    {
        public Ingrediente()
        {
            PassoIngrediente = new HashSet<PassoIngrediente>();
            ReceitaIngrediente = new HashSet<ReceitaIngrediente>();
            RestricoesAlimentares = new HashSet<RestricoesAlimentares>();
        }

        public int Id { get; set; }
        [JsonIgnore]
        public int Unidadeid { get; set; }
        public string Nome { get; set; }
        public string ImageLink { get; set; }

        public Unidade Unidade { get; set; }

        [JsonIgnore]
        public ICollection<PassoIngrediente> PassoIngrediente { get; set; }
        [JsonIgnore]
        public ICollection<ReceitaIngrediente> ReceitaIngrediente { get; set; }
        [JsonIgnore]
        public ICollection<RestricoesAlimentares> RestricoesAlimentares { get; set; }
    }
}
