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
    public class CoursesController : ControllerBase
    {
        private readonly ICourse _course;
        private readonly IMapper _mapper;

        public CoursesController(ICourse course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<GetCourseDto> Get()
        {
            var results = _course.GetAll();
            var lstGetCourseDto = _mapper.Map<IEnumerable<GetCourseDto>>(results);
            return lstGetCourseDto;
        }

        [HttpGet("{id}")]
        public GetCourseDto Get(int id)
        {
            var result = _course.GetById(id);
            var getCourseDto = _mapper.Map<GetCourseDto>(result);
            return getCourseDto;
        }

        [HttpGet("GetByName")]
        public IEnumerable<GetCourseDto> GetByName(string name)
        {
            var results = _course.GetByName(name);
            var lstGetCourseDto = _mapper.Map<IEnumerable<GetCourseDto>>(results);
            return lstGetCourseDto;
        }

        [HttpPost]
        public IActionResult Post(AddCourseDto addCourseDto)
        {
            try
            {
                var course = _mapper.Map<Course>(addCourseDto);
                var newCourse = _course.Insert(course);

                var getCourseDto = _mapper.Map<GetCourseDto>(newCourse);
                return CreatedAtAction("Get", new { id = getCourseDto.CourseID }, getCourseDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AddCourseDto addCourseDto)
        {
            try
            {
                var course = new Course
                {
                    CourseID = id,
                    Title = addCourseDto.Title,
                    Credits = addCourseDto.Credits
                };

                var editCourse = _course.Update(course);
                GetCourseDto getCourseDto = new GetCourseDto
                {
                    CourseID = id,
                    Title = addCourseDto.Title,
                    Credits = addCourseDto.Credits
                };

                return Ok(getCourseDto);
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
                _course.Delete(id);
                return Ok($"Delete id {id} berhasil");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByIdWithStudents")]
        public CourseWithStudentDto WithCourse(int courseId)
        {
            var courses = _course.GetCourseWithStudent(courseId);
            var courseWithStudentDto = _mapper.Map<CourseWithStudentDto>(courses);
            return courseWithStudentDto;
        }

        [HttpGet("GetAllWithStudents")]
        public IEnumerable<CourseWithStudentDto> GetAllWithStudent()
        {
            var results = _course.GetAllWithStudent();
            var lstCourseWithStudentDto = _mapper.Map<IEnumerable<CourseWithStudentDto>>(results);
            return lstCourseWithStudentDto;
        }
    }
}
