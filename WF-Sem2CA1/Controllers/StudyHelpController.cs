using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WF_Sem2CA1.DTOs;
using WF_Sem2CA1.Models;

namespace WF_Sem2CA1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudyHelpController : ControllerBase
	{
		private readonly StudyContext sContext;

		public StudyHelpController(StudyContext con)
		{
			sContext = con;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<StudyHelpReadDTO>>> GetRequests()
		{
			var ReadRequests = await sContext.StudyHelp
				.Include(s => s.Module)
				.Select(s => new StudyHelpReadDTO
				{
					id = s.id,
					ModuleTitle = s.Module.ModuleTitle,
					IssueDescription = s.IssueDescription,
					SubmittedAt = s.SubmittedAt,
				})
				.ToListAsync();

			return Ok(ReadRequests);
		}

		[HttpPost]
		public async Task<ActionResult> RequestCreation(StudyHelpCreateDTO CreateDTO)
		{
			var CreateRequests = new StudyHelp
			{
				ModuleId = CreateDTO.ModuleId,
				IssueDescription = CreateDTO.IssueDescription,
				SubmittedAt = DateTime.Now
			};

			sContext.StudyHelp.Add(CreateRequests);
			await sContext.SaveChangesAsync();

			return CreatedAtAction(nameof(GetRequests), new {id = CreateRequests.id}, "Create Request Successful !");
		}

		[HttpPut("{id}")]
		public async Task <IActionResult> UpdateRequest(int id, StudyHelpCreateDTO CreateDTO)
		{
			if(!Request.Headers.TryGetValue("Auth-Token", out var token) || token != "AuthenicationToken")
			{
				return Unauthorized("Auth-Token is required and is missing !");
			}

			var CreateRequest = await sContext.StudyHelp.FindAsync(id);
			if (CreateRequest == null) return NotFound();

			CreateRequest.IssueDescription = CreateDTO.IssueDescription;
			CreateRequest.ModuleId = CreateDTO.ModuleId;

			await sContext.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRequest(int id)
		{
			if (!Request.Headers.TryGetValue("Auth-Token", out var token) || token != "AuthenicationToken")
			{
				return Unauthorized("Auth-Token is required and is missing !");
			}

			var request = await sContext.StudyHelp.FindAsync(id);
			if (request == null) return NotFound();

			sContext.StudyHelp.Remove(request);
			await sContext.SaveChangesAsync();

			return Ok($"Deletion Success for request: {id}");
		}
	}
}
