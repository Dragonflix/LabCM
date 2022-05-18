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
        public float RectangleMethod(float a, float b, int n)
        {
            float h = (b - a) / n;
            float result = 0;
            float x = a;
            for(int i = 0; i < n; i++)
            {
                result += f(x);
                x = x + h;
            }
            return result * h;  
        }
        public float TrapeziaMethod(float a, float b, int n)
        {
            float h = (b - a) / n;
            float result = 0;
            float x = a;
            for (int i = 0; i < n-1; i++)
            {
                result += (f(x)+f(x+h))/2;
                x = x + h;
            }
            return result * h;
        }
        public float SimpsonMethod(float a, float b, int n)
        {
            float h = (b - a) / n;
            float result = f(a) + f(a + 2 * n * h);
            float sum1 = 0;
            float sum2 = 0;
            for (int i = 0; i < n; i++)
            {
                sum1 += f(a + (2 * i - 1) * h);
            }
            result += sum1 * 4;
            for (int i = 0; i < n - 1; i++)
            {
                sum1 += f(a + 2 * i * h);
            }
            result += sum2 * 2;
            return result * h/3;
        }
    }
}
