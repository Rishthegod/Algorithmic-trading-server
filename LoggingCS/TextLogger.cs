using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Options;
using TradingEngineServer.Logging.LoggingConfiguration;
using System.IO;
using System.Threading.Tasks;

namespace TradingEngineServer.Logging
{

    public class TextLogger : AbstractLogger, ITextLogger
    {
        private LoggerConfiguration _loggingConfiguration;

        public TextLogger(IOptions<LoggerConfiguration> loggingConfiguration) : base()
        {
            _loggingConfiguration = loggingConfiguration.Value ?? throw new ArgumentNullException(nameof(loggingConfiguration));
        }

        private static async Task LogAsync(string filepath, BufferBlock<LogInformation> logQueue, CancellationToken token) 
        {
            using var fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs);
            try
            {
                while (true)
                {
                    var logitem = await logQueue.ReceiveAsync(token).ConfigureAwait(false);
                    string formattedMessage = FormatLogItem(logitem);
                    await sw.WriteAsync(formattedMessage).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {

            }
        }

        protected override void Log(LogLevel logLevel, string module, string message) //Implment abstract class for TextLogger
        {
            _logQueue.Post(new LogInformation(logLevel, module, message, DateTime.Now, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name)); //forward all field values into queue
        }
        public void Dispose() //implement I TextLogger dispose method
        {
            throw new NotImplementedException();
        }
        private readonly BufferBlock<LogInformation> _logQueue = new BufferBlock<LogInformation>(); //creating a log queue
    }
}
