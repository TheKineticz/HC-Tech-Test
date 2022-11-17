public static class Ex1
{
    public static long StrToInteger(string strNumber)
    {
        if (strNumber.All(Char.IsDigit) || (strNumber.StartsWith("-") && !strNumber.Equals("-") && strNumber.Remove(0, 1).All(Char.IsDigit)))
        {
            /* 
                For each char in string, convert the ASCII value of the char to it's digit value, 
                then shift the whole number left (multiply by 10) and add the new digit.
            */
            bool isNegative = false;
            if (strNumber.StartsWith("-"))
            {
                isNegative = true;
                strNumber = strNumber.Remove(0, 1);
            }

            long value = 0;
            foreach (char c in strNumber)
            {
                value = checked(value * 10);
                value += c - '0';
            }

            if (isNegative)
            {
                value = -value;
            }

            return value;

        } 
        else
        {
            throw new ArgumentException("Input string must contain only digits, excepting the negative sign.");
        }
    }

    public static IResult Endpoint(String? value)
    {
        if (!String.IsNullOrEmpty(value))
        {
            try
            {
                long numberValue = StrToInteger(value);
                return TypedResults.Ok(new { value = numberValue });
            } 
            catch (ArithmeticException)
            {
                return TypedResults.BadRequest(new { error = "Input value is too large." });
            }
            catch (ArgumentException)
            {
                return TypedResults.BadRequest(new { error = "Input value must only contain digits, not including negative sign." });
            }
        }

        return TypedResults.BadRequest(new { error = "Value query is missing or empty." });
    }
}