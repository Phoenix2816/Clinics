using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using SQL_project_1.Entities;
using SQL_project_1.interfaces;
using SQL_project_1.Repository;

namespace SQL_project_1.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {

            _doctorRepository = doctorRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            try 
            {
                var doctors = await _doctorRepository.GetDoctors();
                return Ok(doctors);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("MultipleMapping")]
        public async Task<IActionResult> GetDoctorsPatientsMultipleMapping()
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctorsPatientsMultipleMapping();
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
