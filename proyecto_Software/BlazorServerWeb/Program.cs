using BlazorServerWeb.Components;
using BlazorServerWeb.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<LogService>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_GATEWAY_URL")));
builder.Services.AddHttpClient<PersonaService>(client => client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_GATEWAY_URL")));
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_GATEWAY_URL")) });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
