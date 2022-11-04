using SignalRPollsterSite.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();  //enable SignalR
builder.Services.AddSingleton<IPollProvider, InMemoryPollProvider>();   //register the InMemoryPollProvider for use in Dependency Injection in the PollHub
var app = builder.Build();
app.UseDefaultFiles();  //redirects to index.html
app.UseStaticFiles();   //allows use of wwwroot folder
app.MapHub<PollHub>("/pollster");   //maps the PollHub class to the route "/pollster"
app.Run();