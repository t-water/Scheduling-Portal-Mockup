using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TEServerTest.Validation
{
    public sealed class StartBeforeEnd : ValidationAttribute, IClientValidatable
    {
        private readonly string propertyName;
        private readonly bool allowEqualDates;

        public StartBeforeEnd(string propertyName, bool allowEqualDates = false)
        {
            this.propertyName = propertyName;
            this.allowEqualDates = allowEqualDates;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.propertyName);
            if (property == null)
            {
                return new ValidationResult($"Unknown property {this.propertyName}");
            }

            var propertyValue = property.GetValue(validationContext.ObjectInstance, null);

            if (value == null || !(value is DateTime))
            {
                return ValidationResult.Success;
            }

            if (propertyValue == null || !(propertyValue is DateTime))
            {
                return ValidationResult.Success;
            }

            // Compare values
            if ((DateTime)value >= (DateTime)propertyValue)
            {
                if (this.allowEqualDates && value.Equals(propertyValue))
                {
                    return ValidationResult.Success;
                }
                else if ((DateTime)value > (DateTime)propertyValue)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("End Must Come After Start");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessageString,
                ValidationType = "isdateafter"
            };
            rule.ValidationParameters["propertytested"] = this.propertyName;
            rule.ValidationParameters["allowequaldates"] = this.allowEqualDates;
            yield return rule;
        }
    }
}