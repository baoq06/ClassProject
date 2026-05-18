namespace ClassProject.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Fname { get; set; } = "";
        public string Lname { get; set; } = "";
        public DateTime? Dob { get; set; }
        public string Gender { get; set; } = "";
        public int GroupId { get; set; }
        public string GroupName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public int UserId { get; set; }
        public byte[]? Picture { get; set; }
        public string FullName => $"{Lname} {Fname}";
    }
}
