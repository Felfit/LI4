using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class CookieAuth
    {
        public int Cookie { get; set; }
        public int Utilizadorid { get; set; }

        public Utilizador Utilizador { get; set; }
    }
}
