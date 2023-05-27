using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class AdapterForBritishScales : IScales
    {
        BritishScales _britishScales;
        public AdapterForBritishScales(BritishScales british) { _britishScales = british; }
        public float GetWeight() => (float)(_britishScales.GetWeight() * 0.453);
    }
}
