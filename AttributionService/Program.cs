using Hangfire;
using Hangfire.Console;
using Hangfire.Console.LogExtension;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMemoryStorage()
        .UseConsole());//.UseConsoleLogger();

builder.Services.AddHangfireServer(options => options.Queues = ["ad-content"]);

// builder.Services.AddSingleton<IJobStateProvider<SyncAdsJobState>, SyncAdsJobStateProvider>();

// builder.Services.AddTransient<JobBase<SyncAdsJobState, bool>, SyncAdsJob>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseHangfireDashboard();
app.MapHangfireDashboard();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<Job1>(nameof(Job1), r => r.Excecute(null), Cron.Minutely);

app.Run();

