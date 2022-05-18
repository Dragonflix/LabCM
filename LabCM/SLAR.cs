using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCM
{
    internal class SLAR: Matrix
    {
        public SLAR(double[,] matrix):base(matrix)
        {
            
        }

        public double[] CramerMethod()
        {
            Console.WriteLine("Cramer Method");
            double DetA = this.DeleteColumn(this.m-1).Determinant();
            Console.WriteLine("Determinant: " + DetA);
            double[] result = new double[this.n];
            for (int i = 0; i < this.n; i++)
            {
                Matrix tmp = this.DeleteColumn(this.m-1);               
                for (int j = 0; j < this.n; j++)
                {
                    tmp[j, i] = this[j, this.m - 1];
                }
                result[i] = tmp.Determinant() / DetA;
                Console.WriteLine("A{0}\n"+tmp,i);
                Console.WriteLine(tmp.Determinant());
            }
            return result;
        }

        public Matrix MatrixMethod()
        {
            Console.WriteLine("Matrix Method");
            SLAR B = new SLAR(this._matrix);
            for (int i = 0; i < this.m-1; i++)
            {
                B = B.DeleteColumn(0);
            }
            SLAR A = this.Reversed();
            Console.WriteLine("A^-1:\n"+A);
            Console.WriteLine("B:\n"+B);
            Matrix X = A * B;
            return X;
        }

        public Matrix GaussMethod()
        {
            Console.WriteLine("Gauss Method");
            int count1 = 0, count2 = 0;
            double[,] triangle = new double[this.n, this.m];
            List<Matrix> tempres = GaussHelper(this);
            Console.WriteLine("A[0]:\n" + tempres[0]);
            Console.WriteLine("MainLine:\n" + tempres[1]);
            for (int i = 0; i < tempres[1].m; i++)
            {
                triangle[count1, i] = tempres[1][0, i];
            }

            count1++;

            while (tempres[1].m > 2)
            {
                tempres = GaussHelper(tempres[0]);
                for (int i = 0; i < tempres[1].m; i++)
                {
                    triangle[count1, i] = tempres[1][0, i];
                }
                Console.WriteLine("A[{0}]:\n" + tempres[0], count1);
                Console.WriteLine("MainLine:\n" + tempres[1]);
                count1++;
            }

            double[,] result = new double[this.n, 1];
            result[this.n - 1, 0] = triangle[this.n - 1, 1] / triangle[this.n - 1, 0];
            for (int i = this.n - 2; i >= 0; i--)
            {
                double temp = 0;
                for (int j = 1; j < this.m - i - 1; j++)
                {
                    temp += triangle[i, j] * result[i + j, 0];
                }

                temp = triangle[i, this.m - i - 1] - temp;
                result[i, 0] = temp / triangle[i, 0];
            }
            double[,] triangle1 = new double[this.n, this.m];
            List<double> t = new List<double>();
            foreach(var i in triangle)
            {
                if(i!=0)
                    t.Add(i);
            }
            int cnt = 0;
            for (int i = 0; i < this.n; i++)
            {
                int k = 0;
                for (int j = 0; j < this.m; j++)
                {
                    if (k < i)
                    {
                        triangle1[i, j] = 0;
                        k++;
                    }
                    else
                    {
                        triangle1[i, j] = t[cnt];
                        cnt++;
                    }

                }
            }
        
            Console.WriteLine(new Matrix(triangle1));
            return new Matrix(result);
        }

        public List<Matrix> GaussHelper(Matrix matr)
        {
            List<Matrix> result = new List<Matrix>();
            int p = 0, k = 0;
            double max = Math.Abs(matr[0, 0]);
            for (int i = 0; i < matr.n; i++)
            {
                if (Math.Abs(matr[i, 0]) > max)
                {
                    max = Math.Abs(matr[i, 0]);
                    p = i;
                    k = 0;
                }
            }

            double[] multipliers = new double[matr.n];

            for (int i = 0; i < matr.n; i++)
            {
                if (i != p)
                {
                    multipliers[i] = -1 * matr[i, k] / matr[p, k];
                }
            }


            Matrix result1 = new Matrix(new double[matr.n,matr.m]);

            for (int i = 0; i < matr.n; i++)
            {
                for (int j = 0; j < matr.m; j++)
                {
                    result1[i, j] = multipliers[i] * matr[p, j];
                }
            }

            Matrix result2 = matr + result1;
            Matrix result3 = new Matrix(new double[matr.n - 1, matr.m - 1]);
            int i1 = 0, j1 = 0;

            for (int i = 0; i < matr.n; i++)
            {
                if (i != p)
                {
                    for (int j = 0; j < matr.m; j++)
                    {
                        if (j != k)
                        {
                            result3[i1, j1] = result2[i, j]; 
                            j1++;
                        }
                    }

                    j1 = 0;
                    i1++;
                }
            }

            double[,] result4 = new double[1,matr.m];
            for (int i = 0; i < matr.m; i++)
            {
                result4[0,i] = matr[p, i];
            }

            result.Add(result3);
            result.Add(new Matrix(result4));
            return result;
        }

        public Matrix YakobiMethod(float eps)
        {
            Console.WriteLine("Yakobi Method");
            double sum = 1;
            int n = 0;
            double[,] B = new double[this.n,1];
            double[,] A = new double[this.n, this.n];

            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    A[i, j] = -1 * this[i, j] / this[i, i];
                    if (i == j)
                        A[i, j] = 0;
                }
            }

            for (int i = 0; i < this.n; i++)
            {
                B[i,0] = this[i, this.m - 1] / this[i, i];
            }

            double sum1 = 0;

            for (int i = 0; i < this.n; i++)
            {
                for(int j = 0; j < this.n; j++)
                {
                    sum1 += Math.Abs(A[i, j] / A[i, i]);
                }
                if(sum1>=1)
                {
                    Console.WriteLine("Matrixes arent convergent");
                    return new Matrix(B);
                }
                sum1 = 0;
            }

            for (int j = 0; j < this.n; j++)
            {
                for (int i = 0; i < this.n; i++)
                {
                    sum1 += Math.Abs(A[i, j] / A[i, i]);
                }
                if (sum1 >= 1)
                {
                    Console.WriteLine("Matrixes arent convergent");
                    return new Matrix(B);
                }
                sum1 = 0;
            }

            Matrix X1 = new Matrix(B);
            Matrix X2 = new Matrix(B);
            while (sum>eps)
            {
                Console.WriteLine("X[{0}]:\n"+X1, n);
                sum = 0;
                
                X2 = new Matrix(B) + new Matrix(A) * X1;
                for (int i = 0; i < this.n; i++)
                {
                    sum += Math.Pow(X2[i, 0] - X1[i, 0], 2);
                }
                sum = Math.Sqrt(sum);
                X1 = X2;
                n++;
            }
            Console.WriteLine("X[{0}]:\n" + X1, n);
            return (X1);
        }

        public Matrix ZeidelMethod(float eps)
        {
            Console.WriteLine("Zeidel Method");
            double sum = 1;
            int n = 0;
            double[,] B = new double[this.n, 1];

            for (int i = 0; i < this.n; i++)
            {
                B[i, 0] = this[i, this.m - 1] / this[i, i];
            }

            double[,] A = new double[this.n, this.n];
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    A[i, j] = -1 * this[i, j] / this[i, i];
                    if (i == j)
                        A[i, j] = 0;
                }
            }

            Matrix X1 = new Matrix(new double[this.n, 1]);
            Matrix X2 = new Matrix(new double[this.n, 1]);
            double summ = 0;
            for (int j = 0; j < this.n; j++)
            {
                for (int k = 0; k < j; k++)
                {
                    summ += X2[k, 0] * A[j, k];
                }
                X2[j, 0] = B[j, 0] + summ;
                summ = 0;
            }
            X1 = (Matrix)X2.Clone();
            while (sum>eps)
            {
                Console.WriteLine("X[{0}]:\n" + X1, n);
                sum = 0;
                
                for (int i = 0; i < this.n; i++)    
                {
                    double sum1 = 0;
                    double sum2 = 0;
                        
                        for (int j = 0; j < i; j++)
                        {
                            sum1 += A[i, j] * X2[j, 0];
                        }
                        for (int j = i; j < this.n; j++)
                        {
                            sum2 += A[i, j] * X1[j, 0];
                        }
                        X2[i,0] = B[i, 0] + sum1 + sum2;
                }

                for (int i = 0; i < this.n; i++)
                {
                    sum += Math.Pow(X2[i, 0] - X1[i, 0], 2);
                }
                sum = Math.Sqrt(sum);
                X1 = (Matrix)X2.Clone();
                n++;
            }
            Console.WriteLine("X[{0}]:\n" + X1, n);
            return (X1);
        }

        public Matrix SquareRootMethod(Matrix U)
        {
            Matrix result = new Matrix(new double[this.n,1]);
            double[,] L = new double[this.n,this.n];
            L[0, 0] = Math.Sqrt(this[0, 0]);

            for (int i = 1; i < this.n; i++)
            {
                L[i, 0] = this[i,0]/L[0,0];
            }
            for(int i = 1; i < this.n; i++)
            {
                for(int j = 1;j<i;j++)
                {
                    double sum = 0;
                    for(int k = 1; k < this.n-1; k++)
                    {
                        sum += L[k, 0] * L[k+1, 0];
                    }
                    L[i,j] = (this[i,j] - sum) / L[i-1,i-1];
                }
                double sum1 = 0;
                for(int p=0;p<i;p++)
                {
                    sum1+=Math.Pow(L[i,p],2);
                }
                L[i,i] = Math.Sqrt(this[i,i]-sum1);
            }
            Console.WriteLine("L:\n" + new Matrix(L));
            Console.WriteLine("Lt:\n" + new SLAR(L).Transpose());;
            Matrix a = new SLAR(L)*new SLAR(L).Transpose();
            SLAR A = new SLAR(new double[this.n,this.n+1]);
            for (int i = 0; i < this.n; i++)
            {
                for(int j = 0; j < this.n+1; j++)
                {
                    if (j == this.n)
                    {
                        A[i, j] = U[i,0];
                    }
                    else
                    {
                        A[i, j] = a[i, j];
                    }
                }
            }
            
            Matrix X = A.Reversed() * U *-1;
            return X;
        }

        public Matrix QuadratMethod()
        {
            SLAR B = new SLAR(this._matrix);
            for (int i = 0; i < this.m - 1; i++)
            {
                B = B.DeleteColumn(0);
            }
            
            SLAR A = new SLAR((this.DeleteColumn(this.m-1).Transpose() * this.DeleteColumn(this.m - 1))._matrix);
            Matrix U = this.DeleteColumn(this.m - 1).Transpose() * B;
            Console.WriteLine("Norm:\n"+A);
            Console.WriteLine("NormB:\n" + U);
            return A.SquareRootMethod(U);
        }

        public SLAR DeleteColumn(int column)
        {
            List<double> temp = new List<double>();
            double[,] result = new double[this.n, this.m - 1];
            int amount = 0;
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    if (j != column)
                        temp.Add(this[i, j]);
                }
            }
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m - 1; j++)
                {
                    result[i, j] = temp[amount];
                    amount++;
                }
            }
            return new SLAR(result);
        }

        public SLAR Transpose()
        {
            double[,] result = new double[this.m, this.n];
            for (int i = 0; i < this.m; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    result[i, j] = this[j, i];
                }
            }
            return new SLAR(result);
        }

        public SLAR LU()
        {
            Console.WriteLine("LU Method");
            double[,] L = new double[this.n, this.n];
            double[,] U = new double[this.n, this.n];
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    U[0, j] = this[0, j] / this[0,0];
                    L[i, 0] = this[i, 0];
                    double sum = 0;
                    for (int k = 0; k < i; k++)
                    {
                        sum += L[i, k] * U[k, j];
                    }
                    L[i, j] = this[i, j] - sum;
                    if (j > i)
                    {
                        L[i, j] = 0;    
                    }
                    
                    sum = 0;
                    for (int k = 0; k < i; k++)
                    {
                        sum += L[i, k] * U[k, j];
                    }   
                    U[i, j] = (this[i, j] - sum) / L[i, i];
                    if (i > j)
                    {
                        U[i, j] = 0;
                    }
                    U[i, i] = 1;
                }
            }
            Console.WriteLine("L:\n"+new SLAR(L));
            Console.WriteLine("U:\n" + new SLAR(U));
            double[] B = new double[this.n];
            
            for (int i = 0; i < this.n; i++)
            {
                B[i] = this[i, this.m-1];
            }

            double[] Y = new double[this.n];
            Y[0] = B[0] / L[0, 0];
            for (int i = 1; i < this.n; i++)
            {
                double sum=0;
                for (int j = 0; j < i; j++)
                {
                    sum +=L[i,j] * Y[j];
                }
                Y[i] = (1/L[i,i])*(B[i]-sum);
            }

            SLAR X = new SLAR(new double[this.n, 1]);
            X[this.n-1,0] = Y[this.n-1];
            for (int i = this.n-2; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i+1; j < this.n; j++)
                {
                    sum += U[i, j] * X[j,0];
                }
                X[i,0] = Y[i]-sum;
            }
            return X;
        }

        public SLAR Reversed()
        {
            double[,] result = new double[this.n, this.m-1];
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m-1; j++)
                {
                    result[i, j] =-1 * Math.Pow(-1, j+i) * Math.Abs(1/this.DeleteColumn(this.m-1).Determinant()) * this.DeleteColumn(this.m - 1).Transpose().Minor(i,j).Determinant();
                }
            }
            return new SLAR(result);
        }

    }
}

