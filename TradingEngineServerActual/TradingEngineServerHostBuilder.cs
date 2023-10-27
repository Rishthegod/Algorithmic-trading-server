using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging;

namespace TradingEngineServer.Core
{
	public sealed class TradingEngineServerHostBuilder
	{
		public static IHost BuildTradingEngineServer()
			=> Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
			{
				//Start with configuration dependency inject from appsettings.json

				services.AddOptions();
				services.Configure<TradingEngineServerConfiguration>(context.Configuration.GetSection(nameof(TradingEngineServerConfiguration))); //this configure method makes it so that you don't have to write unneccsary code to read and write files such as appsettings.json, this will read it for you 

				//Add our singleton objects
				services.AddSingleton<ITradingEngineServer, TradingEngineServer>();
				services.AddSingleton<ITextLogger, TextLogger>(); //need to add singleton object ot dependency inject from our LoggingCS project


				// Add hosted service
				services.AddHostedService<TradingEngineServer>();

			}).Build();

	}
}

