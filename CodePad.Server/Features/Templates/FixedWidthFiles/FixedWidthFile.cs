namespace CodePad.Server.Features.Templates.FixedWidthFiles;

using CodePad.Server.Services;

public class FixedWidthFile : Template
{
    public string SourcePath { get; set; }
    public List<RowSchema> Schema { get; set; } = new();
    public override FileType FileType => FileType.FixedWidthSchema;

    public override void SetNameIfMissing()
    {
        throw new NotImplementedException();
    }
}

public class RowSchema
{
    public int Width { get; set; }
    public string? Format { get; set; }
}