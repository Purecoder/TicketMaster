using System.Text.Encodings.Web;
using TicketMaster.Obilet.Mvc.MiddleWare;
using TicketMaster.Obilet.Mvc.Services;
using TicketMaster.OBilet.Mvc.Handlers;
using TicketMaster.OBilet.Mvc.Settings;


var builder = WebApplication.CreateBuilder(args);

#region api settings

builder.Services.Configure<ObiletApiSettings>(builder.Configuration.GetSection(nameof(ObiletApiSettings)));
var obiletApiSettings = builder.Configuration.GetSection(nameof(ObiletApiSettings));
var baseAddress = obiletApiSettings.GetValue<string>("BaseUrl") ?? string.Empty;

//builder.Services.AddHttpClient<ObiletBusApiService>(nameof(ObiletApiSettings), client => client.BaseAddress = new Uri(baseAddress))
//    .AddHttpMessageHandler<ObiletServiceHandler>();

builder.Services.AddHttpClient<ObiletHttpApiService>(nameof(ObiletApiSettings), client => client.BaseAddress = new Uri(baseAddress))
    .AddHttpMessageHandler<ObiletServiceHandler>();

builder.Services.AddTransient<ObiletServiceHandler>();

builder.Services.AddScoped<ObiletHttpApiService>();
builder.Services.AddScoped<IObiletBusApiService, ObiletBusApiService>();
builder.Services.AddScoped<IObiletSessionApiService, ObiletSessionApiService>();


#endregion

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//.WithStaticAssets();

//app.MapControllerRoute(
//  name: "bus",
//  pattern: "bus",
//  defaults: new { controller = "Bus", action = "Welcome" });
app.UseMiddleware<ObiletSessionMiddleware>();

app.Run();
