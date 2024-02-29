using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.Validator
{
    public class RealisticDayOfBirthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            DateTime longTimeAgo = DateTime.Today.AddYears(-100);
            return date < DateTime.Now && longTimeAgo < date;
        }
    }
}
