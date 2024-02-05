namespace CodePad.Server.Services;

using CodePad.Server.Features.Templates;
using System;
using System.Text.Json;

public enum FileType
{
    FormatTemplate = 1,
    FixedWidthSchema = 2,
    DelimitedSchema = 3,
}

public interface IFileManagementService
{
    T ReadFile<T>(FileType type, string name, string projectName = "Default");
    List<FileRow> ListFiles(FileType fileType, string projectName = "Default");
    void WriteFile<T>(T file) where T : Template;
}
public class FileManagerService : IFileManagementService
{
    private string? rootPath;

    public FileManagerService()
    {
        ChooseRootPath();
    }
    public T ReadFile<T>(FileType type, string name, string projectName = "Default")
    {
        var path = GetRootPathFromFileType(type, projectName);
        var json = File.ReadAllText(Path.Combine(path, name));
        return JsonSerializer.Deserialize<T>(json);
    }
    public List<FileRow> ListFiles(FileType fileType, string projectName = "Default")
    {
        var files = new List<FileRow>();
        var path = GetRootPathFromFileType(fileType, projectName);
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        files = Directory.GetFiles(path).ToList().Select(c => new FileRow { Path = c, Name = Path.GetFileName(c), FileType = fileType }).ToList();
        foreach (var file in files)
        {
            DateTime creationTime = File.GetCreationTime(file.Path);
            DateTime modifiedTime = File.GetLastWriteTime(file.Path);
            file.CreateDate = creationTime;
            file.LastModified = modifiedTime;

        }
        return files;
    }
    public void WriteFile<T>(T file)
        where T : Template
    {
        var type = file.FileType;
        var path = GetPathFromFileType(file);
        var json = JsonSerializer.Serialize(file);
        File.WriteAllText(Path.Combine(path, file.Name), json);
    }

    private string GetRootPathFromFileType(FileType type, string projectName)
    {
        var path = Path.Combine(rootPath, RemoveInvalidCharacters(projectName));
        string folder = type.ToString();
        return Path.Combine(path, folder);
    }

    private string GetPathFromFileType(Template template)
    {
        var projectName = string.IsNullOrEmpty(template.ProjectName) ? "Default" : template.ProjectName;
        var path = Path.Combine(rootPath, RemoveInvalidCharacters(projectName));

        string folder = template.FileType.ToString();
        return Path.Combine(path, folder);
    }

    private void ChooseRootPath()
    {
        if (CanWriteLocally())
        {
            rootPath = Environment.CurrentDirectory;
            if (rootPath.IndexOf("bin") > 0)
            {
                rootPath = rootPath.Substring(0, rootPath.IndexOf("bin"));
            }
        }
        else
        {
            rootPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        rootPath = Path.Combine(rootPath, "CodePad");

    }
    public static string RemoveInvalidCharacters(string input)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c.ToString(), string.Empty);
        }
        input = input.Replace(" ", string.Empty);
        return input;
    }
    private bool CanWriteLocally()
    {
        try
        {
            File.WriteAllText("test.txt", "test");
            File.Delete("test.txt");
            return true;
        }
        catch
        {
            return false;
        }
    }
}
