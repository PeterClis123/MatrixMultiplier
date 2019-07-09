using System;
using System.ComponentModel.DataAnnotations;

namespace MatrixArithmetic.Models
{
    public class MatrixSelectorModel
    {
        [Required]
        [StringLength(16, ErrorMessage = "Identifier too long (16 character limit).")]
        public string Identifier { get; set; }

        public string Description { get; set; }

        [Required]
        public string [,] Classification { get; set; }

        [Required]
        [Range(2, 20, ErrorMessage = "Number of rows ust be 2-20).")]
        public int RowsA { get; set; }

        [Required]
        [Range(2, 20, ErrorMessage = "Number of rows ust be 2-20).")]
        public int RowsB { get; set; }

        [Required]
        [Range(2, 20, ErrorMessage = "Number of rows ust be 2-20).")]
        public int ColumnsA { get; set; }

        [Range(1, 100000, ErrorMessage = "Accommodation invalid (1-100000).")]
        public int MaximumAccommodation { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true",
            ErrorMessage = "This form disallows unapproved ships.")]
        public bool IsValidatedDesign { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }
    }
}
