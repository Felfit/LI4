using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class EmentaSemanal
    {
        public DateTime Data { get; set; }
        public int Receitaid { get; set; }
        public int Utilizadorid { get; set; }

        public Receita Receita { get; set; }
        public Utilizador Utilizador { get; set; }
    }
}
