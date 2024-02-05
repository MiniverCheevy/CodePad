using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Models
{
    public abstract class CodeFile
    {
        abstract public string FileName { get; }
        abstract public string Folder { get; }
        abstract public bool GenerateOnce { get; }

    }
}
