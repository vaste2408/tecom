using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tecom_test.Validators
{
    public class NumericValidatorAttribute : ValidationAttribute
    {
        private short minvalue;
        private short maxvalue;
        public NumericValidatorAttribute (short minValue = 0, short maxValue = short.MaxValue, string errorMessage = null, string paramName = "Param")
        {
            minvalue = minValue;
            maxvalue = maxValue;
            ErrorMessage = errorMessage ?? paramName + " cannot be less than " + minvalue.ToString() + (maxValue != short.MaxValue ? " and more than " + maxValue.ToString() : "");
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                short number = Convert.ToInt16(value);
                if (number > minvalue && number < maxvalue)
                    return true;
            }
            return false;
        }
    }
}