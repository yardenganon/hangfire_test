using Hangfire.Server;

namespace HangFire.Jobs.Abstractions;


public interface ISomeAdContentJob
{
    Task Execute(PerformContext? context, SomeState state);
}

public class SomeState
{
    public Guid Id { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Name { get; set; }
}