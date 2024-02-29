using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.Validator
{
    public class GenderAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if ((string)value == "m" || (string)value == "v" || (string)value == "M" || (string)value == "V")
            {
                return true;
            }
            return false;
        }
    }
}
