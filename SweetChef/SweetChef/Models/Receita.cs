using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class Receita
    {   
        [Key]
        public int Id { set; get; }
        [Required]
        public string Nutricao { set; get; }
        [Required]
        public string ImageLink { set; get; }
        [Required]
        public string Descricao { set; get; }
        [Required]
        public string Nome { set; get; }
        [Required]
        public int Dificuldade { set; get; }
        [Required]
        public int Porcoes { set; get; }
        [Required]
        public TimeSpan TempoDePreparacao { set; get; }
        [Required]
        public TimeSpan TempoDeEspera { set; get; }

        public List<ReceitaIngrediente> ReceitaIngredientes { set; get; }

        public List<Passo> Passos { set; get; }

    }
}
