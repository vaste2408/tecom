using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using tecom_test.Validators;

namespace tecom_test.Models
{
    public class Cosmo
    {
        [Required(ErrorMessage = "Name field cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Weight field cannot be empty")]
        [NumericValidator(paramName: "Weight")]
        [Range(1, 300, ErrorMessage = "Weight must be greater than 0")]
        public short Weigth { get; set; }

        [Required(ErrorMessage = "Length field cannot be empty")]
        [NumericValidator(paramName: "Length")]
        [Range(1, 300, ErrorMessage = "Length must be greater than 0")]
        public byte Length { get; set; } //0-255

        [Required(ErrorMessage = "Age field cannot be empty")]
        [NumericValidator(paramName: "Age")]
        [Range(1, 200, ErrorMessage = "Age must be greater than 0")]
        public byte Age { get; set; }

        [Required(ErrorMessage = "Vision field cannot be empty")]
        [Range(0, 1, ErrorMessage = "Vision must be in between 0 and 1")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid vision! Format *.**")]
        [DataType(DataType.Currency)]
        public decimal Vision { get; set; }

        [RegularExpression(@"^\s*[A-Za-z]+(?:\s+[A-Za-z]+)*\s*$", ErrorMessage = "Words must be separeted by 'space'")]
        public string DiseasesAndHabbits { get; set; }

        public string oDiseasesAndHabbits
        {
            get { return String.IsNullOrWhiteSpace(DiseasesAndHabbits) ? "" : DiseasesAndHabbits; }
            set { DiseasesAndHabbits = value; }
        }
    }
}