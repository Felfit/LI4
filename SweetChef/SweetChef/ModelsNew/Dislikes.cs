using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Dislikes
    {
        public int Utilizadorid { get; set; }
        public int Tagid { get; set; }
        [JsonIgnore]
        public Tag Tag { get; set; }
        [JsonIgnore]
        public Utilizador Utilizador { get; set; }
    }
}
