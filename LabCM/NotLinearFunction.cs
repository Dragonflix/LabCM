using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math = System.Math;

namespace LabCM
{
    internal class NotLinearFunction: Function
    {
        private float A;
        private float B;
        private float eps;

        public NotLinearFunction(float A, float B, float eps, Func<float, float> f) : base(f)
        {
            this.A = A;
            this.B = B;
            this.eps = eps;
            if (Math.Abs(A - B) < eps)
                throw new Exception("(A - B) < eps"); 
            if (Math.Sign(this.derivate1(A)) == Math.Sign(this.derivate1(B)))
                throw new Exception("sign(f(A))==sign(f(B))");
        }

        public float DychotomyMethod()
        {
            float a = A;
            float b = B;
            while (Math.Abs(a - b) > eps)
            {
                if (Math.Sign(f(a)) == Math.Sign(f((a + b) / 2))) 
                    a = (a + b) / 2; 
                if (Math.Sign(f(b)) == Math.Sign(f((a + b) / 2)))
                    b = (a + b) / 2;
            }
            return (a + b) / 2;
        }

        public float HordMethod()
        {
            float StaticEnd;
            float x1;
            if (f(A) * this.derivate2(A) > 0)
            {
                StaticEnd = A;
                x1 = (B - ((f(B) * (B - A)) / (f(B) - f(A))));
            }
            else
            {
                StaticEnd = B;
                x1 = (A - ((f(A) * (B - A)) / (f(B) - f(A))));
            }
            float x2 = (x1 - ((f(x1) * (StaticEnd - x1)) / (f(StaticEnd) - f(x1)))); 
            while (Math.Abs(x2 - x1) > eps)
            {
                x1 = x2;
                x2 = (x1 - ((f(x1) * (StaticEnd - x1)) / (f(StaticEnd) - f(x1))));
            }

            return x2;
        }
    }
}
