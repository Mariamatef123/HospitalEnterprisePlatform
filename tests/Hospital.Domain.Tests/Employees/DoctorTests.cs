using FluentAssertions;
using Hospital.Domain.Employees;
using Hospital.Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Tests.Employees
{
    
    public class DoctorTests
    {
        private Doctor CreateDoctor()
        {
            return new Doctor(
                Guid.NewGuid(),
                new NationalId("30203020106543"),
                new PersonName("Mariam", "Atef"),
                new PhoneNumber("01271689560"),
                Guid.NewGuid(),
                DoctorSpecialty.Cardiology,
                "12345");
        }
        [Fact]
        public void Doctor_AssignedToHospitalEmployee_IsDoctor() {
            HospitalEmployee employee = CreateDoctor();

            employee.Should().BeOfType<Doctor>();
        }
        [Fact]
        public void CreateDoctor_WithInvalidSpecialty_ThrowsException()
        {
            Action act = () => new Doctor(
                Guid.NewGuid(),
                new NationalId("30203020106543"),
                new PersonName("Mariam", "Atef"),
                new PhoneNumber("01271689560"),
                Guid.NewGuid(),
                (DoctorSpecialty)999,
                "12345");

            act.Should().Throw<ArgumentException>()
               .WithMessage("Invalid doctor specialty.");
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateDoctor_Should_Throw_Exception_When_LicenseNumber_Is_NullOrWhiteSpace(string? licenseNumber)
        {
            Action act = () => new Doctor(
                Guid.NewGuid(),
                new NationalId("30203020106543"),
                new PersonName("Mariam", "Atef"),
                new PhoneNumber("01271689560"),
                Guid.NewGuid(),
                DoctorSpecialty.Neurology,
                licenseNumber);
            act.Should().Throw<ArgumentException>().WithMessage("License number is required.");
        }
        [Fact]
        public void Doctor_Should_Have_Specialization()
        {
            var doctor = CreateDoctor();
            doctor.Specialty.Should().Be(DoctorSpecialty.Cardiology);
        }
        [Fact]
        public void Doctor_Should_Have_LicenseNumber()
        {
            var doctor = CreateDoctor();
           doctor.LicenseNumber.Should().Be("12345"); 
        }

        [Fact]
        public void Doctor_Should_Have_Correct_Permissions()
        {
            var doctor = CreateDoctor();
            var permissions = doctor.GetRolePermissions();

            permissions.Should().Contain(EmployeePermission.ViewMedicalRecord);
            permissions.Should().Contain(EmployeePermission.UpdateMedicalRecord);
            permissions.Should().Contain(EmployeePermission.CreatePrescription);
        }
    }
}
