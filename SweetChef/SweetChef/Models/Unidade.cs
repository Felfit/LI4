using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class Unidade
    {
        [Key]
        public int UnidadeId { set; get; }
        [Required]
        public string Nome { set; get; }
    }
}
