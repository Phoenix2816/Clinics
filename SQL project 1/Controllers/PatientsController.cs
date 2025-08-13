using Microsoft.AspNetCore.Mvc;
using SQL_project_1.DTO;
using SQL_project_1.Entities;
using SQL_project_1.interfaces;
using SQL_project_1.Repository;

namespace SQL_project_1.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientsController(IPatientRepository patientRepository)
        {

            _patientRepository = patientRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            try 
            {
                var patients = await _patientRepository.GetPatients();
                return Ok(patients);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPost]

        public async Task<IActionResult> CreatePatient(PatientForCreationDto patient)
        {
            try
            {
                await _patientRepository.CreatePatient(patient);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}", Name = "PatientById")]
        public async Task<IActionResult> GetPatient(int id)
        {
            try
            {
                var patient = await _patientRepository.GetPatient(id);
                if (patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientForUpdateDto patient)
        {
            try
            {
                var dbPatient = await _patientRepository.GetPatient(id);
                if (dbPatient == null)
                {
                    return NotFound();
                }

                await _patientRepository.UpdatePatient(id, patient);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                var dbPatient = await _patientRepository.GetPatient(id);
                if (dbPatient == null)
                {
                    return NotFound();
                }

                await _patientRepository.DeletePatient(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }


    }
}
