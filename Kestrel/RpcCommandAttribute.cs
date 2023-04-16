using SuperSocket.Command;

namespace Kestrel.Abp.Server;

public sealed class RpcCommandAttribute : CommandAttribute
{
    public RpcCommandAttribute(CommandKey key)
    {
        Key = (byte)key;
    }
}
