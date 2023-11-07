using AutoMapper;
using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using InfinityCQRS.App.Commands.User;
using InfinityCQRS.App.Repository.Common;
using InfinityCQRS.Backend.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InfinityCQRS.App.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResponseBase<DeleteUserResult>>
    {
        private readonly IBaseRepository<ApplicationUser> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteUserResult> _logger;

        public DeleteUserHandler(IBaseRepository<ApplicationUser> repository,
                                 IMapper mapper,
                                 ILogger<DeleteUserResult> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseBase<DeleteUserResult>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling delete user command");

                var user = await _repository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                user.IsDeleted = true;

                var deletedUser = _mapper.Map<DeleteUserResult>(await _repository.UpdateAsync(user));

                _logger.LogInformation("Finished handling delete user command");

                return new ResponseBase<DeleteUserResult>(deletedUser);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error handling delete user command: {ex.Message}");
                var result = new ResponseBase<DeleteUserResult>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
