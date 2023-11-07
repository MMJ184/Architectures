using AutoMapper;
using InfinityCQRS.App.CommandResults;
using InfinityCQRS.App.CommandResults.Users;
using InfinityCQRS.App.Commands.User;
using InfinityCQRS.App.Repository.Common;
using InfinityCQRS.Backend.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace InfinityCQRS.App.Handlers.Users
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, ResponseBase<AddUserResult>>
    {
        private readonly IBaseRepository<ApplicationUser> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserHandler> _logger;

        public AddUserHandler(IBaseRepository<ApplicationUser> repository,
                              IMapper mapper,
                              ILogger<AddUserHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseBase<AddUserResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling add user command");

                var userRequest = _mapper.Map<ApplicationUser>(request);
                userRequest.IsDeleted = false;
                userRequest.UserName = request.FirstName;
                userRequest.NormalizedUserName = String.Concat(request.FirstName + request.LastName);
                userRequest.NormalizedEmail = request.Email;
                userRequest.PasswordHash = BC.HashPassword(request.Password);
                userRequest.PhoneNumber = request.Phone;

                var response = _mapper.Map<AddUserResult>(await _repository.AddAsync(userRequest));

                _logger.LogInformation("Finished handling add user command");

                return new ResponseBase<AddUserResult>(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error handling add user command: {ex.Message}");
                var result = new ResponseBase<AddUserResult>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
