using Microsoft.AspNetCore.Mvc;
using SQL_project_1.interfaces;

namespace SQL_project_1.Controllers
{
    [Route("api/clinics")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicsController(IClinicRepository clinicRepository)
        {

            _clinicRepository = clinicRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetClinics()
        {
            try 
            {
                var clinics = await _clinicRepository.GetClinics();
                return Ok(clinics);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }

        }
    }
}
