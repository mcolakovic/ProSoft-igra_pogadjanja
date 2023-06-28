using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Matrica
    {
        public string Naziv { get; set; }
        public int xDimension { get; set; }
        public int yDimension { get; set; }
        public int BrojPokusaja { get; set; }
        public Matrica Self { get { return this; } }
    }
}
