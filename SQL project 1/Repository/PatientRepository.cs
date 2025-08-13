using System.Data;
using Clinics.Context;
using Dapper;
using SQL_project_1.DTO;
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

        public async Task CreatePatient(PatientForCreationDto patient)
        {
            var query = "insert into patients (Name, Surname, Diagnos, Doctor_id) values (@Name, @Surname, @Diagnos, @Doctor_id)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", patient.Name, DbType.String); ;
            parameters.Add("Surname", patient.Surname, DbType.String); ;
            parameters.Add("Diagnos", patient.Diagnos, DbType.String); ;
            parameters.Add("Doctor_id", patient.Doctor_id, DbType.Int32); ;

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<Patient> GetPatient(int id)
        {
            var query = "select * from patients where Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var patient = await connection.QuerySingleOrDefaultAsync<Patient>(query, new { id });
                return patient;

            }
        }
        public async Task UpdatePatient(int id, PatientForUpdateDto patient)
        {
            var query = "UPDATE patients SET Name = @Name, Surname = @Surname, Diagnos = @Diagnos, Doctor_id = @Doctor_id WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", patient.Name, DbType.String);
            parameters.Add("Surname", patient.Surname, DbType.String);
            parameters.Add("Diagnos", patient.Diagnos, DbType.String);
            parameters.Add("Doctor_id", patient.Doctor_id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeletePatient(int id)
        {
            var query = "delete from patients where Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

    }
}
