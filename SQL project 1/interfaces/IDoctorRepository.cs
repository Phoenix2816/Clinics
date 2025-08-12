using SQL_project_1.DTO;
using SQL_project_1.Entities;

namespace SQL_project_1.interfaces
{
    public interface IDoctorRepository
    {
        public Task CreateDoctor(DoctorForCreationDto doctor);
        public Task<IEnumerable<Doctor>> GetDoctors();

        public Task<List<Doctor>> GetDoctorsPatientsMultipleMapping();
        public Task<Doctor> GetDoctor(int id);
        public Task UpdateDoctor(int id, DoctorForUpdateDto doctor);
    }
}
