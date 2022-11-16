var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// EXERCISE 1 MAIN FUNCTION
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
    int dollars = (int) number;
    int cents = (int) Math.Round(number % 1 * 100);

    if (dollars > 0 && cents > 0) {
        return String.Format("{0} DOLLARS AND {1} CENTS", NumberToEnglish(dollars), NumberToEnglish(cents));
    } else if (dollars > 0) {
        return String.Format("{0} DOLLARS", NumberToEnglish(dollars));
    } else if (cents > 0) {
        return String.Format("{0} CENTS", NumberToEnglish(cents));
    } else {
        return "";
    }
}

// MAIN APP ROUTING
app.MapGet("/convertstring", (string str) => {
    return TypedResults.Ok(new { value = StrToInteger(str) });
});

app.MapGet("/chequetext", (decimal n) => {
    return TypedResults.Ok(new { chequeText = CreateChequeText(n) });
});

app.Run();
