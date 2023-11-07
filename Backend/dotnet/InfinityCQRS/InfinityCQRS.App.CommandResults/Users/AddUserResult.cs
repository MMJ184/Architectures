using InfinityCQRS.Backend.Contracts;

namespace InfinityCQRS.App.CommandResults.Users
{
    public class AddUserResult : GuidModelBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Language { get; set; }
    }
}
