using AutoMapper;
using Azure;
using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using InfinityCQRS.App.Commands.User;
using InfinityCQRS.App.Repository.Common;
using InfinityCQRS.Backend.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InfinityCQRS.App.Handlers.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResponseBase<UpdateUserResult>>
    {
        private readonly IBaseRepository<ApplicationUser> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserResult> _logger;

        public UpdateUserHandler(IBaseRepository<ApplicationUser> repository,
                                 IMapper mapper,
                                 ILogger<UpdateUserResult> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseBase<UpdateUserResult>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling update user command");

                var user = await _repository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.UserName = request.FirstName;
                user.NormalizedUserName = String.Concat(request.FirstName + request.LastName);
                user.PhoneNumber = request.Phone;
                user.Language = request.Language;                

                var updatedUser = _mapper.Map<UpdateUserResult>(await _repository.UpdateAsync(user));

                _logger.LogInformation("Finished handling update user command");

                return new ResponseBase<UpdateUserResult>(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error handling update user command: {ex.Message}");
                var result = new ResponseBase<UpdateUserResult>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
