using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class PassoIngrediente
    {
        public int Quantidade { set; get; }
        [Key]
        public int PassoId { set; get; }
        public Passo Passo { set; get; }
        //[Key]
        //public int PassoReceitaId { set; get; }
        [Key]
        public int IngredienteId { set; get; }
        public Ingrediente Ingrediente { set; get; }
    }
}
