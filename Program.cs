using Microsoft.EntityFrameworkCore;
using RazorApp.Repository;
using Steeltoe.Connector.EFCore;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Connector.MySql.EFCore;
using Steeltoe.Management.TaskCore;
using Steeltoe.Common.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddCloudFoundryConfiguration();
builder.UseCloudHosting();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BookDbContext>(options => options.UseMySql(builder.Configuration));
//builder.Services.AddTask<MigrateDbContextTask<BookDbContext>>();

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
