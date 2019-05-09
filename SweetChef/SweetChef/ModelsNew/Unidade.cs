using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Unidade
    {
        public Unidade()
        {
            Ingrediente = new HashSet<Ingrediente>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public ICollection<Ingrediente> Ingrediente { get; set; }
    }
}
