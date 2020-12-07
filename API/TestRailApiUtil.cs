using Final_Task.Utils.Api.TestRailApi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestRailHelperStarShip.DataProviders;
using TestRailHelperStarShip.Enums;
using TestRailHelperStarShip.Models;

namespace TestRailHelperStarShip.API
{
    public class TestRailApiUtil
    {
        private static string ApiPartialUrl => DataProviderUtil.GetConfigData<string>("test_rail.api_basic_url");
        private static string Login => DataProviderUtil.GetConfigData<string>("test_rail.credentials.login");
        private static string Password => DataProviderUtil.GetConfigData<string>("test_rail.credentials.password");

        public static async Task<IList<Dictionary<string, object>>> GetTestsReturnAllTestsInfoAsync(long testRunId)
        {
            var response = await GetTestsReturnRawResponseAsync(testRunId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseDictCollection = ThrowIfResponseCantParseOrReturnResponseDictCollection(response);
                return responseDictCollection;
            }
            var message = $"Error occurs in GetTestsReturnAllTestsInfoAsync method because response status was: {response.StatusCode}";
            throw new TestRailApiException(message);
        }
        private static async Task<HttpResponseResult> GetTestsReturnRawResponseAsync(long testRunId)
        {
            var fullUri = $"{ApiPartialUrl}/get_tests/{testRunId}";
            var request = HttpUtil.CreateHttpGetRequest(fullUri, null, WebContentTypes.ApplicationJsonType, Login, Password);
            var response = await HttpUtil.GetApiDataAsync(request);
            return response;
        }
        private static IList<Dictionary<string, object>> ThrowIfResponseCantParseOrReturnResponseDictCollection(HttpResponseResult result)
        {
            try
            {
                return result.GetApiData(JsonConvert.DeserializeObject<IList<Dictionary<string, object>>>);
            }
            catch (Exception ex)
            {
                var exception = new TestRailApiException($"Error occurs when parse response when try to get data Dictionary<string, object>", ex);
                throw exception;
            }
        }

        public static IList<TestCase> GetTestCases(long testRunId)
        {
            var testCases = new List<TestCase>();
            var rawData = GetTestsReturnAllTestsInfoAsync(testRunId).Result;
            foreach (var record in rawData)
            {
                var testCase = new TestCase()
                {
                    Name = (string)record["title"],
                    Id = (long)record["case_id"],
                    RunId = (long)record["run_id"],
                    TestResult = (TestResult)(long)record["status_id"]
                };
                testCases.Add(testCase);
            }
            return testCases;
        }

        public static IList<TestCase> GetTestCases(long testRunId, TestResult testResult)
        {
            var testCases = GetTestCases(testRunId);
            if (testResult == TestResult.All)
            {
                return testCases;
            }
            var querytestCases = testCases.Where(tc => testResult == tc.TestResult).ToList();
            return querytestCases;
        }

    }
}
