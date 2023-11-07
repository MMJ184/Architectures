using InfinityCQRS.App.CommandResults;
using MediatR;

namespace InfinityCQRS.App.Commands.User
{
    public class GetUsersCountRequest : IRequest<ResponseBase<long>>
    {
    }
}
