using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class EuroScales : IScales
    {
        private float _currentWeight;
        public EuroScales(float currentWeight) { _currentWeight = currentWeight; }

        public float GetWeight () { return _currentWeight; }
    }
}
