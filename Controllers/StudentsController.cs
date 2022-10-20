using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.DAL;
using Project1.DTO;
using Project1.Models;

namespace Project1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudent _student;
        private readonly IMapper _mapper;

        public StudentsController(IStudent student, IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<GetStudentDto> Get()
        {
            var results = _student.GetAll();
            var lstGetStudentsDto = _mapper.Map<IEnumerable<GetStudentDto>>(results);
            return lstGetStudentsDto;
        }

        [HttpGet("{id}")]
        public GetStudentDto Get(int id)
        {
            var result = _student.GetById(id);
            var getStudentDto = _mapper.Map<GetStudentDto>(result);
            return getStudentDto;
        }

        [HttpGet("GetByName")]
        public IEnumerable<GetStudentDto> GetByName(string name)
        {
            var results = _student.GetByName(name);
            var lstGetAllStudentsDto = _mapper.Map<IEnumerable<GetStudentDto>>(results);
            return lstGetAllStudentsDto;
        }

        [HttpPost]
        public IActionResult Post(AddStudentDto addStudentDto)
        {
            try
            {
                var student = _mapper.Map<Student>(addStudentDto);
                var newStudent = _student.Insert(student);

                var getStudentDto = _mapper.Map<GetStudentDto>(newStudent);
                return CreatedAtAction("Get", new { id = getStudentDto.ID }, getStudentDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AddStudentDto addStudentDto)
        {
            try
            {
                var student = new Student
                {
                    ID = id,
                    LastName = addStudentDto.LastName,
                    FirstMidName = addStudentDto.FirstMidName,
                    EnrollmentDate = addStudentDto.EnrollmentDate
                };

                var editStudent = _student.Update(student);
                GetStudentDto getStudentDto = new GetStudentDto
                {
                    ID = id,
                    LastName = addStudentDto.LastName,
                    FirstMidName = addStudentDto.FirstMidName,
                    EnrollmentDate = addStudentDto.EnrollmentDate
                };

                return Ok(getStudentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _student.Delete(id);
                return Ok($"Delete id {id} berhasil");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByIdWithCourses")]
        public StudentWithCourseDto WithCourse(int studentId)
        {
            var students = _student.GetStudentWithCourse(studentId);
            var studentWithCourseDto = _mapper.Map<StudentWithCourseDto>(students);
            return studentWithCourseDto;
        }

        [HttpGet("GetAllWithCourses")]
        public IEnumerable<StudentWithCourseDto> GetAllWithCourse()
        {
            var results = _student.GetAllWithCourse();
            var lstStudentWithCourseDto = _mapper.Map<IEnumerable<StudentWithCourseDto>>(results);
            return lstStudentWithCourseDto;
        }
    }
}
