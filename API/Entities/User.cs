namespace API.Entities
{
    public class User
    {
        public int? Id { get; set; } 
        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? SurName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; } 

    }
}
