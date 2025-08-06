using Clinics.Context;
using Dapper;
using SQL_project_1.Entities;
using SQL_project_1.interfaces;

namespace SQL_project_1.Repository
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly DapperContext _context;

        public ClinicRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clinic>> GetClinics()
        {
            var query = "SELECT * from clinics";

            using (var connection = _context.CreateConnection())
            {
                var clinics = await connection.QueryAsync<Clinic>(query);
                Console.WriteLine("clinics: ", clinics.Count());
                return clinics.ToList();
            }
        }
    }
}
