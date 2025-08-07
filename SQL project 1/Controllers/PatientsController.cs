using Microsoft.AspNetCore.Mvc;
using SQL_project_1.Entities;
using SQL_project_1.interfaces;

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
    }
}
