namespace CodePad.Server.Features.Templates.FixedWidthFiles;

using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Parser
{
    private readonly FixedWidthFile fixedWidthFile;
    private List<string> lines;
    private List<List<string>> columns;

    public Parser(FixedWidthFile fixedWidthFile)
    {
        this.fixedWidthFile = fixedWidthFile;
        LoadFile();
        SplitIntoColumns();
    }

    private void LoadFile()
    {
        if (!File.Exists(fixedWidthFile.SourcePath))
        {
            throw new FileNotFoundException($"File not found: {fixedWidthFile.FilePath}");
        }

        lines = File.ReadAllLines(fixedWidthFile.SourcePath).ToList();
    }

    private void SplitIntoColumns()
    {

        columns = new List<List<string>>();

        foreach (var line in lines)
        {
            var columns = new List<string>();
            var position = 0;

            foreach (var schema in fixedWidthFile.Schema)
            {
                var value = new String(line.ToArray().Skip(position).Take(schema.Width).ToArray());
                if (!string.IsNullOrWhiteSpace(schema.Format))
                {
                    value = GetFormattedValue(value, schema.Format);
                }
                columns.Add(value);

                position += schema.Width;
            }

            this.columns.Add(columns);
        }
    }

    private string GetFormattedValue(string value, string format)
    {
        string[] dateTimeFormatSpecifiers = new string[] { "d", "M", "y", "h", "m", "s" };
        string[] numericFormatSpecifiers = new string[] { "f", "F", "e", "E", "g", "G", "n", "N", "p", "P", "r", "R", "x", "X" };


        var formatCharacters = format.ToArray().Select(c => c.ToString()).ToArray();
        if (formatCharacters.Intersect(dateTimeFormatSpecifiers).Any())
        {
            return CoerceValueToDateAndThenFormat(value, format);
        }
        else if (numericFormatSpecifiers.Contains(c.ToString()))
        {
            return "Numeric";
        }
    }

    private string CoerceValueToDateAndThenFormat(string value, string format)
    {
        EntryPointNotFoundException{ }
    }

    public List<List<string>> GetColumns()
    {
        return columns;
    }
}
