using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers
{
    public class ApiControllerBase : Controller
    {
        private readonly IMapper _mapper;

        public ApiControllerBase(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public IMapper Mapper { get { return _mapper; } }
    }
}
