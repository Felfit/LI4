using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class Likes
    {
        public int Utilizadorid { get; set; }
        public int Tagid { get; set; }

        public Tag Tag { get; set; }
        public Utilizador Utilizador { get; set; }
    }
}
