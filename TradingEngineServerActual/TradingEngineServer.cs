using TradingEngineServer.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging; //added a refereence to our LoggingCS

namespace TradingEngineServer.Core
{
    sealed class TradingEngineServer : BackgroundService, ITradingEngineServer //sealed makes sure that nobody can override this class or inherit from this
    {
        private readonly ITextLogger _logger; //rather than I Logger we are now going to use the Loggin Library we are creating in LoggingCS
        private readonly TradingEngineServerConfiguration _tradingEngineServerConfig;


        public TradingEngineServer(ITextLogger textLogger,IOptions<TradingEngineServerConfiguration> config) //dependency injecting Trading engine server configuration into the server, regardless of whther the confirguration is null or not 
        {
            _logger = textLogger ?? throw new ArgumentNullException(nameof(textLogger)); //throw Nullexception if private logger class is null
            _tradingEngineServerConfig = config.Value ?? throw new ArgumentException(nameof(config)); //using the null coalescing operator we can throw a null excdeption if the configuration is null otherwise use the value if not null

        }

        public Task Run(CancellationToken token) => ExecuteAsync(token); //using this Task method we made ExecuteAsync a public method for us to call later

        protected override Task ExecuteAsync(CancellationToken stoppingToken) //override protected methong ExecuteAsync from BackgroundService
        {
            _logger.LogInformation($"Starting {nameof(TradingEngineServer)}");

            while (!stoppingToken.IsCancellationRequested) //keep running the server until cancellation is requested
            {
              

            }

            _logger.LogInformation($"Stopped {nameof(TradingEngineServer)}");
            return Task.CompletedTask;

        }
    }
}

