using Hangfire.Server;

namespace HangFire.Jobs.Abstractions;

public interface ISomeAttributionJob
{
    Task Execute(PerformContext? context);
}