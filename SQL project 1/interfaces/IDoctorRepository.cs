using SQL_project_1.Entities;

namespace SQL_project_1.interfaces
{
    public interface IDoctorRepository
    {
        public Task<IEnumerable<Doctor>> GetDoctors();

        public Task<List<Doctor>> GetDoctorsPatientsMultipleMapping();
    }
}
