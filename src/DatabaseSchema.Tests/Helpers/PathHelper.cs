using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DatabaseSchema.Tests.Helpers
{
    internal static class PathHelper
    {
        public static string GetFullPath(string filename)
        {
            return Regex.IsMatch(filename, @"\w:\\")
                ? filename
                : Path.Combine(
                    (Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) ?? "").Replace("file:\\", ""),
                    filename);
        }
    }
}
