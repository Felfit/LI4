using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Duvida
    {
        public Duvida()
        {
            PassoDúvida = new HashSet<PassoDúvida>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string VideoLink { get; set; }
        public string ImagemLink { get; set; }
        public string Linkexterno { get; set; }
        public string Explicacao { get; set; }
        [JsonIgnore]
        public ICollection<PassoDúvida> PassoDúvida { get; set; }
    }
}
