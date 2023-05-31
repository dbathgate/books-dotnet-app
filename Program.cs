using Microsoft.EntityFrameworkCore;
using RazorApp.Repository;
using Steeltoe.Extensions.Configuration.Kubernetes.ServiceBinding;
using Steeltoe.Connector.MySql.EFCore;
using Steeltoe.Management.TaskCore;
using Steeltoe.Common.Hosting;
using Steeltoe.Connector.EFCore;
using Steeltoe.Connector.MySql;
using Steeltoe.Common.Reflection;
using Steeltoe.Connector;
using Steeltoe.Connector.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddKubernetesServiceBindings();
builder.UseCloudHosting();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BookDbContext>(options => options.UseMySql(builder.Configuration));
builder.Services.AddTask<MigrateDbContextTask<BookDbContext>>(ServiceLifetime.Transient);

foreach(var config in builder.Configuration.AsEnumerable()) {
    Console.WriteLine($"{config.Key} = {config.Value}");
}

var info = builder.Configuration.GetSingletonServiceInfo<MySqlServiceInfo>();
var mySqlConnection = ReflectionHelpers.FindType(MySqlTypeLocator.Assemblies, MySqlTypeLocator.ConnectionTypeNames); 
var mySqlConfig = new MySqlProviderConnectorOptions(builder.Configuration);
var factory = new MySqlProviderConnectorFactory(info, mySqlConfig, mySqlConnection);


Console.WriteLine($"Connection String: {factory.CreateConnectionString()}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.RunWithTasks();
