using System;
using System.Collections.Generic;
using System.Text;
using TestRailHelperStarShip.API;
using TestRailHelperStarShip.Enums;

namespace TestRailHelperStarShip.Models
{
    public class TestCase
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public TestResult TestResult { get; set; }
        public long RunId { get; set; }

        public string ToStringForLogging()
        {
            return FormatDataToStringForLogging();
        }
        private string FormatDataToStringForLogging()
        {
            var testIdStr = $"TestCaseID: {StringUtil.CompleteStringWitnSpasesOrTruncate(Id.ToString(), 5)}";
            var testResultStr = $"Result: {StringUtil.CompleteStringWitnSpasesOrTruncate(TestResult.ToString(), 10)}";
            var testNameStr = $"TestName: {StringUtil.CompleteStringWitnSpasesOrTruncate(Name, 150)}";
            var commonOutput = $"{testIdStr}| {testResultStr}| {testNameStr}";
            return commonOutput;
        }
    }
}
