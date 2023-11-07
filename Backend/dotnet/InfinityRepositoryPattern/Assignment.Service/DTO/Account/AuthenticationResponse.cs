namespace Assignment.Service.DTO.Account
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
