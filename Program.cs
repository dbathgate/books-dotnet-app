using Microsoft.EntityFrameworkCore;
using RazorApp.Repository;
using Steeltoe.Extensions.Configuration.Kubernetes.ServiceBinding;
using Steeltoe.Management.TaskCore;
using Steeltoe.Common.Hosting;
using Steeltoe.Connector.PostgreSql.EFCore;
using Steeltoe.Connector.EFCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddKubernetesServiceBindings();
builder.UseCloudHosting();

// Add services to the container.
builder.Services.AddRazorPages();

var mysqlOptions = builder.Configuration.GetSection("k8s:bindings:books-db");
var connectionString = $"Server={mysqlOptions["host"]}; Port={mysqlOptions["port"]}; Database={mysqlOptions["database"]}; Uid={mysqlOptions["username"]}; Pwd={mysqlOptions["password"]}";

builder.Services.AddDbContext<BookDbContext>(options => options.UseNpgsql(builder.Configuration));
// builder.Services.AddDbContext<BookDbContext>(options => options.UseMySql(builder.Configuration));
builder.Services.AddTask<MigrateDbContextTask<BookDbContext>>(ServiceLifetime.Transient);


Console.WriteLine($"My Secret {builder.Configuration["k8s:bindings:my-secret:password"]}");

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
