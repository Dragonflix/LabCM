using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCM
{
    internal class TableFunction
    {
        List<double> X;
        List<double> Y;
        public TableFunction(List<double> A, List<double> B)
        {
            X = A;
            Y = B;
        }

        public void LagrangePolynome(double x0)
        {
            double sum1 = 1;
            double sum2 = 0;
            for(int i = 0; i < X.Count; i++)
            {
                sum1 *= x0 - X[i];
            }
            for(int i = 0; i < Y.Count; i++)
            {
                double sum3 = 1;    
                for(int j = 0; j < X.Count; j++)
                {
                    if (i == j)
                        sum3*= x0 - X[j];
                    else
                        sum3*= X[i] - X[j];
                }
                sum2 += Y[i] / sum3;
                Console.WriteLine(sum1*sum2);
            }
        }
        public void NewtonPolynome(double x0)
        {
            double h = this.X[0] - this.X[1];
            Matrix tabl = new Matrix(new double[this.X.Count, Y.Count]);
            for (int i = 0; i < X.Count; i++)
            {
                tabl[i,0] = Y[i];
            }

            for (int i = 1; i < X.Count; i++)
            {
                for (int n = 0; n < X.Count - i; n++)
                {
                    tabl[n,i] = (tabl[n + 1,i - 1] - tabl[n,i - 1]) / (X[n + i] - X[n]);
                }
            }

            double result = 0;
            for (int i = 0; i < X.Count; i++)
            {
                if (i == 0)
                    result += tabl[0,0];
                else
                {
                    double sum = 1;
                    for (int j = 0; j < i; j++)
                    {
                        sum *= x0 - X[j];
                    }
                    result += tabl[0,i] * sum;
                }
            }
            Console.WriteLine(tabl);
            Console.WriteLine(result);
        }
        public void Approximation(int m)
        {
            SLAR system = new SLAR(new double[m + 1, m + 2]);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j == 0 && i == 0)
                    {
                        system[i,j] = X.Count;
                    }
                    else
                    {
                        system[i,j] = 0;
                        for (int k = 0; k < X.Count; k++)
                        {
                            system[i,j] += Math.Pow(X[k], (i + j));
                        }
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                system[i, m] = 0;
                for (int j = 0; j < X.Count; j++)
                {
                    system[i, m] += Y[j] * Math.Pow(X[j], i);
                }
            }
            Console.WriteLine(system);
            system.GaussMethod();  
        }
    }
}
