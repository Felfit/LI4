using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class RestricoesAlimentares
    {
        public int Utilizadorid { get; set; }
        public int Ingredienteid { get; set; }

        public Ingrediente Ingrediente { get; set; }
        public Utilizador Utilizador { get; set; }
    }
}
