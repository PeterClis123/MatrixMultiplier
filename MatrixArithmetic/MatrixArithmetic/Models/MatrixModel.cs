namespace MatrixArithmetic.Models
{
    public class MatrixModel
    {
        private double[,] MatrixValues { get; set; }
        public int Rows { get { return MatrixValues.GetLength(0); } }
        public int Columns { get { return MatrixValues.GetLength(1); } }

        public double this[int row, int col]
        {
            get
            {
                return MatrixValues[row, col];
            }
            set
            {
                MatrixValues[row, col] = value;
            }
        }

        public MatrixModel(int rows, int columns)
        {
            MatrixValues = new double[rows, columns];
        }

        public MatrixModel(int m) : this(m, m) { }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public MatrixModel SubMatrix(int rowFrom, int rowTo, int colFrom, int colTo)
        {
            MatrixModel result = new MatrixModel(rowTo - rowFrom, colTo - colFrom);
            for (int row = rowFrom, i = 0; row < rowTo; row++, i++)
                for (int col = colFrom, j = 0; col < colTo; col++, j++)
                    result[i, j] = MatrixValues[row, col];
            return result;
        }
    }
}
