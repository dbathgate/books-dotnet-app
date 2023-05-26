using Microsoft.EntityFrameworkCore;
using RazorApp.Repository;
using Steeltoe.Connector.EFCore;
using Steeltoe.Extensions.Configuration.Kubernetes;
using Steeltoe.Connector.MySql.EFCore;
using Steeltoe.Management.TaskCore;
using Steeltoe.Common.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddKubernetesConfiguration();
builder.UseCloudHosting();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BookDbContext>(options => options.UseMySql(builder.Configuration));
//builder.Services.AddTask<MigrateDbContextTask<BookDbContext>>();

foreach(var config in builder.Configuration.AsEnumerable()) {
    Console.WriteLine($"{config.Key} = {config.Value}");
}

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