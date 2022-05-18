using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCM
{
    internal class SNAE
    {
        private List<Func<float, float, float>> _system = new List<Func<float, float, float>>();
        public SNAE(params Func<float, float, float>[] equalations)
        {
            foreach (var equalation in equalations)
                _system.Add(equalation);
        }
        public Func<float, float, float> this[int i]
        {
            get => _system[i];
            set => _system[i] = value;
        }

        public float partY1(float x)
        {
            return 1f;
        }
        public float partY2(float x)
        {
            return -1 * (float)Math.Cos(x + 1);
        }
        public float partX1(float x)
        {
            return (float)Math.Cos(x - 1);
        }
        public float partX2(float x)
        {
            return 1f;
        }
        float fi1(float y)
        {
            return (float)Math.Sin(y + 1) + 0.8f;
        }
        float fi2(float x)  
        {
            return 1.3f - (float)Math.Sin(x - 1);
        }
        public Matrix IterationMethod(float x0, float y0, float eps)
        {
            int count = 0;
            Console.WriteLine("Iteration Method");
            float x2 = x0;
            float y2 = y0;
            Console.WriteLine($"{count}: X:{x2} Y:{y2}");
            float x1, y1;
            do
            {
                count++;
                x1 = x2;
                y1 = y2;
                x2 = fi1(y2);
                y2 = fi2(x2);
                Console.WriteLine($"{count}: X:{x2} Y:{y2}");               
            } 
            while (Math.Abs(x2 - x1) + Math.Abs(y2 - y1) > eps);
            return new Matrix(new double[,] { { x2 },{ y2 } });
        }

        public Matrix NewtonMethod(float x0, float y0, float eps)
        {
            float det = partX1(x0) * partY2(y0) - partY1(y0) * partX2(x0);
            int count = 0;
            Console.WriteLine("Newton Method");
            float x2 = x0;
            float y2 = y0;
            Console.WriteLine($"{count}: X:{x2} Y:{y2}");
            float x1, y1;
            do
            {
                count++;
                x1 = x2;
                y1 = y2;
                float detx = -(_system[0](x2, y2) * partY2(y2) - _system[1](x2, y2) * partY1(y2)) / det;
                float dety = -(_system[1](x2, y2) * partX1(x2) - _system[0](x2, y2) * partX2(y2)) / det;
                x2 = x2+detx;
                y2 = y2+dety;
                Console.WriteLine($"{count}: X:{x2} Y:{y2}");
            }
            while (Math.Abs(x2 - x1) + Math.Abs(y2 - y1) > eps);
            return new Matrix(new double[,] { { x2 }, { y2 } });
        }
    }
}
