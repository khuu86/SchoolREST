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

        public TeachersController(TeacherRepository teacherRepository)
        {
            _teachersRepository = teacherRepository;
        }

        // GET: api/<TeachersController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Teacher>> Get()
        {
            IEnumerable<Teacher> teachers = _teachersRepository.Get();
            if (teachers == null)
            {
                return NotFound();
            }
            return Ok(teachers);
        }


        // GET api/<TeachersController>/5
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
