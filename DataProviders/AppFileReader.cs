using System;
using System.IO;

namespace TestRailHelperStarShip.DataProviders
{
    public class ApplicationFileReader
    {
        private readonly string resourcesFolder;
        public ApplicationFileReader(string recourcesFolderName)
        {
            resourcesFolder = recourcesFolderName;
        }
        public string GetTextFromResource(string fileName)
        {
            return GetTextFromFile(GetResourceFile(fileName));
        }
        public string GetTextFromFile(FileInfo fileInfo)
        {
            using (var reader = fileInfo.OpenText())
            {
                return reader.ReadToEnd();
            }
        }
        private FileInfo GetResourceFile(string fileName)
        {
            return new FileInfo(Path.Combine(AppContext.BaseDirectory, resourcesFolder, fileName));
        }
    }
}
