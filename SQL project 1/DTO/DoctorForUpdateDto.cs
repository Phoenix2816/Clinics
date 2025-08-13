namespace SQL_project_1.DTO
{
    public class DoctorForUpdateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }

        // Add this property to allow updating Clinic_id
        public int Clinic_id { get; set; }
    }
}
