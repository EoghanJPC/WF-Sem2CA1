using Microsoft.EntityFrameworkCore;

namespace WF_Sem2CA1.Models
{
	public class StudyContext : DbContext
	{
		public StudyContext(DbContextOptions<StudyContext> options) : base(options) { }

		public DbSet<StudyHelp> StudyHelp { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Module> Modules { get; set; }
	}
}
