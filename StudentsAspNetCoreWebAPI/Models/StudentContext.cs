using Microsoft.EntityFrameworkCore;

namespace StudentsAspNetCoreWebAPI.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public StudentContext(DbContextOptions<StudentContext>options)
            :base(options)
        {
            if (Database.EnsureCreated())
            {
                Students.Add(new Student { Name = "John", Surname = "Johnson", Age = 20, GPA = 10.5 });
                Students.Add(new Student { Name = "George", Surname = "Anderson", Age = 23, GPA = 11.5 });
                Students.Add(new Student { Name = "Peter", Surname = "Peterson", Age = 25, GPA = 12 });
                SaveChanges();
            }    
        }
    }
}
