using System.IO;

namespace CompareTool
{
    public static class Extentions
    {
        public static bool IsFileExist(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                return true;
            }

            return false;
        }

        public static bool IsDirectoryExist(string directory)
        {
            if (Directory.Exists(directory))
            {
                return true;
            }

            return false;         
        }
    }
}