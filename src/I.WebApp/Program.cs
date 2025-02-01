using I.Service;
using I.Service.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Move startup
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpClientService,HttpClientService>();
builder.Services.AddSingleton<IHHClientService,HHClientService>();


var app = builder.Build();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
