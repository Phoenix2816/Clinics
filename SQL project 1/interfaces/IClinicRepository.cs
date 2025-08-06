using SQL_project_1.Entities;

namespace SQL_project_1.interfaces
{
    public interface IClinicRepository
    {
        public Task<IEnumerable<Clinic>> GetClinics();
    }
}
