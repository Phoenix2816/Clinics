namespace SQL_project_1.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public int Clinic_id { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
