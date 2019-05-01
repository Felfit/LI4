using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class Ingrediente
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Nome { set; get; }
        [Required]
        // ver melhor isto
        public int UnidadeId { set; get; }
        [NotMapped]
        [JsonIgnore]
        public Unidade unidade { set; get; }

        public List<ReceitaIngrediente> ReceitaIngredientes { set; get; }
        public List<PassoIngrediente> PassoIngredientes { set; get; }
    }
}
