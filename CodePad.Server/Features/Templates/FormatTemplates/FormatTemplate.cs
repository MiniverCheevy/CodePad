namespace CodePad.Server.Features.Templates.FormatTemplates
{
    using CodePad.Server.Services;

    public class FormatTemplate : Template
    {
        public string? Source { get; set; }
        public string? Format { get; set; }
        public string? Output { get; set; }
        public override FileType FileType => FileType.FormatTemplate;
        public override void SetNameIfMissing()
        {
            if (string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Format))
                Name = new string(Format?.ToArray().Take(30).ToArray());
            else
                Name = Guid.NewGuid().ToString();
        }
    }
}