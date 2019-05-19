using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Nutricao
    {
        public int Receitaid { get; set; }
        public int Energia { get; set; }
        public int Gordura { get; set; }
        public int HidratosCarbono { get; set; }

        public Receita Receita { get; set; }
    }
}
