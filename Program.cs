using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.SignalR;
using RhythmsOfGiving.Authentication;
using RhythmsOfGiving.Controller.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, RhythmsAuthStateProvider>();
builder.Services.AddSingleton<UiService>();
builder.Services.AddSignalR();
builder.Services.AddCors();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<InfoHub>("infohub");

app.MapPost("broadcast-auction-update", async (IHubContext<InfoHub, IInfoHub> context, int idLeilao) =>
{
    await context.Clients.All.UpdateAuctionInfo(idLeilao);

    return Results.NoContent();
});

app.MapPost("broadcast-not-update", async (IHubContext<InfoHub, IInfoHub> context, List<int> idsLicitadores) =>
{
    await context.Clients.All.UpdateNotificationInfo(idsLicitadores);

    return Results.NoContent();
});

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
