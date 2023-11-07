using InfinityCQRS.App.CommandResults.Users;
using InfinityCQRS.App.CommandResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityCQRS.App.Commands.User
{
    public class DeleteUserCommand : IRequest<ResponseBase<DeleteUserResult>>
    {
        public string Id { get; set; }
    }
}
