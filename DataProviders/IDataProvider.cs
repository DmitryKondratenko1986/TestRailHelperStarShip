using System.Collections.Generic;

namespace TestRailHelperStarShip.DataProviders
{
    public interface IDataProvider
    {
        T GetValue<T>(string path);
        IReadOnlyList<T> GetValueList<T>(string path);
        IReadOnlyDictionary<string, T> GetValueDictionary<T>(string path);
        bool IsValuePresent(string path);
    }
}
