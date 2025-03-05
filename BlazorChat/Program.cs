using ApplicationCore.Commons.Repository;
using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models;
using BlazorChat.Components;
using BlazorChat.Components.Hubs;
using Infrastructure.Memory.Generators;
using Infrastructure.Memory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();  
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IGenericRepository<ChatUser, int>>(provider => 
    new MemoryGenericRepository<ChatUser, int>(new IntGenerator()));
builder.Services.AddSingleton<IChatUserService, ChatUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapHub<BlazorChatHub>(BlazorChatHub.HubUrl);

app.Run();