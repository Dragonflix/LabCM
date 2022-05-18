using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabCM
{
    internal class Matrix:ICloneable
    {
        //Variables

        public double[,] _matrix;
        public int n
        { get { return _matrix.GetLength(0); } }
        public int m
        { get { return _matrix.GetLength(1); } }
        public double this[int i, int j]
        {
            get => _matrix[i, j];
            set => _matrix[i, j] = value;
        }
        public Matrix(double[,] Matrix)

        {
            _matrix = Matrix;
        }

        //Operators

        public static Matrix operator *(Matrix LVal,int RVal)
        {
            double[,] result = new double[LVal.n, LVal.m];
            for (int i = 0; i < LVal.n; i++)
            {
                for(int j = 0;j<LVal.m;j++)
                {
                    result[i,j] = LVal[i, j]*RVal;
                }
            }
            return new Matrix(result);
        }

        public static Matrix operator *(Matrix LVal, Matrix RVal)
        {         
            if (LVal.m != RVal.n)
            {
                throw new Exception("Matrixes cant be multiplied");
            }
            double[,] result = new double[LVal.n, RVal.m]; 
            for(int i=0; i<LVal.n; i++)
            {
                for(int j=0;j<RVal.m;j++)
                {
                    double tempres = 0;
                    for (int k = 0; k < LVal.m; k++)
                    {
                        tempres += LVal[i, k] * RVal[k, j];
                    }
                    result[i, j] = tempres;
                }
            }
            return new Matrix(result);
        }

        public static Matrix operator +(Matrix LVal, Matrix RVal)
        {
            if (LVal.n!=RVal.n || LVal.m != RVal.m)
            {
                throw new Exception("Matrixes havent same sizes");
            }
            double[,] result = new double[LVal.n, LVal.m];
            for (int i = 0; i < LVal.n; i++)
            {
                for (int j = 0; j < LVal.m; j++)
                {
                    result[i, j] = LVal[i, j] + RVal[i,j];
                }
            }
            return new Matrix(result);
        }

        public static Matrix operator ++(Matrix LVal)
        {
            double[,] result = new double[LVal.n, LVal.m];
            for (int i = 0; i < LVal.n; i++)
            {
                for (int j = 0; j < LVal.m; j++)
                {
                    result[i, j] = LVal[i, j]++;
                }
            }
            return new Matrix(result);
        }

        public static Matrix operator -(Matrix LVal, Matrix RVal)
        {
            double[,] result = new double[LVal.n, LVal.m];
            for (int i = 0; i < LVal.n; i++)
            {
                for (int j = 0; j < LVal.m; j++)
                {
                    result[i, j] = LVal[i, j] - RVal[i, j];
                }
            }
            return new Matrix(result);
        }
        public static Matrix operator --(Matrix LVal)
        {
            double[,] result = new double[LVal.n, LVal.m];
            for (int i = 0; i < LVal.n; i++)
            {
                for (int j = 0; j < LVal.m; j++)
                {
                    result[i, j] = LVal[i, j]--;
                }
            }
            return new Matrix(result);
        }

        //Methods

        public Matrix Minor(int n, int m)
        {
            if (this.n != this.m)
            {
                throw new Exception("Matrix isnt square");
            }
            List<double> temp = new List<double>();
            double[,] result = new double[(int)Math.Sqrt(_matrix.Length)-1, (int)Math.Sqrt(_matrix.Length)-1];
            int amount = 0;
            for (int i = 0; i < (int)Math.Sqrt(_matrix.Length); i++)
            {
                if (i != n)
                {
                    for (int j = 0; j < (int)Math.Sqrt(_matrix.Length); j++)
                    {
                        if (j != m)
                        {
                            temp.Add(_matrix[i,j]);
                        }
                    }
                }
            }
            for (int i = 0; i < (int)Math.Sqrt(_matrix.Length) - 1; i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(_matrix.Length) - 1; j++)
                {
                    result[i,j] = temp[amount];
                    amount++;
                }
            }
            return new Matrix(result);
        }
        public double Determinant()
        {
            if (this.n != this.m)
            {
                throw new Exception("Matrix isnt square");
            }
            double det = 0;       
            if ((int)Math.Sqrt(_matrix.Length) == 1)
            {
                det = _matrix[0,0];
            }
            else
            {
                for (int i = 0; i < (int)Math.Sqrt(_matrix.Length); i++)
                {
                    Matrix tmp = this.Minor(i, 0);
                    det += _matrix[i,0] * (int)Math.Pow(-1, i) * tmp.Determinant();//Math.Pow(-1, i+1)
                }
            }
            return det;         
        }

        public override string ToString()
        {
            string result="";
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.m; j++)
                {
                    result += ((float)this[i, j]).ToString() + " ";
                }
                result += '\n';
            }
            return result;
        }

        public object Clone()
        {
            double[,] matr = new double[this.n,this.m];
            for (int i = 0; i < this.n; i++)
            {
                for(int j = 0; j < this.m; j++)
                {
                    matr[i, j] = this[i, j];
                }
            }
            return new Matrix(matr);
        }
    }
}
