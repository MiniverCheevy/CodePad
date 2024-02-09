using CodePad.Server.Features.Templates;
using CodePad.Server.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class SortedDistinctList
{
    public class Command : IRequest<Result>
    {
        public string Text { get; set; }
    }

    public class Result : Reply
    {
        public string Text { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Result { Text = StringUtilities.Clean(request.Text) });
        }
    }
}

[Route("api/FormatTemplates/SortedDistinctList")]
[ApiController]
public class SortedDistinctListController : ControllerBase
{
    private readonly IMediator _mediator;

    public SortedDistinctListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<SortedDistinctList.Result> Clean([FromBody] SortedDistinctList.Command command)
    {
        return _mediator.Send(command);
    }
}
