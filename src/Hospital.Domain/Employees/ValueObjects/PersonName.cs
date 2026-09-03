using Hospital.Domain.Common;

namespace Hospital.Domain.Employees.ValueObjects
{
    public sealed class PersonName: ValueObject
    {
        public string FirstName { get; }
         public string LastName { get; }
    public PersonName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

}
}