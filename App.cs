var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/exa", ExA.Endpoint);
app.MapGet("/exb", ExB.Endpoint);

app.Run();