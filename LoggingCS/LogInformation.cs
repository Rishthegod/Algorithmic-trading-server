using System;
namespace TradingEngineServer.Logging
{ //we are going to be using a record type in C# to creat an immutable object type
    public record LogInformation(LogLevel LogLevel, string Module, string Message, DateTime Now, int ThreadId, string ThreadName);
}
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }; //circumventing Visual studio bug regarding records
}
