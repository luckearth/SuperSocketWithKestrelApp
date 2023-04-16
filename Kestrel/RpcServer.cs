using Microsoft.Extensions.Options;
using SuperSocket.Server;
using SuperSocket;

namespace Kestrel.Abp.Server;

public sealed class RpcServer : SuperSocketService<RpcPackageBase>
{
    public RpcServer(IServiceProvider serviceProvider, IOptions<ServerOptions> serverOptions) : base(serviceProvider, serverOptions)
    {
    }
}
