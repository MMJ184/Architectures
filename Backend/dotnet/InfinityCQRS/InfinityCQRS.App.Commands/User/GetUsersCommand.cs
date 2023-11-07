using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using MediatR;

namespace InfinityCQRS.App.Commands.User
{
    public class GetUsersCommand : IRequest<ResponseBase<List<GetUsersResult>>>
    {
    }
}
