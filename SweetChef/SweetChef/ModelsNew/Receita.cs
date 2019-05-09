﻿using System;
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
        public string Nutricao { get; set; }
        public string ImagemLink { get; set; }
        public string VideoLink { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public int Dificuldade { get; set; }
        public int Porcoes { get; set; }
        public DateTime Tempodepreparacao { get; set; }
        public DateTime Tempodeespera { get; set; }

        public ICollection<EmentaSemanal> EmentaSemanal { get; set; }
        public ICollection<Execucao> Execucao { get; set; }
        public ICollection<Opiniao> Opiniao { get; set; }
        public ICollection<Passo> Passo { get; set; }
        public ICollection<ReceitaIngrediente> ReceitaIngrediente { get; set; }
        public ICollection<TagReceita> TagReceita { get; set; }
        public ICollection<UtensilioReceita> UtensilioReceita { get; set; }
    }
}