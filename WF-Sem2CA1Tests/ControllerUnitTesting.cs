using Microsoft.EntityFrameworkCore;
using WF_Sem2CA1.Controllers;
using WF_Sem2CA1.Models;
using Microsoft.AspNetCore.Mvc;
using WF_Sem2CA1.DTOs;

namespace WF_Sem2CA1Tests
{
	public class ControllerUnitTesting
	{
		[Fact]
		public async Task GetRequests_Empty()
		{
			var tempDB = new DbContextOptionsBuilder<StudyContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			using (var DbContext = new StudyContext(tempDB))
			{
				var TestController = new StudyHelpController(DbContext);

				var result = await TestController.GetRequests();

				var requestResult = Assert.IsType<OkObjectResult>(result.Result);
				var returnValue = Assert.IsAssignableFrom<IEnumerable<object>>(requestResult.Value);
				Assert.Empty(returnValue);
			}
		}

		[Fact]
		public async Task DeleteRequest_MissingKey()
		{
			var tempDB = new DbContextOptionsBuilder<StudyContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			using var DbContext = new StudyContext(tempDB);
			var TestController = new StudyHelpController(DbContext);

			var result = await TestController.DeleteRequest(1);

			Assert.IsType<UnauthorizedObjectResult>(result);
		}

		[Fact]
		public async Task CreateRequest_AddToDb()
		{
			var tempDB = new DbContextOptionsBuilder<StudyContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			using var DbContext = new StudyContext(tempDB);

			DbContext.Departments.Add(new Department { DeptId = "CompSciMath", DeptTitle = "Comp Science and Maths" });
			await DbContext.SaveChangesAsync();

			DbContext.Modules.Add(new Module { ModuleId = "WF67", DeptId = "CompSciMath", ModuleTitle = "Web Frameworks", ModuleDescription = ""});
			await DbContext.SaveChangesAsync();

			var TestController = new StudyHelpController(DbContext);

			var CreateDTO = new StudyHelpCreateDTO { ModuleId = "WF67", IssueDescription = "Unit Testing" };

			await TestController.RequestCreation(CreateDTO);

			Assert.Equal(1, await DbContext.StudyHelp.CountAsync());
		}
	}
}
