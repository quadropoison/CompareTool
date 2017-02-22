using System.IO;

namespace CompareTool
{
    public class Extentions
    {
        public bool IsFileExist(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                return true;
            }

            return false;
        }
    }
}