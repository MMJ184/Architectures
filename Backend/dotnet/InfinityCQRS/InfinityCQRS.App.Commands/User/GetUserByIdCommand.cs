using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using InfinityCQRS.Backend.Contracts;
using MediatR;

namespace InfinityCQRS.App.Commands.User
{
    public class GetUserByIdCommand : GuidModelBase, IRequest<ResponseBase<GetUsersResult>>
    {
    }
}
