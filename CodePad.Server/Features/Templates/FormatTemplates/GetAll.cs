using CodePad.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CodePad.Server.Features.Templates.FormatTemplates
{
    public class GetAll
    {
        public class Query : IRequest<Result>
        {
        }

        public class Result
        {
            public List<FileRow> FileRows { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Result>
        {
            private readonly IFileManagementService _fileManagementService;

            public QueryHandler(IFileManagementService fileManagementService)
            {
                _fileManagementService = fileManagementService;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var fileRows = _fileManagementService.ListFiles(FileType.FormatTemplate);
                return new Result { FileRows = fileRows };
            }
        }
    }

    [Route("api/FormatTemplates")]
    [ApiController]
    public class FormatTemplateGetAllController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormatTemplateGetAllController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetAll.Result> GetAll([FromQuery] GetAll.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}
