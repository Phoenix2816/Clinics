using Microsoft.AspNetCore.Mvc;
using SQL_project_1.DTO;
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

        [HttpGet("MultipleMapping")]
        public async Task<IActionResult> GetClinicsDoctorsMultipleMapping()
        {
            try
            {
                var clinic = await _clinicRepository.GetClinicsDoctorsMultipleMapping();
                return Ok(clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpPost]

        public async Task<IActionResult> CreateClinic(ClinicForCreationDto clinic)
        {
            try
            {
                await _clinicRepository.CreateClinic(clinic);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "ClinicById")]
        public async Task<IActionResult> GetClinic(int id)
        {
            try
            {
                var clinic = await _clinicRepository.GetClinic(id);
                if (clinic == null)
                {
                    return NotFound();
                }
                return Ok(clinic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClinic(int id, ClinicForUpdateDto clinic)
        {
            try
            {
                var dbClinic = await _clinicRepository.GetClinic(id);
                if (dbClinic == null)
                {
                    return NotFound();
                }

                await _clinicRepository.UpdateClinic(id, clinic);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {
            try
            {
                var dbClinic = await _clinicRepository.GetClinic(id);
                if (dbClinic == null)
                {
                    return NotFound();
                }

                await _clinicRepository.DeleteClinic(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

    }
}