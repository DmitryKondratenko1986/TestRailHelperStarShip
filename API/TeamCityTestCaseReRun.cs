using System;
using System.Collections.Generic;
using System.Text;
using TestRailHelperStarShip.Models;

namespace TestRailHelperStarShip.API
{
    public class TeamCityTestCaseReRun
    {   
        public TeamCityTestCaseReRun(IList<TestCase> testCases)
        {
            this.testCases = testCases;
        }
        private IList<TestCase> testCases = new List<TestCase>();

        public string GetTestCasesStringForTeamCity()
        {
            var builder = new StringBuilder();
            var count = testCases.Count;
            if (count == 0)
            {
                return "";
            }
            builder.Append("\"");
            for (int i = 0; i < count; i++)
            {
                builder.Append($"cat == {testCases[i].Id}");
                if (i < count - 1)
                {
                    builder.Append(" || ");
                }
            }
            builder.Append("\"");
            return builder.ToString();
        }

        public string GetTestCasesInfo()
        {
            var builder = new StringBuilder();
            foreach (var test in testCases)
            {
                builder.Append(test.ToStringForLogging() + "\n");
            }
            return builder.ToString();
        }
    }
}
