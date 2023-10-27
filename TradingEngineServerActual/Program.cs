using System;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Hosting; //importing logging class



using TradingEngineServer.Core; //changing namespace

//top level statement

//TradingEngineserver when this is called will now return a object of type IHost
using var engine = TradingEngineServerHostBuilder.BuildTradingEngineServer(); //we want to get rid of this server once the program terminates thats hwy we use the "using" keyword


TradingEngineServerServiceProvider.ServiceProvider = engine.Services; //makes all the services we configured in service host builder publicly
                                                                      //accesible

{
    using var scope = TradingEngineServerServiceProvider.ServiceProvider.CreateScope(); //creates a scope in which scoped services will exit is
    await engine.RunAsync().ConfigureAwait(false);
    //runAsync takes a default cancellationtoken to stop the program if it takes too long, but c# takes care of it for us.
        //asynchronous programming


}