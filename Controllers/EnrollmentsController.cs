using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.DAL;
using Project1.DTO;
using Project1.Models;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollment _enrollment;
        private readonly IMapper _mapper;

        public EnrollmentsController(IEnrollment enrollment, IMapper mapper)
        {
            _enrollment = enrollment;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<GetEnrollmentDto> Get()
        {
            var results = _enrollment.GetAll();
            var lstGetEnrollmentDto = _mapper.Map<IEnumerable<GetEnrollmentDto>>(results);
            return lstGetEnrollmentDto;
        }

        [HttpPost]
        public IActionResult Post(AddEnrollmentStudentToCourseDto addEnrollmentStudentToCourseDto)
        {
            try
            {
                var enrollment = _mapper.Map<Enrollment>(addEnrollmentStudentToCourseDto);
                var newEnrollment = _enrollment.Insert(enrollment);

                var getEnrollmentDto = _mapper.Map<GetEnrollmentDto>(newEnrollment);
                return CreatedAtAction("Get", new { id = getEnrollmentDto.EnrollmentID }, getEnrollmentDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
