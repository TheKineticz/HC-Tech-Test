var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/convertstring", Ex1.Endpoint);
app.MapGet("/chequetext", Ex2.Endpoint);

app.Run();