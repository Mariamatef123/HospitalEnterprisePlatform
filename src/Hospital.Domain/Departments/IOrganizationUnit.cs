namespace Hospital.Domain.Departments;

public interface IOrganizationUnit
{
    Guid Id { get; }
    string Name { get; }

    IReadOnlyCollection<IOrganizationUnit> Children { get; }

    void AddSubDepartment(IOrganizationUnit child);
    void RemoveSubDepartment(IOrganizationUnit child);

    bool HasChildren();
}