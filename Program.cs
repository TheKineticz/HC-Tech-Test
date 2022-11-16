var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int StrToInt(string strNumber){
    int number = 0;
    foreach (char c in strNumber){
        number *= 10;
        number += c - '0';
    }
    return number;
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/strtoint", (string number) => {
    return StrToInt(number);
});

app.Run();
