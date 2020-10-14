using System.IO;

namespace JSONdata
{
    public static class FileReader
    {
        public static string ReadToString(string path)
        {
            var sr = new StreamReader(path);
            return sr.ReadToEnd();
        }
    }
}