using CodePad.Server.Features.Templates;
using CodePad.Server.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

public class Format
{
    public class Command : IRequest<Result>
    {
        public string Format { get; set; }
        public string Source { get; set; }
    }

    public class Result : Reply<string?>
    {
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var lines = request.Source?.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var builder = new StringBuilder();

            var rowNumber = 0;
            foreach (var line in lines)
            {

                var tempFormat = request.Format;

                tempFormat = tempFormat.Replace("{#}", rowNumber.ToString());
                tempFormat = tempFormat.Replace("{+}", (rowNumber + 1).ToString());
                var row = line.Split(',');
                var formatter =
                    new LineFormatter(new LineFormattingRequest
                    {
                        FormatString = tempFormat,
                        Arguments =
                            row.Select(t => (t?.ToString() ?? string.Empty).Trim())
                                .Cast<object>()
                                .ToArray()
                    });

                builder.AppendLine(formatter.Execute());
                rowNumber++;
            }

            return Task.FromResult(new Result { Data = builder.ToString() });
        }
    }
}
[Route("api/FormatTemplates/format")]
[ApiController]
public class FormatController : ControllerBase
{
    private readonly IMediator _mediator;

    public FormatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Format.Result> Format([FromBody] Format.Command command)
    {
        return _mediator.Send(command);
    }
}
