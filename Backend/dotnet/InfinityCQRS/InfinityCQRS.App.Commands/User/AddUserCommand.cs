using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace InfinityCQRS.App.Commands.User
{
    public class AddUserCommand : IRequest<ResponseBase<AddUserResult>>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Language { get; set; }
    }
}
