using SQL_project_1.DTO;
using SQL_project_1.Entities;

namespace SQL_project_1.interfaces
{
    public interface IPatientRepository
    {
        public Task CreatePatient(PatientForCreationDto patient);
        public Task<IEnumerable<Patient>> GetPatients();


        public Task<Patient> GetPatient(int id);
        public Task UpdatePatient(int id, PatientForUpdateDto patient);

    }
}
