namespace MatrixArithmetic.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SimpleMatrixMultiplierModel
    {
        [Required]
        public string CommaSeperatedValuesA { get; set; }
        [Required]
        public string CommaSeperatedValuesB { get; set; }

        [Required]
        [Range(2, 50, ErrorMessage = "Number of rows must be 2-50).")]
        public int RowsA { get; set; }
        [Required]
        [Range(2, 50, ErrorMessage = "Number of rows must be 2-50).")]
        public int RowsB { get; set; }
        [Required]
        [Range(2, 50, ErrorMessage = "Number of columns must be 2-50).")]
        public int ColumnsA { get; set; }
    }
}
