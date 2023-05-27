using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    public abstract class CarAccessoriesDecorator : ICar
    {

        private ICar _car;

        public CarAccessoriesDecorator(ICar aCar)
        {
            this._car = aCar;
        }
        public virtual string GetDescription()
        {
            return this._car.GetDescription();
        }

        public virtual double GetCost()
        {
            return this._car.GetCost();
        }
    }
}
