using FluentAssertions;
using Hospital.Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Domain.Tests.Employees.ValueObjects
{

        public class PersonNameTests
        {
        [Theory]
        [InlineData("Mariam", "Atef")]
        [InlineData("Ahmed", "Ali")]
        [InlineData("John", "Smith")]
        public void Create_WithValidName_CreatesPersonName(
        string firstName,
        string lastName)
        {
            PersonName personName = new PersonName(firstName, lastName);

            personName.FirstName.Should().Be(firstName);
            personName.LastName.Should().Be(lastName);
        }
        [Fact]
        public void Create_WithSpacesAroundFirstName_TrimsSpaces()
        {
            var prersonName = new PersonName(" Mariam ","Atef");

           PersonName expectedPersonName = new PersonName("Mariam", "Atef");
            prersonName.Should().Be(expectedPersonName);
        }
        [Fact]
        public void Create_WithSpacesAroundLastName_TrimsSpaces()
        {
            var prersonName = new PersonName("Mariam", " Atef ");
            PersonName expectedPersonName = new PersonName("Mariam", "Atef");
            prersonName.Should().Be(expectedPersonName);
        }

        [Theory]
        [InlineData(null, "Atef")]
        [InlineData("", "Atef")]
        [InlineData(" ", "Atef")]
        public void Create_WithInvalidFirstName_ThrowsException(string? firstName,string lastName)
        {
            Action act = () => new PersonName(firstName!, lastName);

            act.Should()
               .Throw<ArgumentException>()
               .WithMessage("First name is required.");
        }
        [Theory]
        [InlineData("Mariam", null)]
        [InlineData("Mariam", "")]
        [InlineData("Mariam", " ")]
        public void Create_WithInvalidLastName_ThrowsException(string firstName,string? lastName)
        {
            Action act = () => new PersonName(firstName, lastName!);

            act.Should()
               .Throw<ArgumentException>()
               .WithMessage("Last name is required.");
        }
        [Fact]
        public void TwoPersonNames_WithSameValue_AreEqual()
        {
            
            var personName1 = new PersonName("Mariam", "Atef");
            var personName2 = new PersonName("Mariam", "Atef");

           
            personName1.Should().Be(personName2);
        }

        [Fact]
            public void TwoPersonNames_WithDifferentValues_AreNotEqual()
            {

            var personName1 = new PersonName("Mariam", "Hany");
            var personName2 = new PersonName("Mariam", "Atef");


            personName1.Should().NotBe(personName2);

        }
        }
    
}
