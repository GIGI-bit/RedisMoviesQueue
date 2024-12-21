using Microsoft.Extensions.Options;
using RedisMoviesQueue.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
    string connectionString = builder.Configuration.GetValue<string>("AzureStorage:ConnectionString");
    string queueName = "mynewqueue"; 
builder.Services.AddScoped<IQueueService, QueueService>(serviceProvider =>
{
    return new QueueService(connectionString, queueName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
