using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class PassoDúvida
    {
        public int Passoid { get; set; }
        public int PassoReceitaid { get; set; }
        public int Dúvidaid { get; set; }
        public string Questao { get; set; }
        [JsonIgnore]
        public Duvida Dúvida { get; set; }
        [JsonIgnore]
        public Passo Passo { get; set; }
    }
}
