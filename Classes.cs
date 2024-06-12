namespace MauiApp1
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
    public class Appointment
    {
        public int Id { get; set; }
        public int Tooth { get; set; }
        public string Description { get; set; }
        public TimeOnly Time { get; set; }
        public DateOnly Date { get; set; }
        public int PatientId { get; set; }
        public Dentist Dentist { get; set; }
        public Cabinet Cabinet { get; set; }
        public Reason Reason { get; set; }
        public int CabinetId { get; set; }
        public int ReasonId { get; set; }

    }
    public class Dentist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

    }
    public class Cabinet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountChairs { get; set; }
    }
    public class Reason
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Minutes { get; set; }
    }
}
