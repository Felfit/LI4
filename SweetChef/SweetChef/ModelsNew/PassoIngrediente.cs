using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class PassoIngrediente
    {
        public int Quantidade { get; set; }
        public int Passoid { get; set; }
        public int PassoReceitaid { get; set; }
        public int Ingredienteid { get; set; }

        public Ingrediente Ingrediente { get; set; }
        public Passo Passo { get; set; }
    }
}
