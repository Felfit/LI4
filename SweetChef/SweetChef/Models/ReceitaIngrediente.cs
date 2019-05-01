using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class ReceitaIngrediente
    {
        public int quantidade { set; get; }

        public int ReceitaId { set; get; }
        public Receita Receita { set; get; }

        public int IngredienteId { set; get; }
        public Ingrediente Ingrediente { set; get; }
    }
}
