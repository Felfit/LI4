using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class TagReceita
    {
        public int Receitaid { get; set; }
        public int Tagid { get; set; }

        public Receita Receita { get; set; }
        public Tag Tag { get; set; }
    }
}
