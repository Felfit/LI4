using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Receita
    {
        public Receita()
        {
            EmentaSemanal = new HashSet<EmentaSemanal>();
            Execucao = new HashSet<Execucao>();
            Opiniao = new HashSet<Opiniao>();
            Passo = new HashSet<Passo>();
            ReceitaIngrediente = new HashSet<ReceitaIngrediente>();
            TagReceita = new HashSet<TagReceita>();
            UtensilioReceita = new HashSet<UtensilioReceita>();
        }

        public int Id { get; set; }
        public string ImagemLink { get; set; }
        public string VideoLink { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public int Dificuldade { get; set; }
        public int Porcoes { get; set; }
        public int Tempodepreparacao { get; set; }
        public int Tempodeespera { get; set; }
        public int Energia { get; set; }
        public int Gordura { get; set; }
        public int HidratosCarbono { get; set; }

        [JsonIgnore]
        public ICollection<EmentaSemanal> EmentaSemanal { get; set; }
        [JsonIgnore]
        public ICollection<Execucao> Execucao { get; set; }
        [JsonIgnore]
        public ICollection<Opiniao> Opiniao { get; set; }
        [JsonIgnore]
        public ICollection<Passo> Passo { get; set; }
        [JsonIgnore]
        public ICollection<ReceitaIngrediente> ReceitaIngrediente { get; set; }
        [JsonIgnore]
        public ICollection<TagReceita> TagReceita { get; set; }
        [JsonIgnore]
        public ICollection<UtensilioReceita> UtensilioReceita { get; set; }

        public override bool Equals(object obj)
        {
            var receita = obj as Receita;
            return receita != null &&
                   Id == receita.Id;
        }
    } 
}
