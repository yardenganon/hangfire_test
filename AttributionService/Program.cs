using AttributionService;
using Hangfire;
using Hangfire.Console;
using HangFire.Jobs.Abstractions;
using Hangfire.MAMQSqlExtension;
using Hangfire.SqlServer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMAMQSqlServerStorage("Data Source=localhost,1433;Initial Catalog=master;User Id=sa;Password=yarden1234!;Integrated Security=False;MultiSubnetFailover=True;MultipleActiveResultSets=True;TrustServerCertificate=True",
            new SqlServerStorageOptions(),
            queues: new []{ Queues.AttributionQueue })
        .UseConsole());//.UseConsoleLogger();



builder.Services.AddHangfireServer(options => options.Queues = new[] { Queues.AttributionQueue });
builder.Services.AddTransient<ISomeAttributionJob, SomeAttributionJob>();

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

RecurringJob.AddOrUpdate<ISomeAttributionJob>(nameof(SomeAttributionJob), r => r.Execute(null), Cron.Minutely, queue: Queues.AttributionQueue);


app.Run();

