using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class Poruka
    {
        public bool isSuccessful { get; set; }
        public string MessageText { get; set; }
        public Object PorukaObject { get; set; }
        public Operations Operations { get; set; }
    }
}
