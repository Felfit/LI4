﻿using System;
using System.Collections.Generic;

namespace SweetChef.ModelsNew
{
    public partial class UtensilioReceita
    {
        public int Receitaid { get; set; }
        public int Utensilioid { get; set; }

        public Receita Receita { get; set; }
        public Utensilio Utensilio { get; set; }
    }
}