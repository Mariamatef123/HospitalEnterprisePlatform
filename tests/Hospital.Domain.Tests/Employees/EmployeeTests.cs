using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Tests.Employees
{
    public class EmployeeTests
    {
        [Fact]
        public void HospitalEmployee_IsAbstract_CannotBeInstantiated()
        {
            // Arrange
            Type employeeType = typeof(Hospital.Domain.Employees.HospitalEmployee);
            // Act
            bool isAbstract = employeeType.IsAbstract;
            // Assert
            Assert.True(isAbstract, "HospitalEmployee should be an abstract class and cannot be instantiated.");
        }
    }
}
