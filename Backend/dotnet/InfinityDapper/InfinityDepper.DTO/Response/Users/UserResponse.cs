namespace InfinityDapper.DTO.Response.Users
{
    public class UserResponse : BaseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string PasswordHash { get; set; }
    }
}
