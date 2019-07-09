namespace MatrixArithmetic.Services.Implementations
{
    using MatrixArithmetic.Models;
    using MatrixArithmetic.Services.Interfaces;
    using System;
    using System.Linq;

    public class MatrixService : IMatrixService
    {
        private readonly Random random = new Random();

        public MatrixModel Populate(int m, int n, string[] values)
        {
            var a = Array.ConvertAll(values, int.Parse);
            MatrixModel matrix = new MatrixModel(m, n);
            var index = 0;
            for (int row = 0; row < matrix.Rows; row++)
            {
                for (int col = 0; col < matrix.Columns; col++)
                {
                    matrix[row, col] = a[index];
                    index++;
                }
            }
            return matrix;
        }

        public MatrixModel Generate(int m, int n, int minValue = -9, int maxValue = 9)
        {
            MatrixModel matrix = new MatrixModel(m, n);

            for (int row = 0; row < matrix.Rows; row++)
                for (int col = 0; col < matrix.Columns; col++)
                    matrix[row, col] = random.Next(minValue, maxValue + 1);

            return matrix;
        }

        public MatrixModel Generate(int m, int minValue = -9, int maxValue = 9)
        {
            return this.Generate(m, m, minValue, maxValue);
        }

        public MatrixModel Add(MatrixModel a, MatrixModel b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new ArgumentException("Not identical matrices.");

            MatrixModel result = new MatrixModel(a.Rows, a.Columns);

            for (int row = 0; row < a.Rows; row++)
                for (int col = 0; col < a.Columns; col++)
                    result[row, col] = a[row, col] + b[row, col];

            return result;
        }

        public MatrixModel Subtract(MatrixModel a, MatrixModel b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new ArgumentException("Not identical matrices.");

            MatrixModel result = new MatrixModel(a.Rows, a.Columns);

            for (int row = 0; row < a.Rows; row++)
                for (int col = 0; col < a.Columns; col++)
                    result[row, col] = a[row, col] - b[row, col];

            return result;
        }

        public MatrixModel NormalMultiply(MatrixModel a, MatrixModel b)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Number of rows of the matrix a doesnt equal to number of columns of the matrix b.");

            MatrixModel result = new MatrixModel(a.Rows, b.Columns);

            for (int row = 0; row < a.Rows; row++)
            {
                for (int col = 0; col < b.Columns; col++)
                {
                    double tmp = 0;
                    for (int i = 0; i < a.Columns; i++) // or i < b.Rows, it's equal
                        tmp += a[row, i] * b[i, col];

                    result[row, col] = tmp;
                }
            }

            return result;
        }

        public MatrixModel StrassenMultiply(MatrixModel a, MatrixModel b)
        {
            // If the matrices A, B are not of type 2n x 2n Fallback to using NormalMultiply
            var sizes = new int[] { a.Rows, a.Columns, b.Rows, b.Columns };
            if (sizes.Distinct().Count() != 1 || (a.Rows & (a.Rows - 1)) != 0)
            {
                this.NormalMultiply(a, b);
            }

            int N = b.Rows;
            if (N <= 48)
                return NormalMultiply(a, b);

            int halfN = N / 2;

            var a11 = a.SubMatrix(0, halfN, 0, halfN);
            var a12 = a.SubMatrix(0, halfN, halfN, N);
            var a21 = a.SubMatrix(halfN, N, 0, halfN);
            var a22 = a.SubMatrix(halfN, N, halfN, N);

            var b11 = b.SubMatrix(0, halfN, 0, halfN);
            var b12 = b.SubMatrix(0, halfN, halfN, N);
            var b21 = b.SubMatrix(halfN, N, 0, halfN);
            var b22 = b.SubMatrix(halfN, N, halfN, N);

            MatrixModel[] m = new MatrixModel[]{
                StrassenMultiply(this.Add(a11, a22), this.Add(b11, b22)),
                StrassenMultiply(this.Add(a21, a22), b11),
                StrassenMultiply(a11, this.Subtract(b12, b22)),
                StrassenMultiply(a22, this.Subtract(b21, b11)),
                StrassenMultiply(this.Add(a11, a12), b22),
                StrassenMultiply(this.Subtract(a21, a11), this.Add(b11, b12)),
                StrassenMultiply(this.Subtract(a12, a22), this.Add(b21, b22))
            };

            var c11 = this.Add(this.Subtract(this.Add(m[0], m[3]), m[4]), m[6]);
            var c12 = this.Add(m[2], m[4]);
            var c21 = this.Add(m[1], m[3]);
            var c22 = this.Add(this.Add(this.Subtract(m[0], m[1]), m[2]), m[5]);

            return CombineSubMatrices(c11, c12, c21, c22);
        }

        
        private static MatrixModel CombineSubMatrices(MatrixModel a11, MatrixModel a12, MatrixModel a21, MatrixModel a22)
        {
            MatrixModel result = new MatrixModel(a11.Rows * 2);
            int shift = a11.Rows;
            for (int row = 0; row < a11.Rows; row++)
                for (int col = 0; col < a11.Columns; col++)
                {
                    result[row, col] = a11[row, col];
                    result[row, col + shift] = a12[row, col];
                    result[row + shift, col] = a21[row, col];
                    result[row + shift, col + shift] = a22[row, col];
                }
            return result;
        }
    }
}
