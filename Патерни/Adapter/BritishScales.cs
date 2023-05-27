using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class BritishScales
    {
        private float _currentWeight;
        public BritishScales(float currentWeight) { _currentWeight = currentWeight; }

        public float GetWeight() { return _currentWeight; }
    }
}
