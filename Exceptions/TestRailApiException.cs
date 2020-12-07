using System;

namespace Final_Task.Utils.Api.TestRailApi.Exceptions
{
    public class TestRailApiException : Exception
    {
        public TestRailApiException(string message) : base(message)
        {
        }
        public TestRailApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
