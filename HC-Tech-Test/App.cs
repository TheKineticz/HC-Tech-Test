var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/exa", ExA.Endpoint);
app.MapGet("/exb", ExB.Endpoint);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();