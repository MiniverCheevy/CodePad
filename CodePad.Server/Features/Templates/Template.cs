namespace CodePad.Server.Features.Templates
{
    using CodePad.Server.Services;

    public abstract class Template
    {
        public string? Name { get; set; }

        public abstract void SetNameIfMissing();

        public abstract FileType FileType { get; }

        public string? ProjectName { get; set; }
    }
}