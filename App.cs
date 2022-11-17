var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/convertstring", ExA.Endpoint);
app.MapGet("/chequetext", ExB.Endpoint);

app.Run();