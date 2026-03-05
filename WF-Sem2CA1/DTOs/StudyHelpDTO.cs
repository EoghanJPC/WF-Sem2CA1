using WF_Sem2CA1.Models;

namespace WF_Sem2CA1.DTOs
{
	public class StudyHelpCreateDTO
	{
		public required string ModuleId { get; set;  }
		public required string IssueDescription { get; set; }
	}

	public class StudyHelpReadDTO
	{
		public int id { get; set;  }
		public string ModuleTitle { get; set; }
		public string IssueDescription { get; set;  }
		public DateTime SubmittedAt { get; set; }
	}
}
