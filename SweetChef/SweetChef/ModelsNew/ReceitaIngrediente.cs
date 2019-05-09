using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class ReceitaIngrediente
    {
        public int Quantidade { get; set; }
        public int Receitaid { get; set; }
        public int Ingredienteid { get; set; }

        public Ingrediente Ingrediente { get; set; }
        public Receita Receita { get; set; }
    }
}
