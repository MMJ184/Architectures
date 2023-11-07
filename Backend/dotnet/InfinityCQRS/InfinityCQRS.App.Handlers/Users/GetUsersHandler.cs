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
    public class GetUsersHandler : IRequestHandler<GetUsersCommand, ResponseBase<List<GetUsersResult>>>
    {
        private readonly IBaseRepository<ApplicationUser> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersResult> _logger;

        public GetUsersHandler(IBaseRepository<ApplicationUser> repository,
                               IMapper mapper,
                               ILogger<GetUsersResult> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseBase<List<GetUsersResult>>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Handling get users command.");

                var users = _repository.GetQueryable().Where(x => x.IsDeleted == false).ToList();

                if (users == null)
                    throw new Exception("Users Not Found.");

                var usersResult = _mapper.Map<List<GetUsersResult>>(users);

                _logger.LogInformation("Finished handling get users command.");

                return new ResponseBase<List<GetUsersResult>>(usersResult);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error handling get users command: {ex.Message}");
                var result = new ResponseBase<List<GetUsersResult>>(null);
                result.AddExceptionLog(ex);
                return result;
            }
        }
    }
}
