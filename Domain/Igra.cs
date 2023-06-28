using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Igra
    {
        public int xDimension { get; set; }
        public int yDimension { get; set; }
        public string Vrijednost { get; set; }
    }
}
