using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class UtensilioReceita
    {
        public int Receitaid { get; set; }
        public int Utensilioid { get; set; }
        [JsonIgnore]
        public Receita Receita { get; set; }
        [JsonIgnore]
        public Utensilio Utensilio { get; set; }
    }
}
