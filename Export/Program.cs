using Export.Areas.Identity;
using Export.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();
var sqlConnectionConfiguration = new SqlConnectionConfiguration(builder.Configuration.GetConnectionString("SqlDBContext"));
builder.Services.AddSingleton(sqlConnectionConfiguration);
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@32322e302e30VFxOfkDIwwqo1kFDSDH8fmLKW9cgzkwJ7RLZFaPfOuA=");
builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<IAduanasService, AduanasService>();
builder.Services.AddScoped<ICargadorService, CargadorService>();
builder.Services.AddScoped<IConsignatariosService, ConsignatariosService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IDetalleProductoService, DetalleProductoService>();
builder.Services.AddScoped<IExportHeaderService, ExportHeaderService>();
builder.Services.AddScoped<IFacturaCoexpoService, FacturaCoexpoService>();
builder.Services.AddScoped<IPolizaImportacionService, PolizaImportacionService>();
builder.Services.AddScoped<IPolizasCoexpoService, PolizasCoexpoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
