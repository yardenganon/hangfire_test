using Hangfire;
using Hangfire.Console;
using HangFire.Jobs.Abstractions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage("Data Source=localhost,1433;Initial Catalog=master;User Id=sa;Password=yarden1234!;Integrated Security=False;MultiSubnetFailover=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
        .UseConsole());//.UseConsoleLogger();


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

app.MapGet("/RunSomeAdContentJob", ([AsParameters] SomeState state) => {
    
    var jobId = BackgroundJob.Enqueue<ISomeAdContentJob>(Queues.AdContentQueue, x => x.Execute(null, state));
    return $"Job Enqueued {jobId}";
    
}).WithName("RunSomeAdContentJob");

app.MapGet("/RunSomeAttributionJob", () => {
    
    var jobId = BackgroundJob.Enqueue<ISomeAttributionJob>(Queues.AttributionQueue, x => x.Execute(null));
    return $"Job Enqueued {jobId}";
    
}).WithName("RunSomeAttributionJob");

app.Run();

