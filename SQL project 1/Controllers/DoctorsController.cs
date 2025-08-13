using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using SQL_project_1.DTO;
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


        [HttpPost]

        public async Task<IActionResult> CreateDoctor(DoctorForCreationDto doctor)
        {
            try
            {
                await _doctorRepository.CreateDoctor(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "DoctorById")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctor(id);
                if (doctor == null)
                {
                    return NotFound();
                }
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, DoctorForUpdateDto doctor)
        {
            try
            {
                var dbDoctor = await _doctorRepository.GetDoctor(id);
                if (dbDoctor == null)
                {
                    return NotFound();
                }

                await _doctorRepository.UpdateDoctor(id, doctor);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                var dbDoctor = await _doctorRepository.GetDoctor(id);
                if (dbDoctor == null)
                {
                    return NotFound();
                }

                await _doctorRepository.DeleteDoctor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }
}
