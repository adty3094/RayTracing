using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Matrices
    {
        private double[,] matrix;
        public double[,] Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
        private int row;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        private int col;

        public int Col
        {
            get { return col; }
            set { col = value; }
        }

        private double[,] invers;

        public double[,] Invers
        {
            get { return invers; }
            set { invers = value; }
        }

        private bool isInvertable;
        
        public Matrices(string command, double x, double y, double z)
        {
            if (command.Equals("translate"))
            {
                this.matrix = new double[4, 4]
                    { {1.0 ,0   ,0   ,x}, 
                      {0   ,1.0 ,0   ,y},
                      {0   ,0   ,1.0 ,z},
                      {0   ,0   ,0   ,1.0} };
                row = col = 4;
            }
            else if (command.Equals("scale"))
            {
                Console.WriteLine("dalam scale");
                this.matrix = new double[4, 4]
                    { {x ,0 ,0 ,0  }, 
                      {0 ,y ,0 ,0  },
                      {0 ,0 ,z ,0  },
                      {0 ,0 ,0 ,1.0} };
                row = col = 4;
            }
            this.isInvertable = true;
        }

        public Matrices(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.matrix = new double[row, col];
            if(row == col)
            {
                for(int i = 0 ; i < row ; i++)
                {
                    this.Matrix[i, i] = 1.0;
                }
            }
            if (col == row) this.isInvertable = true;
            else this.isInvertable = false;
        }

        public Matrices(int x, int y, int z, double angle)
        {
            double cos = Math.Cos(Func.DegreeToRadian(angle));
            double sin = Math.Sin(Func.DegreeToRadian(angle));
            if (x == 1)
            {
                this.matrix = new double[4, 4]
                    { {1.0  ,0    ,0    ,0  }, 
                      {0    ,cos  ,-sin ,0  },
                      {0    ,sin  ,cos  ,0  },
                      {0    ,0    ,0    ,1.0} };
                row = col = 4;
            }
            else if (y == 1)
            {
                this.matrix = new double[4, 4]
                    { {cos  ,0    ,sin  ,0  }, 
                      {0    ,1.0  ,0    ,0  },
                      {-sin ,0    ,cos  ,0  },
                      {0    ,0    ,0    ,1.0} };
                row = col = 4;
            }
            else if (z == 1)
            {
                this.matrix = new double[4, 4]
                    { {cos  ,-sin ,0    ,0  }, 
                      {sin  ,cos  ,0    ,0  },
                      {0    ,0    ,1.0  ,0  },
                      {0    ,0    ,0    ,1.0} };
                row = col = 4;
            }
            this.isInvertable = true;
        }

        public Matrices(double a, double b, double c, double d)
        {
            this.matrix = new double[4, 1]
                { {a},
                  {b},
                  {c},
                  {d}};
            this.row = 4;
            this.col = 1;
            this.isInvertable = false;
        }

        public static double[,] InversCalculate(double[,] matrixTemp)
        {
            int n = matrixTemp.GetLength(0);
            double[,] invers = new double[n,n];
            
            double scale;
            for (int i = 0; i < n; i++)
                invers[i, i] = 1.0;

            for (int r0 = 0; r0 < n; r0++) 
            {
                if (Math.Abs(matrixTemp[r0, r0]) <= 2.0 * double.Epsilon)
                {
                    for(int r1 = r0 + 1 ; r1 < n ; r1++)
                    {
                        if(Math.Abs(matrixTemp[r1,r0]) <= 2.0 * double.Epsilon)
                        {
                            RowSwap(matrixTemp, n, r0, r1);
                            RowSwap(invers, n, r0, r1);
                            break;
                        }
                    }
                }
                scale = 1.0 / matrixTemp[r0, r0];
                RowScale(matrixTemp, n, scale, r0);
                RowScale(invers, n, scale, r0);

                for (int r1 = 0; r1 < n; r1++)
                {
                    if (r1 != r0)
                    {
                        scale = -matrixTemp[r1, r0];
                        RowScaleAdd(matrixTemp, n, scale, r0, r1);
                        RowScaleAdd(invers, n, scale, r0, r1);
                    }
                }
            }
            return invers;
        }

        private static void RowScale(double[,] matrixTemp, int n, double a, int r)
        {
            for (int i = 0; i < n; ++i)
            {
                matrixTemp[r, i] *= a;
            }
        }

        private static void RowScaleAdd(double[,] matrixTemp, int n, double a, int r0, int r1)
        {
            for (int i = 0; i < n; ++i)
            {
                matrixTemp[r1, i] += a * matrixTemp[r0, i];
            }
        }

        private static void RowSwap(double[,] matrixTemp, int n, int r0, int r1) 
        {
            double tmp;
            for(int a = 0 ; a < n ; a++)
            {
                tmp = matrixTemp[r0, a];
                matrixTemp[r0, a] = matrixTemp[r1, a];
                matrixTemp[r1, a] = tmp;
            }
        }

        public static Matrices operator*(double[,] matrix1, Matrices matrix2)
        {
            Matrices hasil = new Matrices(matrix1.GetLength(0), matrix2.Col);
            for (int row = 0; row < hasil.Row; row++)
            {
                for (int col = 0; col < hasil.Col; col++)
                {
                    double temp = 0.0;
                    for (int count = 0; count < matrix2.Row; count++)
                    {
                        temp += matrix1[row, count] * matrix2.Matrix[count, col];
                    }
                    hasil.Matrix[row, col] = temp;
                }
            }

            return hasil;
        }

        public static Matrices operator*(Matrices matrix1, Matrices matrix2)
        {
            Matrices hasil = new Matrices(matrix1.Row, matrix2.Col);
           
            for(int row = 0 ; row < hasil.Row ; row++)
            {
                for(int col = 0 ; col < hasil.col ; col++)
                {
                    double temp = new double();
                    for(int count = 0 ; count < matrix1.Col ; count++)
                    {
                        temp += matrix1.Matrix[row, count] * matrix2.Matrix[count, col];
                    }
                    hasil.Matrix[row, col] = temp;
                }
            }

            return hasil;
        }

        public static Matrices operator/(Matrices matrix1 ,double n)
        {
            Matrices hasil = new Matrices(matrix1.Row, matrix1.Col);

            for (int row = 0; row < hasil.Row; row++)
            {
                for (int col = 0; col < hasil.col; col++)
                {
                    hasil.Matrix[row, col] = matrix1.Matrix[row, col] / n;
                }
            }

            return hasil;
        }

        public static Matrices operator*(Matrices matrix1, double n)
        {
            Matrices hasil = new Matrices(matrix1.Row, matrix1.Col);

            for (int row = 0; row < hasil.Row; row++)
            {
                for (int col = 0; col < hasil.col; col++)
                {
                    hasil.Matrix[row, col] = matrix1.Matrix[row, col] * n;
                }
            }

            return hasil;
        }

        public static Matrices operator+(Matrices matrix1,Matrices matrix2)
        {
            int n = matrix1.Row;
            Matrices hasil = new Matrices(n, n);
            for(int i = 0 ; i < n ; i++)
            {
                for(int j = 0 ; j < n ; j++)
                {
                    hasil.Matrix[i, j] = matrix1.Matrix[i, j] + matrix2.Matrix[i, j];
                }
            }
            return hasil;
        }

        public static Matrices operator-(Matrices matrix1, Matrices matrix2)
        {
            int n = matrix1.Row;
            Matrices hasil = new Matrices(n, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    hasil.Matrix[i, j] = matrix1.Matrix[i, j] - matrix2.Matrix[i, j];
                }
            }
            return hasil;
        }
    }
}
