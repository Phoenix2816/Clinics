using Dapper;
using SQL_project_1.Entities;
using SQL_project_1.interfaces;
using Clinics.Context;

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

    }
}
