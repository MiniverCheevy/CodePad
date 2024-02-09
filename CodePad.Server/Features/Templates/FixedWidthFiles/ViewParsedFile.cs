using CodePad.Server.Features.Templates.FixedWidthFiles;
using CodePad.Server.Models;
using MediatR;

public class ViewParsedFile
{
    public class Command : IRequest<Result>
    {
        public FixedWidthFile FixedWidthFile { get; set; }
    }

    public class Result : Reply
    {
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            // TODO: Implement the handler logic here
            throw new NotImplementedException();
        }
    }
}
