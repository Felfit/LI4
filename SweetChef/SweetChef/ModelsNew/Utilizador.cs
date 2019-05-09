using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Utilizador
    {
        public Utilizador()
        {
            Dislikes = new HashSet<Dislikes>();
            EmentaSemanal = new HashSet<EmentaSemanal>();
            Execucao = new HashSet<Execucao>();
            Likes = new HashSet<Likes>();
            Opiniao = new HashSet<Opiniao>();
            RestricoesAlimentares = new HashSet<RestricoesAlimentares>();
            UtilizadorPasso = new HashSet<UtilizadorPasso>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime DataNascimento { get; set; }

        [JsonIgnore]
        public ICollection<Dislikes> Dislikes { get; set; }
        [JsonIgnore]
        public ICollection<EmentaSemanal> EmentaSemanal { get; set; }
        [JsonIgnore]
        public ICollection<Execucao> Execucao { get; set; }
        [JsonIgnore]
        public ICollection<Likes> Likes { get; set; }
        [JsonIgnore]
        public ICollection<Opiniao> Opiniao { get; set; }
        [JsonIgnore]
        public ICollection<RestricoesAlimentares> RestricoesAlimentares { get; set; }
        [JsonIgnore]
        public ICollection<UtilizadorPasso> UtilizadorPasso { get; set; }
    }
}
