using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Shared.CustomValidation
{
    public class SelectValidation : ValidationAttribute
    {
        public string SelectName { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int strings = (int)value;

                if (strings > 0)
                {
                    return null;
                }
                return new ValidationResult($"{SelectName} field is required.",
                                            new[] { validationContext.MemberName });
            }
            return null;
        }
    }
}
