var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// EXERCISE 1 FUNCTION
long StrToInteger(string strNumber) {
    long value = 0;
    foreach (char c in strNumber) {
        value = checked(value * 10);
        value += c - '0';
    }
    return value;
}

// EXERCISE 2 FUNCTIONS
String NumberToEnglish(long n){
    String[] units = {"ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"};
    String[] tens = {"ZERO", "TEN", "TWENTY", "THIRTY", "FOURTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"};
    Tuple<long, String>[] powersOfTen = {
        new Tuple<long, String>(1000000000000, " TRILLION "),
        new Tuple<long, String>(1000000000, " BILLION "),
        new Tuple<long, String>(1000000, " MILLION "),
        new Tuple<long, String>(1000, " THOUSAND "),
        new Tuple<long, String>(100, " HUNDRED "),
    };

    String text = "";
    foreach (Tuple<long, String> pair in powersOfTen) {
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

string CreateChequeText(Decimal number) {
    long dollars = (long) Math.Floor(number);
    long cents = (long) (Math.Round(number % 1 * 100));

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
app.MapGet("/convertstring", (String? value) => {
    if (!(value is null) && !value.Equals("") && value.All(Char.IsDigit)) {
        try {
            long numberValue = StrToInteger(value);
            return Results.Ok(new { value = numberValue });
        } catch (ArithmeticException) {
            return Results.BadRequest(new { error = "input value is too large." });
        }
    }

    return Results.BadRequest(new { error = "value query is missing or invalid." });
});

app.MapGet("/chequetext", (String? value) => {
    Decimal dValue;
    String chequeText;

    if (!(value is null) && !value.Equals("") && Decimal.TryParse(value, out dValue)) {
        try {
            chequeText = CreateChequeText(dValue);
        } catch (OverflowException) {
            return Results.BadRequest(new { error = "input value is too large." });
        }

        return Results.Ok(new { chequeText });
    }

    return Results.BadRequest(new { error = "value query is missing or invalid." });
});

app.Run();
