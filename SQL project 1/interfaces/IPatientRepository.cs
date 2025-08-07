using SQL_project_1.Entities;

namespace SQL_project_1.interfaces
{
    public interface IPatientRepository
    {
        public Task<IEnumerable<Patient>> GetPatients();

    }
}
