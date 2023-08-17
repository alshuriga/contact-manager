using FluentValidation;

namespace ContactManagerTest.Models
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Salary).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.Married).NotNull();
            RuleFor(x => x.Phone).NotNull();
        }
    }
}
