using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

public class SomeAdContentJob
{
    [Queue("ad-content")]
    public async Task Excecute(PerformContext? context)
    {
        int[] numbers = new int[100];
        var bar = context.WriteProgressBar();
        
        context.WriteLine($"SomeAdContentJob, Id: {context!.BackgroundJob.Id} has started");

        foreach(var num in numbers.WithProgress(bar))
        {
            await Task.Delay(500);
        }

        context.WriteLine($"SomeAdContentJob, Id: {context!.BackgroundJob.Id} has finished");

        return;
    }
}