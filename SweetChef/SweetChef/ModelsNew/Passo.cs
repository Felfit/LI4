﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Passo
    {
        public Passo()
        {
            PassoDúvida = new HashSet<PassoDúvida>();
            PassoIngrediente = new HashSet<PassoIngrediente>();
            UtensilioPasso = new HashSet<UtensilioPasso>();
            UtilizadorPasso = new HashSet<UtilizadorPasso>();
        }

        public int Numero { get; set; }
        public int Receitaid { get; set; }
        public int Duracao { get; set; }
        public string Descricao { get; set; }
        public string ImagemLink { get; set; }
        public string VideoLink { get; set; }
        public string LinkExterno { get; set; }
        [JsonIgnore]
        public Receita Receita { get; set; }
        [JsonIgnore]
        public ICollection<PassoDúvida> PassoDúvida { get; set; }
        [JsonIgnore]
        public ICollection<PassoIngrediente> PassoIngrediente { get; set; }
        [JsonIgnore]
        public ICollection<UtensilioPasso> UtensilioPasso { get; set; }
        [JsonIgnore]
        public ICollection<UtilizadorPasso> UtilizadorPasso { get; set; }
    }
}
