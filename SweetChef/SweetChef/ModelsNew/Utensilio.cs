using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Utensilio
    {
        public Utensilio()
        {
            UtensilioPasso = new HashSet<UtensilioPasso>();
            UtensilioReceita = new HashSet<UtensilioReceita>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImageLink { get; set; }
        [JsonIgnore]
        public ICollection<UtensilioPasso> UtensilioPasso { get; set; }
        [JsonIgnore]
        public ICollection<UtensilioReceita> UtensilioReceita { get; set; }
    }
}
