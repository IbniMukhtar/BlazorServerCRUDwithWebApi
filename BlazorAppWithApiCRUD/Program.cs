using Demo.ApiClient;
using Demo.ApiClient.IoC;
using Radzen;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor(); // Add HttpContextAccessor for AuthenticationService
builder.Services.AddDemoApiClientService(x => x.ApiBaseAddress = builder.Configuration.GetValue<string>("ApiBaseAddress"));

// Register HttpClient and configure it
builder.Services.AddHttpClient<AuthenticationService>(client =>
{
    string? apiBaseAddress = builder.Configuration.GetValue<string>("ApiBaseAddress");
    if (apiBaseAddress != null)
    {
        client.BaseAddress = new Uri(apiBaseAddress);
    }
    else
    {
        throw new InvalidOperationException("API base address is not configured.");
    }
});

builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
