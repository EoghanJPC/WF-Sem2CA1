using WF_Sem2CA1.Models;
using WF_Sem2CA1.Controllers;
using Xunit;
using WF_Sem2CA1.DTOs;

namespace WF_Sem2CA1Tests

{
	public class ModelUnitTests
	{
		[Fact]
		public void StudyHelpCorrectPath()
		{
			var sHelp = new StudyHelp
			{
				id = 1,
				Module = new Module { ModuleTitle = "Web Frameworks" },
				IssueDescription = "Unit Testing",
				SubmittedAt = DateTime.Now,
			};

			var Function = new StudyHelpReadDTO
			{
				id = sHelp.id,
				ModuleTitle = sHelp.Module.ModuleTitle,
				IssueDescription = sHelp.IssueDescription,
				SubmittedAt = sHelp.SubmittedAt
			};

			Assert.Equal(1, Function.id);
			Assert.Equal("Web Frameworks", Function.ModuleTitle);
			Assert.Equal("Unit Testing", Function.IssueDescription);
		}
	}
}
