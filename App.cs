var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// MAIN APP ROUTING
// EXERCISE 1
app.MapGet("/convertstring", (String? value) => {
    if (!String.IsNullOrEmpty(value)) {
        if (value.All(Char.IsDigit) || (value.StartsWith("-") && !value.Equals("-") && value.Remove(0, 1).All(Char.IsDigit))) {
            try {
                long numberValue = StrToInteger(value);
                return Results.Ok(new { value = numberValue });
            } catch (ArithmeticException) {
                return Results.BadRequest(new { error = "Input value is too large." });
            }
        } else {
            return Results.BadRequest(new { error = "Input value must only contain digits, not including negative sign." });
        }
    }

    return Results.BadRequest(new { error = "Value query is missing or empty." });
});

// EXCERCISE 2
app.MapGet("/chequetext", (String? value) => {
    Decimal dValue;
    String chequeText;

    if (!String.IsNullOrEmpty(value)) {
        if (Decimal.TryParse(value, out dValue) && dValue > 0) {
            try {
                chequeText = CreateChequeText(dValue);
                return Results.Ok(new { chequeText });
            } catch (OverflowException) {
                return Results.BadRequest(new { error = "Input value is too large." });
            }
        } else {
            return Results.BadRequest(new { error = "Input value must be a positive number." });
        }
    }

    return Results.BadRequest(new { error = "Value query is missing or empty." });
});

app.Run();


// EXERCISE 1 FUNCTION
long StrToInteger(string strNumber) {
    /* 
        For each char in string, convert the ASCII value of the char to it's digit value, 
        then shift the whole number left (multiply by 10) and add the new digit.
    */
    bool isNegative = false;
    if (strNumber.StartsWith("-")) {
        isNegative = true;
        strNumber = strNumber.Remove(0, 1);
    }

    long value = 0;
    foreach (char c in strNumber) {
        value = checked(value * 10);
        value += c - '0';
    }

    if (isNegative) {
        value = -value;
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

    /* 
        Recursively calls the function to split each "big number" section, 
        ending each function call after adding remaining tens and ones
        e.g. 123456 is split into 7 parts, added to the string in order 100, 20, 3, 1000, 400, 50, 6
    */
    String text = "";
    foreach (Tuple<long, String> pair in powersOfTen) {
        if (n / pair.Item1 > 0){
            text += NumberToEnglish(n / pair.Item1) + pair.Item2;
            n %= pair.Item1;
        }
    }

    if (n > 0){
        if (!text.Equals("")) {
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

    // Post cleanup of any trailing spaces
    if (text.EndsWith(" ")) {
        text = text.Remove(text.Length - 1);
    }
    return text;
}

string CreateChequeText(Decimal number) {
    // Splits the decimal input into two long int values around the point
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