using NLog;
using System;
using TestRailHelperStarShip.API;
using TestRailHelperStarShip.DataProviders;
using TestRailHelperStarShip.Enums;

namespace TestRailHelperStarShip
{
    class Program
    {
        static void Main(string[] args)
        {
            var testRunId = DataProviderUtil.GetConfigData<long>("test_rail.test_Run_id");
            var testResultType = StringUtil.GetEnumFromStr<TestResult>(DataProviderUtil.GetConfigData<string>("test_rail.test_result"));

            var testCases = TestRailApiUtil.GetTestCases(testRunId, testResultType);

            var teamCityResult = new TeamCityTestCaseReRun(testCases);
            
            var testInfo = teamCityResult.GetTestCasesInfo();
            var reRunStr = teamCityResult.GetTestCasesStringForTeamCity();
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info(testInfo);
            logger.Info(reRunStr);
        }
    }
}
