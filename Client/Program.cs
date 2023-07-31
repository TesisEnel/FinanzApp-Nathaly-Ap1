using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FinanzApp.Client;
using Radzen;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using FinanzApp.Client.Extensors;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<NotificationService>();
        builder.Services.AddScoped<TooltipService>();
        builder.Services.AddScoped<ContextMenuService>();

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddScoped<AuthenticationStateProvider,AutenticacionExtension>();
        builder.Services.AddAuthorizationCore();
        
await builder.Build().RunAsync();
