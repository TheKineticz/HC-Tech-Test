var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// EXERCISE 1 FUNCTION
long StrToInteger(string strNumber) {
    long value = 0;
    foreach (char c in strNumber) {
        value *= 10;
        value += c - '0';
    }
    return value;
}

// EXERCISE 2 FUNCTIONS
string NumberToEnglish(long n){
    string[] units = {"ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"};
    string[] tens = {"ZERO", "TEN", "TWENTY", "THIRTY", "FOURTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"};
    Tuple<long, string>[] powersOfTen = {
        new Tuple<long, string>(1000000000000, " TRILLION "),
        new Tuple<long, string>(1000000000, " BILLION "),
        new Tuple<long, string>(1000000, " MILLION "),
        new Tuple<long, string>(1000, " THOUSAND "),
        new Tuple<long, string>(100, " HUNDRED "),
    };

    string text = "";
    foreach (Tuple<long, string> pair in powersOfTen) {
        if (n / pair.Item1 > 0){
            text += NumberToEnglish(n / pair.Item1) + pair.Item2;
            n %= pair.Item1;
        }
    }

    if (n > 0){
        if (!String.IsNullOrEmpty(text)) {
            text += "AND ";
        }
        if (n < 20) {
            text += units[n];
        } else {
            text += tens[n / 10];
            if (n % 10 > 0) {
                text += "-" + units[n % 10];
            }
        }
    }

    if (text.EndsWith(" ")) {
        text = text.Remove(text.Length - 1);
    }
    return text;
}

string CreateChequeText(decimal number) {
    long dollars = (long) number;
    long cents = (long) Math.Round(number % 1 * 100);

    if (dollars > 0 && cents > 0) {
        return String.Format("{0} DOLLAR{1} AND {2} CENT{3}", NumberToEnglish(dollars), dollars > 1 ? "S":"", NumberToEnglish(cents), cents > 1 ? "S":"");
    } else if (dollars > 0) {
        return String.Format("{0} DOLLAR{1}", NumberToEnglish(dollars), dollars > 1 ? "S":"");
    } else if (cents > 0) {
        return String.Format("{0} CENT{1}", NumberToEnglish(cents), cents > 1 ? "S":"");
    } else {
        return "";
    }
}

// MAIN APP ROUTING
app.MapGet("/convertstring", (string? value) => {
    if (!(value is null) && !value.Equals("") && value.All(Char.IsDigit)) {
        return Results.Ok(new { value = StrToInteger(value) });
    }
    return Results.BadRequest(new { message = "value query is missing or invalid." });
});

app.MapGet("/chequetext", (string? value) => {
    decimal dValue;
    if (!(value is null) && !value.Equals("") && decimal.TryParse(value, out dValue)) {
        return Results.Ok(new { chequeText = CreateChequeText(dValue) });
    }
    return Results.BadRequest(new { message = "value query is missing or invalid." });
});

app.Run();
