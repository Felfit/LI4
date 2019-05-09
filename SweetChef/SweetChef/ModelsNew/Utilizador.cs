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
        public string Password { get; set; }
        public DateTime DataNascimento { get; set; }

        public ICollection<Dislikes> Dislikes { get; set; }
        public ICollection<EmentaSemanal> EmentaSemanal { get; set; }
        public ICollection<Execucao> Execucao { get; set; }
        public ICollection<Likes> Likes { get; set; }
        public ICollection<Opiniao> Opiniao { get; set; }
        public ICollection<RestricoesAlimentares> RestricoesAlimentares { get; set; }
        public ICollection<UtilizadorPasso> UtilizadorPasso { get; set; }
    }
}
