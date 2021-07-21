using System;
using kasic.Kasic;
using kasic.Memory;
using OperationResult;

namespace kasic.Files
{
    public class Reader
    {
        public static readonly string EOF = "eof";
        public string Path { get; private set; }
        private string[] lines;
        public int LineNumber { get; private set; }

        public Reader(string path)
        {
            Path = path;
            LineNumber = 0;
            lines = System.IO.File.ReadAllLines(path);
            BuildScopes();
        }

        public Result<string, KasicError> ReadLine()
        {
            while (LineNumber < lines.Length)
            {
                if (ValidLine(lines[LineNumber]))
                {
                    return Helpers.Ok(lines[LineNumber++]);
                }

                LineNumber++;
            }

            return Helpers.Ok(EOF);
        }

        public bool MovePointer(int lineNumber)
        {
            if (lineNumber < lines.Length && lineNumber >= 0)
            {
                LineNumber = lineNumber;
                return true;
            }

            return false;
        }

        private void BuildScopes()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith(">"))
                {
                    var scopeName = line.Substring(1).Trim().Split(" ")[0];
                    Scope.RegisterGotoScope(scopeName, i);
                }
            }
        }

        private bool ValidLine(string line)
        {
            return !(line.StartsWith(">") || line.StartsWith("#") || String.IsNullOrEmpty(line));
        }
        
    }
}