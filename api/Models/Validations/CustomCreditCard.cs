using api.Extensions;
using System.ComponentModel.DataAnnotations;

namespace api.Models.Validations
{
    public class CustomCreditCard : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object? value)
        {
            string? strValue = value as string;
            if (!string.IsNullOrEmpty(strValue))
            {
                var digits = strValue.OnlyNumbers();
                if (digits.Length == 16) return true;
            }

            return false;
        }
    }
}
