﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Matrica Matrica { get; set; }
    }
}
