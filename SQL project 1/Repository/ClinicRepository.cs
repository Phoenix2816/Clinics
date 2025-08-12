using System.Data;
using System.Security.Policy;
using Clinics.Context;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using SQL_project_1.DTO;
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
                return clinics.ToList();
            }
        }

        public async Task<List<Clinic>> GetClinicsDoctorsMultipleMapping()
        {
            var query = "SELECT * FROM Clinics JOIN Doctors ON Clinics.Id = Doctors.Clinic_id";
            using (var connection = _context.CreateConnection())
            {
                var clinicDict = new Dictionary<int, Clinic>();
                var clinics = await connection.QueryAsync<Clinic, Doctor, Clinic>(
                    query, (clinic, doctor) =>
                    {
                        if (!clinicDict.TryGetValue(clinic.Id, out var currentClinic))
                        {
                            currentClinic = clinic;
                            clinicDict.Add(currentClinic.Id, currentClinic);
                        }
                        currentClinic.Doctors.Add(doctor);
                        return currentClinic;
                    }
                    );
                return clinics.Distinct().ToList();
            }
        }

        public async Task CreateClinic(ClinicForCreationDto clinic)
        {
            var query = "insert into clinics (Name, Address) values (@Name, @Address)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", clinic.Name, DbType.String); ;
            parameters.Add("Address", clinic.Address, DbType.String); ;

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Clinic> GetClinic(int id)
        {
            var query = "select * from clinics where Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var clinic = await connection.QuerySingleOrDefaultAsync<Clinic>(query, new { id });
                return clinic;

            }
        }
        public async Task UpdateClinic(int id, ClinicForUpdateDto clinic)
        {
            var query = "update clinics set Name = @Name, Address = @Address where Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", clinic.Name, DbType.String);
            parameters.Add("Address", clinic.Address, DbType.String);

            using (var connection = _context.CreateConnection()){
                await connection.ExecuteAsync(query, parameters);   
            }
        }


    }
}
