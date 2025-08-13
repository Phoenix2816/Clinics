using System.Data;
using System.Numerics;
using Clinics.Context;
using Dapper;
using SQL_project_1.DTO;
using SQL_project_1.Entities;
using SQL_project_1.interfaces;

namespace SQL_project_1.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DapperContext _context;

        public DoctorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            var query = "SELECT * from doctors";

            using (var connection = _context.CreateConnection())
            {
                var doctors = await connection.QueryAsync<Doctor>(query);
                return doctors.ToList();
            }
        }

        public async Task<List<Doctor>> GetDoctorsPatientsMultipleMapping()
        {
            var query = "SELECT * FROM Doctors JOIN Patients ON Doctors.Id = Patients.Doctor_id";
            using (var connection = _context.CreateConnection())
            {
                var doctorDict = new Dictionary<int, Doctor>();
                var doctors = await connection.QueryAsync<Doctor, Patient, Doctor>(
                    query, (doctor, patient) =>
                    {
                        if (!doctorDict.TryGetValue(doctor.Id, out var currentDoctor))
                        {
                            currentDoctor = doctor;
                            doctorDict.Add(currentDoctor.Id, currentDoctor);
                        }
                        currentDoctor.Patients.Add(patient);
                        return currentDoctor;
                    }
                    );
                return doctors.Distinct().ToList();
            }
        }


        public async Task CreateDoctor(DoctorForCreationDto doctor)
        {
            var query = "insert into doctors (Name, Surname, Position, Clinic_id) values (@Name, @Surname, @Position, @Clinic_id)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", doctor.Name, DbType.String); ;
            parameters.Add("Surname", doctor.Surname, DbType.String); ;
            parameters.Add("Position", doctor.Position, DbType.String); ;
            parameters.Add("Clinic_id", doctor.Clinic_id, DbType.Int32); ;

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<Doctor> GetDoctor(int id)
        {
            var query = "select * from doctors where Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var doctor = await connection.QuerySingleOrDefaultAsync<Doctor>(query, new { id });
                return doctor;

            }
        }
        public async Task UpdateDoctor(int id, DoctorForUpdateDto doctor)
        {
            var query = "update doctors set Name = @Name, Surname = @Surname, Position = @Position, Clinic_id = @Clinic_id where Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", doctor.Name, DbType.String);
            parameters.Add("Surname", doctor.Surname, DbType.String);
            parameters.Add("Position", doctor.Position, DbType.String);
            parameters.Add("Clinic_id", doctor.Clinic_id, DbType.Int32);  // Add Clinic_id parameter

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteDoctor(int id)
        {
            var query = "delete from doctors where Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


    }
}
