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
        public int Unidadeid { get; set; }
        public string Nome { get; set; }

        public Unidade Unidade { get; set; }
        public ICollection<PassoIngrediente> PassoIngrediente { get; set; }
        public ICollection<ReceitaIngrediente> ReceitaIngrediente { get; set; }
        public ICollection<RestricoesAlimentares> RestricoesAlimentares { get; set; }
    }
}
