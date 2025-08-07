namespace SQL_project_1.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Diagnos { get; set; }
        public int Doctor_id { get; set; }
    }
}
