namespace CodePad.Server.Features.Templates
{
    using CodePad.Server.Services;

    public class FileRow
    {
        public FileType FileType { get; set; }
        public string? Name { get; set; }

        public string? Path { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}