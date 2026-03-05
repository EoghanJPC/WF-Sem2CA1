namespace WF_Sem2CA1.Models
{
	public class StudyHelp
	{
		public int id { get; set; }
		public string ModuleId {  get; set; }
		public Module? Module { get; set; }
		public required string IssueDescription { get; set; }
		public DateTime SubmittedAt { get; set; } = DateTime.Now;
	}

	public class Department
	{
		[System.ComponentModel.DataAnnotations.Key]
		public string DeptId { get; set; }
		public required string DeptTitle { get; set; }
		public List<Module> Modules { get; set; } = new();
	}

	public class Module
	{
		[System.ComponentModel.DataAnnotations.Key]
		public string ModuleId { get; set; }
		public string DeptId { get; set; }
		public Department? Department { get; set; }
		public required string ModuleTitle { get; set; }
		public string ModuleDescription { get; set; }
	}
}
