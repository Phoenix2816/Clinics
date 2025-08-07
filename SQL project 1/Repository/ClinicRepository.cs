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

        //Task<List<Clinic>> IClinicRepository.GetClinicsDoctorsMultipleMapping()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
