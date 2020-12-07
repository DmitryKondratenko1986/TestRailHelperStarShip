using System;
using System.IO;

namespace TestRailHelperStarShip.DataProviders
{
    public class DataProviderUtil
    {
        private const string TestDataFolder = "TestData";
        private const string TestDataFile = "TestData.json";
        private const string ConfigFolder = "Config";
        private const string ConfigFile = "Config.json";


        public static T GetTestData<T>(string data)
        {
            return GetData<T>(data, TestDataFolder, TestDataFile);
        }
        public static T GetConfigData<T>(string data)
        {
            return GetData<T>(data, ConfigFolder, ConfigFile);
        }
        public static string GetFullPathToFile(string folderName, string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, folderName, fileName);
        }
        private static T GetData<T>(string data, string folderName, string fileName)
        {
            var fileReader = new ApplicationFileReader(folderName);
            var dataProvider = new JsonDataProvider(fileReader.GetTextFromResource(fileName));
            return dataProvider.GetValue<T>($".{data}");
        }
    }
}
