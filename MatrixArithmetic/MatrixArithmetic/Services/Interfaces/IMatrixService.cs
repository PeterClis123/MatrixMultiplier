namespace MatrixArithmetic.Services.Interfaces
{
    using MatrixArithmetic.Models;
    interface IMatrixService
    {
        MatrixModel Add(MatrixModel a, MatrixModel b);
        MatrixModel Generate(int m, int n, int minValue = -9, int maxValue = 9);
        MatrixModel Generate(int m, int minValue = -9, int maxValue = 9);
        MatrixModel NormalMultiply(MatrixModel a, MatrixModel b);
        MatrixModel Populate(int m, int n, string[] values);
        MatrixModel StrassenMultiply(MatrixModel a, MatrixModel b);
        MatrixModel Subtract(MatrixModel a, MatrixModel b);
    }
}
