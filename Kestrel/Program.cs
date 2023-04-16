using Kestrel.Abp.Server;
using Kestrel.Abp.Server.Commands;
using Kestrel.Abp.Server.Kestrel;
using Microsoft.AspNetCore.Connections;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel((context, options) =>
{
    var serverOptions = context.Configuration.GetSection("ServerOptions").Get<ServerOptions>()!;

    foreach (var listeners in serverOptions.Listeners)
    {
        options.Listen(listeners.GetListenEndPoint(), listenOptions =>
        {
            listenOptions.UseConnectionHandler<KestrelChannelCreator>();
        });
    }
});

builder.Host.AsSuperSocketHostBuilder<RpcPackageBase, RpcPipeLineFilter>()
    .UseHostedService<RpcServer>()
    .UseSession<RpcSession>()
    .UsePackageDecoder<RpcPackageDecoder>()
    .UseCommand(options => options.AddCommandAssembly(typeof(Login).Assembly))
    .UseClearIdleSession()
    .UseInProcSessionContainer()
    .UseChannelCreatorFactory<KestrelChannelCreatorFactory>()
    .AsMinimalApiHostBuilder()
    .ConfigureHostBuilder();

builder.Services.AddLogging();
builder.Services.AddSingleton<KestrelChannelCreator>();
builder.Services.AddSingleton<IPackageEncoder<RpcPackageBase>, RpcPackageEncode>();

var app = builder.Build();

await app.RunAsync();
