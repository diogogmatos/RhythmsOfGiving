using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using RhythmsOfGiving.Controller.UI;
using RhythmsOfGiving.Data; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
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

app.MapPost("broadcast-auction-update", async (IHubContext<InfoHub, IInfoHub> context) =>
{
    await context.Clients.All.UpdateAuctionInfo();

    return Results.NoContent();
});

app.MapPost("broadcast-not-update", async (IHubContext<InfoHub, IInfoHub> context) =>
{
    await context.Clients.All.UpdateNotificationInfo();

    return Results.NoContent();
});

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
