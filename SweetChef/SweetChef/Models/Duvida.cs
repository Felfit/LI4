using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class Duvida
    {
        public int DuvidaId { get; set; }
        public string Titulo { get; set; }
        public string VideoLink { get; set; }
        public string ImageLink { get; set; }
        public string LinkExterno { get; set; }
        public string Explicacao { get; set; }
    }
}
