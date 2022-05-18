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
            if (f(A) * f(B) > 0)
                throw new Exception("there is no or more than 1 root");
        }

        public float DychotomyMethod()
        {
            int i = 1;
            float a = A;
            float b = B;
            Console.WriteLine("DychotomyMethod");
            Console.WriteLine("A: {0} B: {1} X: {2} I: 0", a, b, (a + b) / 2);
            while (Math.Abs(a - b) > eps)
            {
                if (Math.Sign(f(a)) == Math.Sign(f((a + b) / 2))) 
                    a = (a + b) / 2; 
                if (Math.Sign(f(b)) == Math.Sign(f((a + b) / 2)))
                    b = (a + b) / 2;
                Console.WriteLine("A: {0} B: {1} X: {2} I: {3}", a, b, (a + b) / 2,i);
                i++;
            }
            return (a + b) / 2;
        }

        public float HordMethod()
        {
            int i = 1;
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
            Console.WriteLine("HordMethod");
            Console.WriteLine("X[0]: {0} X[1]: {1} Border: {2}", x1, x2, StaticEnd);
            while (Math.Abs(x2 - x1) > eps)
            {
                x1 = x2;
                x2 = (x1 - ((f(x1) * (StaticEnd - x1)) / (f(StaticEnd) - f(x1))));
                Console.WriteLine("X[{3}]: {0} X[{4}]: {1} Border: {2}", x1, x2, StaticEnd,i,i+1);
                i++;
            }

            return x2;
        }
        public float NewtonMethod()
        {
            float StaticEnd;
            float x1;
            if (f(A) * this.derivate2(A) > 0)
            {
                StaticEnd = A;
                x1 = A;
            }
            else
            {
                StaticEnd = B;
                x1 = B;
            }
            float x2 = StaticEnd-((f(StaticEnd))/(this.derivate1(StaticEnd)));
            Console.WriteLine("NewtonMethod");
            Console.WriteLine("X[i]: {0} X[i+1]: {1} Border: {2}", x1, x2, StaticEnd);
            while (Math.Abs(x2 - x1) > eps)
            {
                x1 = x2;
                x2 = x1 - ((f(x1)) / (this.derivate1(x1)));
                Console.WriteLine("X[i]: {0} X[i+1]: {1} Border: {2}", x1, x2, StaticEnd);
            }

            return x2;
        }

        public float IterationMethod()
        {
            float k;
            if (f(A) * this.derivate2(A) > 0)
                k = this.derivate1(A);
            else
                k = this.derivate1(B);
            float x1 = A;
            float x2 = x1 - f(x1) / k;
            Console.WriteLine("IterationMethod");
            Console.WriteLine("X[i]: {0} X[i+1]: {1} K: {2}", x1, x2, k);
            while (Math.Abs(x2 - x1) > eps)
            {
                x1 = x2;
                x2 = x1 - f(x1) / k;
                Console.WriteLine("X[i]: {0} X[i+1]: {1} K: {2}", x1, x2, k);
            }

            return x2;
        }
    }

}
