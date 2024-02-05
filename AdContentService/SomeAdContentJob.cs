using Hangfire;
using Hangfire.Console;
using HangFire.Jobs.Abstractions;
using Hangfire.MAMQSqlExtension;
using Hangfire.Server;

namespace AdContentService;

public class SomeAdContentJob : ISomeAdContentJob
{
    [Queue(Queues.AdContentQueue)]
    [RetryInQueue(Queues.AdContentQueue)]
    [DisableConcurrentExecution(10)]
    public async Task Execute(PerformContext? context, SomeState state)
    {
        var numbers = new [] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
        
        var bar = context.WriteProgressBar();
        
        context.WriteLine($"SomeAdContentJob, Id: {context!.BackgroundJob.Id} has started");

        foreach(var num in numbers.WithProgress(bar))
        {
            context.WriteLine($"{num}");
            await Task.Delay(1000);
        }

        context.WriteLine($"SomeAdContentJob, Id: {context!.BackgroundJob.Id} has finished");

        return;
    }

    public static SomeState GetDefaults => new SomeState
    {
        From = DateTime.Now,
        To = default,
        Name = "SomeAdContentJob",
        Id = Guid.NewGuid()
    };
}