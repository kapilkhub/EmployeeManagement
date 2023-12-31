﻿using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using Xunit;

namespace EmployeeManagement.Test
{
	public class EmployeeFactoryTests : IDisposable
    { 
        private EmployeeFactory _employeeFactory;

        public EmployeeFactoryTests()
        {
			_employeeFactory = new EmployeeFactory();

		}
        public void Dispose()
		{
			// implement cleanup code
		}
		[Fact]
		[Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
          

            var employee = (InternalEmployee)_employeeFactory
				.CreateEmployee("Kevin", "Dockx");

            Assert.Equal(2500, employee.Salary);
        }

        [Fact]
		[Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {

            // Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");

            // Assert
            Assert.True(employee.Salary >= 3000 && employee.Salary <= 3500,
                "Salary not in acceptable range.");
        }

        [Fact]
		[Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_Alternative()
        {
           

            // Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");

            // Assert
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
        }

        [Fact]
		[Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_AlternativeWithInRange()
        {
          
            // Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");

            // Assert
            Assert.InRange(employee.Salary, 2500, 3500);
        }


        [Fact]
		[Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
		public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
        {
          

            // Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");
            employee.Salary = 2500.123m;

            // Assert
            Assert.Equal(2500, employee.Salary, 0);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_ReturnType")]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
         

            // Act
            var employee = _employeeFactory.CreateEmployee("Kevin", "Dockx", "Marvin", true);

            // Assert
            Assert.IsType<ExternalEmployee>(employee);
            //Assert.IsAssignableFrom<Employee>(employee);
        }

		
	}
}
