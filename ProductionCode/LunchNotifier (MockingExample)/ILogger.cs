using System;

namespace ProductionCode.MockingExample
{
    public interface ILogger
    {
        void Write(string s);
        void Debug(string s);
        void Error(Exception exception);
    }
}