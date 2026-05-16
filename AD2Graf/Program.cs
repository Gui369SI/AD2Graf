using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AD2Graf.Data;
using AD2Graf.Repositorios;
using AD2Graf.Servicos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AD2GrafContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AD2GrafContext") ?? throw new InvalidOperationException("Connection string 'AD2GrafContext' not found.")));

// Injeção de dependência
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();