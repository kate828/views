using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewTask.Models
{
    public enum ProductType
    {
        Toy,
        Technique,
        Clothes,
        Transport
    }

    public class Product : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000.")]
        public double Price { get; set; }

        [MinLength(3, ErrorMessage = "Description should be longer than 2 symbols.")]
        public string? Description { get; set; }

        public ProductType Type { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Name))
            {
                if (Description == Name)
                {
                    yield return new ValidationResult(
                        "Description should not be equal to Name.", 
                        new[] { nameof(Description) });
                }

                if (!Description.StartsWith(Name))
                {
                    yield return new ValidationResult(
                        "Description must start with the Name of the product.", 
                        new[] { nameof(Description) });
                }
            }
        }
    }
}