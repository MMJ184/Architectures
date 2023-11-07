using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using MediatR;

namespace InfinityCQRS.App.Commands.User
{
    public class UpdateUserCommand : IRequest<ResponseBase<UpdateUserResult>>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Language { get; set; }
    }
}
