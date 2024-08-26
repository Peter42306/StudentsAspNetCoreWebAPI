using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsAspNetCoreWebAPI.Models;

namespace StudentsAspNetCoreWebAPI.Controllers
{
    [ApiController] // Indicates that this class is an API controller, which automatically applies features like model validation and response formatting
    [Route("api/Students")] // Specifies the route template for this controller. All actions in this controller will be accessible via "api/Students"
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _studentContext;

        public StudentController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        // GET: api/Students
        [HttpGet] // Maps this method to a GET request at the route "api/Students"
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _studentContext.Students.ToListAsync();
        }

        // GET: api/Students/{id}
        [HttpGet("{id}")] // Maps this method to a GET request at the route "api/Students/{id}", where {id} is a placeholder for the student ID
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentContext.Students.SingleOrDefaultAsync(e => e.Id == id); // Asynchronously retrieves a single student based on the provided ID

            if (student == null)
            {
                return NotFound(); // If no student is found, return a 404 Not Found response
            }

            return new ObjectResult(student); // Returns the found student with a 200 OK status
        }

        // PUT: api/Students
        [HttpPut] // Maps this method to a PUT request at the route "api/Students"
        public async Task<ActionResult<Student>> PutStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns a 400 Bad Request response if the model state is invalid
            }

            if (!_studentContext.Students.Any(e => e.Id == student.Id))
            {
                return NotFound(); // Returns a 404 Not Found response if the student does not exist
            }

            _studentContext.Update(student);

            await _studentContext.SaveChangesAsync();

            return Ok(); // Returns a 200 OK response indicating the update was successful
        }

        // POST: api/Students
        [HttpPost] // Maps this method to a POST request at the route "api/Students"
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns a 400 Bad Request response if the model state is invalid
            }

            _studentContext.Add(student);

            await _studentContext.SaveChangesAsync();

            return Ok(); // Returns a 200 OK response indicating the student was successfully created
        }

        // DELETE: api/Students/{id}
        [HttpDelete("{id}")] // Maps this method to a DELETE request at the route "api/Students/{id}"
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns a 400 Bad Request response if the model state is invalid
            }

            var student = await _studentContext.Students.SingleOrDefaultAsync(e => e.Id == id);

            if (student == null)
            {
                return NotFound(); // If no student is found, return a 404 Not Found response
            }

            _studentContext.Students.Remove(student);

            await _studentContext.SaveChangesAsync();

            return Ok(student); // Returns the deleted student along with a 200 OK response
        }
    }
}
