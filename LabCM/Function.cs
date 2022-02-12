using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCM
{
    abstract class Function
    {
        protected Func<float, float> f;

        public Function(Func<float, float> f)
        {
            this.f = f;
        }
        public float derivate1(float x)
        {
            return ((f(x + 0.1f) - f(x - 0.1f)) / 0.2f);
        }
        public float derivate2(float x)
        {
            return ((f(x + 0.1f) - 2*f(x) - f(x - 0.1f)) / 0.04f);
        }
    }
}
