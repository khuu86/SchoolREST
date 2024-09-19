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
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            return _teachersRepository.Get();
        }

        // GET api/<TeachersController>/5
        [HttpGet("{id}")]
        public Teacher Get(int id)
        {
            return _teachersRepository.GetById(id);
        }

        // POST api/<TeachersController>
        [HttpPost]
        public Teacher Post([FromBody] Teacher newTeacher)
        {
         return _teachersRepository.AddTeacher(newTeacher);
        }

        // PUT api/<TeachersController>/5
        [HttpPut("{id}")]
        public Teacher Put(int id, [FromBody] Teacher putTeacher)
        {
            return _teachersRepository.Update(id, putTeacher);
        }

        // DELETE api/<TeachersController>/5
        [HttpDelete("{id}")]
        public Teacher Delete(int id)
        {
            return _teachersRepository.Remove(id);
        }
    }
}
