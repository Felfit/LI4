using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class UtilizadorPasso
    {
        public int Utilizadorid { get; set; }
        public int Passoid { get; set; }
        public int PassoReceitaid { get; set; }
        public int? Dificuldade { get; set; }
        public string Comentario { get; set; }

        public Passo Passo { get; set; }
        public Utilizador Utilizador { get; set; }
    }
}
