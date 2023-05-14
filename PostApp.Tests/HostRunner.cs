using Microsoft.Extensions.Hosting;

namespace PostApp.Tests;

public class HostRunner: IAsyncDisposable
{
    private Task _runTask;
    private CancellationTokenSource _cts = new();
    
    public HostRunner(IHost host)
    {
        _runTask = host.RunAsync(_cts.Token);
    }
    
    public async ValueTask DisposeAsync()
    {
        _cts.Cancel();
        await _runTask;
    }
}