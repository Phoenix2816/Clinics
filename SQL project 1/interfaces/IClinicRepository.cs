using SQL_project_1.DTO;
using SQL_project_1.Entities;

namespace SQL_project_1.interfaces
{
    public interface IClinicRepository
    {
        public Task CreateClinic(ClinicForCreationDto clinic);
        public Task<IEnumerable<Clinic>> GetClinics();

        public Task<List<Clinic>> GetClinicsDoctorsMultipleMapping();
        public Task<Clinic> GetClinic(int id);
        public Task UpdateClinic(int id, ClinicForUpdateDto clinic);
    }
}
