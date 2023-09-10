using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using EmployeeManagement.Test.TestData;
using Xunit;
using Xunit.Sdk;

namespace EmployeeManagement.Test
{
	[Collection("EmployeeServiceCollection")]
	public class DataDrivenEmployeeServiceTests //: IClassFixture<EmployeeServiceFixture>
	{
		private readonly EmployeeServiceFixture _employeeServiceFixture;

		public DataDrivenEmployeeServiceTests(
			EmployeeServiceFixture employeeServiceFixture)
		{
			_employeeServiceFixture = employeeServiceFixture;
		}

		[Fact]
		public async Task GiveRaise_MinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeTrue()
		{
			// Arrange  
			var internalEmployee = new InternalEmployee(
				"Brooklyn", "Cannon", 5, 3000, false, 1);

			// Act
			await _employeeServiceFixture
				.EmployeeService.GiveRaiseAsync(internalEmployee, 100);

			// Assert
			Assert.True(internalEmployee.MinimumRaiseGiven);
		}


		[Fact]
		public async Task GiveRaise_MoreThanMinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeFalse()
		{
			// Arrange  
			var internalEmployee = new InternalEmployee(
				"Brooklyn", "Cannon", 5, 3000, false, 1);

			// Act 
			await _employeeServiceFixture.EmployeeService
				.GiveRaiseAsync(internalEmployee, 200);

			// Assert
			Assert.False(internalEmployee.MinimumRaiseGiven);
		}

		public static IEnumerable<object[]> EmployeeTestDataForGiveRaise_WithProperty
		{
			get
			{
				return new List<object[]> {
				 new object[] {100, true },
				 new object[] {200, false }
				};
			}
		}

		public static IEnumerable<object[]> EmployeeTestDataForGiveRaise_WithMethod(int allowedData)
		{
			var list  = new List<object[]> {
				 new object[] {100, true },
				 new object[] {200, false }
				};

			return list.Take(allowedData);

		}

		[Theory]
		//[MemberData(nameof(EmployeeTestDataForGiveRaise_WithProperty))]
		//[MemberData(nameof(EmployeeTestDataForGiveRaise_WithMethod))]
		[MemberData(nameof(DataDrivenEmployeeServiceTests.EmployeeTestDataForGiveRaise_WithMethod),
			1,
			MemberType = typeof(DataDrivenEmployeeServiceTests))
		]
		[ClassData(typeof(EmployeeServiceTestData))]
		[ClassData(typeof(StronglyTypeEmployeeServiceData))]
		public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue(int raiseGiven, bool expectedValueForMinimumRaiseGiven)
		{
			var internalEmployee = new InternalEmployee(
			"Brooklyn", "Cannon", 5, 3000, false, 1);

			// Act 
			await _employeeServiceFixture.EmployeeService
				.GiveRaiseAsync(internalEmployee, raiseGiven);

			// Assert
			Assert.Equal(expectedValueForMinimumRaiseGiven, internalEmployee.MinimumRaiseGiven);
		}


		[Theory]
		[InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
		[InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
		public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedSecondObligatoryCourse(Guid courseId)
		{

			// Act
			var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Brooklyn", "Cannon");

			// Assert
			Assert.Contains(internalEmployee.AttendedCourses,
				course => course.Id == courseId);
		}
	}
}
