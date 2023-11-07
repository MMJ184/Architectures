//using AutoMapper;
//using MediatR;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using InfinityCQRS.App.CommandResults;

//namespace InfinityCQRS.App.Handlers
//{
//    public abstract class HandlerBase<TRequest, TResponse, THandler> : IRequestHandler<TRequest, TResponse>, IMap
//        where TRequest : IRequest<TResponse>
//        where THandler : HandlerBase<TRequest, TResponse, THandler>
//    {
//        private readonly IMapper _mapper;
//        private readonly ILogger<THandler> _logger;
//        protected HandlerBase(IMapper mapper, ILogger<THandler> logger)
//        => (_mapper, MapperConfiguration, _logger) = (mapper, mapper.ConfigurationProvider, logger);

//        protected IConfigurationProvider MapperConfiguration { get; }

//        public TDest Map<TDest>(object source) => _mapper.Map<TDest>(source);

//        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("Handling Request");
//            var result = OnHandleRequest(request, cancellationToken);
//            _logger.LogInformation("Finished Handling Request");
//            return result;
//        }

//        /// <summary>
//        /// Generate Result
//        /// </summary>
//        /// <typeparam name="TResponse">Type of Response Model</typeparam>
//        /// <param name="createResponse">Function Creating response</param>
//        /// <returns>Generated Result</returns>
//        protected async Task<ResponseBase<TResponse>> GenerateResult<TResponse>(Func<Task<TResponse>> createResponse)
//        {
//            try
//            {
//                return new ResponseBase<TResponse>(await createResponse());
//            }
//            catch (DbUpdateException ex) when (ex.InnerException is SqlException)
//            {
//                var result = new ResponseBase<TResponse>(default);
//                result.AddExceptionLog(ex);
//                return result;
//            }
//        }

//        /// <summary>
//        /// Generate Result
//        /// </summary>
//        /// <typeparam name="TResponse">Type of Response Model</typeparam>
//        /// <param name="createResponse">Function Creating response</param>
//        /// <returns>Generated Result</returns>
//        protected ResponseBase<TResponse> GenerateResult<TResponse>(Func<TResponse> createResponse)
//        {
//            try
//            {
//                return new ResponseBase<TResponse>(createResponse());
//            }
//            catch (DbUpdateException ex) when (ex.InnerException is SqlException)
//            {
//                var result = new ResponseBase<TResponse>(default);
//                result.AddExceptionLog(ex);
//                return result;
//            }
//        }

//        /// <summary>
//        /// Generate Base Result
//        /// </summary>
//        /// <typeparam name="TResponse">Type of Response Model</typeparam>
//        /// <param name="createResponse">Function Creating response</param>
//        /// <returns>Generated Result</returns>
//        protected async Task<ResponseBaseModel> GenerateBaseResult(Func<Task> createResponse)
//        {
//            try
//            {
//                await createResponse();
//                return new ResponseBaseModel();
//            }
//            catch (DbUpdateException ex) when (ex.InnerException is SqlException)
//            {
//                var result = new ResponseBaseModel();
//                result.AddExceptionLog(ex);
//                return result;
//            }
//        }

//        /// <summary>
//        /// Generate Base Result
//        /// </summary>
//        /// <param name="createResponse">Function Creating response</param>
//        /// <returns>Generated Result</returns>
//        protected ResponseBaseModel GenerateBaseResult(Action createResponse)
//        {
//            try
//            {
//                createResponse();
//                return new ResponseBaseModel();
//            }
//            catch (DbUpdateException ex) when (ex.InnerException is SqlException)
//            {
//                var result = new ResponseBaseModel();
//                result.AddExceptionLog(ex);
//                return result;
//            }
//        }

//        protected abstract Task<TResponse> OnHandleRequest(TRequest request, CancellationToken cancellationToken);
//    }
//}
