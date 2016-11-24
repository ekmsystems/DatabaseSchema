using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DatabaseSchema.OleDb.Tests.Helpers
{
    internal static class PathHelper
    {
        public static string GetFullPath(string filename)
        {
            if (Regex.IsMatch(filename, @"\w:\\"))
                return filename;
            var dir = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) ?? "").Replace("file:\\", "");

            return Path.Combine(dir, filename);
        }
    }
}
