using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Person : IValidatableObject
    {
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 10, ErrorMessage = "Range from 1-10")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Threshold is required")]
        public int? Threshold { get; set; }

        [Display(Name="Title")]
        [Required(ErrorMessage = "Title is required")]
        public int? TitleId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age < Threshold)
                yield return new ValidationResult("Age cannot be less than the threshold", new[] { "Age" });

            if (Age == null && Threshold == null && string.IsNullOrEmpty(Name))
                yield return new ValidationResult("You must enter at least one value", new[] { string.Empty });
        }
    }
}