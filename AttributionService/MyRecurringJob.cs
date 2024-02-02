// using Hangfire.Console.LogExtension;
// using Hangfire.Server;


// public abstract class JobBase<TJobState, TResultValue>() 
// where TJobState : JobState
// {
//     protected Guid Id => Guid.NewGuid();
//     protected IJobStateProvider<TJobState> _provider;
//     protected TJobState _state;
//     public abstract Task<JobResult<TResultValue>> Execute(PerformContext? context = null);
// }


// public class SyncAdsJob : JobBase<SyncAdsJobState, bool>
// {
//     private readonly SyncAdsJobState _state;

//     private readonly IHangfireLogger<SyncAdsJob> _logger;

//     public SyncAdsJob(SyncAdsJobState state, IHangfireLogger<SyncAdsJob> logger)
//     {
//         _state = state;
//         _logger = logger;
//     }

//     public override async Task<JobResult<bool>> Execute(PerformContext? context = null)
//     {
//         _logger.SetPerformContext(context);

//         _logger.LogInformation($"{context.JobId}, {_state.From}, {_state.To}");

//         await Task.Delay(5_000);

//         _logger.LogInformation($"Job finised {context.JobId}");

//         return JobResult<bool>.Create(true);
//     }
// }



// public class JobResult<T>
// {
//     public T Value {get; set;}
//     private JobResult(T value)
//     {
//         Value = value;
//     }
//     public static JobResult<T> Create(T value) => new(value);
// }


// public class SyncAdsJobRequest
// {
//     public int[] Providers {get;set;}
//     public int[] Campaigns {get;set;}
// }

// public interface IJobStateProvider<T> where T : JobState
// {
//     T GetJobState();
// }

// public class JobState 
// {
//     public Guid Id => Guid.NewGuid();
// }

// public class SyncAdsJobState : JobState
// {
//     public DateTime From {get;set;}
//     public DateTime To {get;set;}
// }

// public class SyncAdsJobStateProvider : IJobStateProvider<SyncAdsJobState>
// {
//     private readonly ITimeProvider _timeProvider;
//     public SyncAdsJobStateProvider(ITimeProvider timeProvider)
//     {
//         _timeProvider = timeProvider;
//     }

//     public SyncAdsJobState GetJobState() => new SyncAdsJobState {
//             From = _timeProvider.GetTime.AddDays(-2),
//             To = _timeProvider.GetTime
//     };

// }










// public interface ITimeProvider
// {
//     public DateTime GetTime {get;}
// }

// public class TimeProvider : ITimeProvider
// {
//     public DateTime GetTime => DateTime.UtcNow;
// }

using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

public class SomeAttributionJob
{
    [Queue("attribution")]
    public async Task Excecute(PerformContext? context)
    {
        int[] numbers = new int[100];
        var bar = context.WriteProgressBar();
        
        context.WriteLine($"SomeAttributionJob, Id: {context!.BackgroundJob.Id} has started");

        foreach(var num in numbers.WithProgress(bar))
        {
            await Task.Delay(500);
            context.WriteLine($"{num}");
        }

        context.WriteLine($"SomeAttributionJob, Id: {context!.BackgroundJob.Id} has finished");

        return;
    }
}