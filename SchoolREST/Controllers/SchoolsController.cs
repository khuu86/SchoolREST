using Microsoft.AspNetCore.Mvc;
using SchoolLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private TeacherRepository _teachersRepository;

        // Constructor der initialiserer TeacherRepository og tilføjer mock data.
        public TeachersController(TeacherRepository teacherRepository)
        {
            _teachersRepository = teacherRepository;
            _teachersRepository.AddMockTeachers();
        }

        // GET: api/<TeachersController>
        // Henter alle lærere.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> Get([FromQuery] int? minSalary, [FromQuery] string? name, [FromQuery] string? sortBy)
        {
            IEnumerable<Teacher> teachers = _teachersRepository.Get(minSalary, name, sortBy);
            if (teachers == null)
            {
                return NotFound();
            }
            return Ok(teachers);
        }

        // GET api/<TeachersController>/amount
        // Henter et bestemt antal lærere baseret på "amount" headeren.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("amount")]
        public ActionResult<IEnumerable<Teacher>> GetAmount([FromHeader] string? amount)
        {
            if (amount == null)
            {
                return BadRequest("Du har ikke udfyldt amount, men det er ok!");
            }
            if (int.TryParse(amount, out int amountInt))
            {
                var teachers = _teachersRepository.Get();
                if (teachers == null)
                {
                    return NotFound("No teachers found");
                }
                return Ok(teachers.Take(amountInt));
            }
            return BadRequest("Amount header is not a number");
        }

        // GET api/<TeachersController>/5
        // Henter en lærer baseret på ID.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Teacher> Get(int id)
        {
            Teacher? teacher = _teachersRepository.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        // POST api/<TeachersController>
        // Tilføjer en ny lærer.
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Teacher> Post([FromBody] Teacher newTeacher)
        {
            try
            {
                Teacher createdTeacher = _teachersRepository.AddTeacher(newTeacher);
                return Created("/" + createdTeacher.Id, createdTeacher);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TeachersController>/5
        // Opdaterer en eksisterende lærer baseret på ID.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<Teacher?> Put(int id, [FromBody] Teacher putTeacher)
        {
            try
            {
                Teacher? updatedTeacher = _teachersRepository.Update(id, putTeacher);
                if (updatedTeacher == null)
                {
                    return BadRequest("Teacher not found");
                }
                return Ok(updatedTeacher);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TeachersController>/5
        // Sletter en lærer baseret på ID.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Teacher> Delete(int id)
        {
            Teacher? deletedTeacher = _teachersRepository.Remove(id);
            if (deletedTeacher == null)
            {
                return NotFound();
            }
            return Ok(deletedTeacher);
        }
    }
}
