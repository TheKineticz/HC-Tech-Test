public static class Ex1 {
    public static long StrToInteger(string strNumber) {
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

    public static IResult Endpoint(String? value) {
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
    }
}