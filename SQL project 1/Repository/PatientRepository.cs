using Clinics.Context;
using Dapper;
using SQL_project_1.Entities;
using SQL_project_1.interfaces;

namespace SQL_project_1.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DapperContext _context;

        public PatientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            var query = "SELECT * from patients";

            using (var connection = _context.CreateConnection())
            {
                var patients = await connection.QueryAsync<Patient>(query);
                return patients.ToList();
            }
        }

    }
}
