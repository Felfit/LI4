using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class TagReceita
    {
        public int Receitaid { get; set; }
        public int Tagid { get; set; }
        [JsonIgnore]
        public Receita Receita { get; set; }
        [JsonIgnore]
        public Tag Tag { get; set; }
    }
}
