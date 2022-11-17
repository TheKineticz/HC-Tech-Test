/*
    Exercise B: Write the text on a cheque
*/
public static class ExB
{
    public static String NumberToEnglish(long n)
    {
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
        foreach (Tuple<long, String> pair in powersOfTen)
        {
            if (n / pair.Item1 > 0)
            {
                text += NumberToEnglish(n / pair.Item1) + pair.Item2;
                n %= pair.Item1;
            }
        }

        if (n > 0)
        {
            // If a large numword has been added to this string previously, seperate the large and small numword sections with "AND"
            if (!text.Equals(""))
            {
                text += "AND ";
            }

            // If only a number < 20 remains, add just that numword to the string
            if (n < 20)
            {
                text += units[n];
            }
            // Else add the numword for the tens-value to the string, and convert the remaining digit numword seperately
            else
            {
                text += tens[n / 10];
                if (n % 10 > 0)
                {
                    text += "-" + units[n % 10];
                }
            }
        }

        // Post-process cleanup of any trailing spaces
        if (text.EndsWith(" "))
        {
            text = text.Remove(text.Length - 1);
        }
        return text;
    }

    public static string CreateChequeText(Decimal number)
    {
        if (number < 0)
        {
            throw new ArgumentException("number parameter cannot be negative", nameof(number));
        }
        
        // Splits the decimal input into two long int values around the point
        long dollars = (long) Math.Floor(number);
        long cents = (long) (Math.Round(number % 1 * 100));

        if (dollars > 0 && cents > 0)
        {
            return String.Format("{0} DOLLAR{1} AND {2} CENT{3}", NumberToEnglish(dollars), dollars > 1 ? "S":"", NumberToEnglish(cents), cents > 1 ? "S":"");
        }
        else if (dollars > 0)
        {
            return String.Format("{0} DOLLAR{1}", NumberToEnglish(dollars), dollars > 1 ? "S":"");
        }
        else if (cents > 0)
        {
            return String.Format("{0} CENT{1}", NumberToEnglish(cents), cents > 1 ? "S":"");
        }
        else
        {
            return "ZERO DOLLARS";
        }
    }

    /*
        Main function for the /chequetext endpoint. Returns a Status OK with the converted string payload on success, Status BadRequest with error message on fail.
        
        param value: The query parameter storing the string value of the number to be converted to decimal and passed to CreateChequeText. Nullable.
        returns: A result with payload, to be interpreted by the browser.
    */
    public static IResult Endpoint(String? value)
    {
        Decimal dValue;
        String chequeText;

        if (!String.IsNullOrEmpty(value))
        {
            // Input is received as String? so that the function can explicitly parse the input value to decimal and respond accordingly.
            if (Decimal.TryParse(value, out dValue))
            {
                try
                {
                    chequeText = CreateChequeText(dValue);
                    return TypedResults.Ok(new { chequeText });
                }
                catch (OverflowException)
                {
                    return TypedResults.BadRequest(new { error = "Input value is too large." });
                }
                catch (ArgumentException)
                {
                    return TypedResults.BadRequest(new { error = "Input value must be positive or zero." });
                }
            }
        }

        return TypedResults.BadRequest(new { error = "Value query is missing or empty." });
    }
}