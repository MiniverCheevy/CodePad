using ClientGenerator.Models;

namespace ClientGenerator.Helpers
{
    public class FileWriter
    {
        private string rootPath;

        public FileWriter(string rootPath)
        {
            this.rootPath = rootPath;
        }
        public void WriteFiles(params CodeFile[] files)
        {
            foreach (var file in files)
            {
                var path = Path.Combine(rootPath, file.Folder, file.FileName);
                if (File.Exists(path))
                {
                    if (file.GenerateOnce)
                        continue;

                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);
                }
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, file.ToString());
            }
        }
    }
}
