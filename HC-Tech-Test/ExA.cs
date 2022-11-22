/*
    Exercise A: Find the number in a string
*/
public static class ExA
{
    /*
        Given a string that represents an integer number, convert that string to it's represented long int form.
        
        param strNumber: The string form of the number to be converted
        returns: a long int with the value represented by the input string
    */
    public static long StrToInteger(string strNumber)
    {
        // Check that the input string is in the expected format (all digits, with the exception of the negative sign)
        if (strNumber.All(Char.IsDigit) || (strNumber.StartsWith("-") && !strNumber.Equals("-") && strNumber.Remove(0, 1).All(Char.IsDigit)))
        {
            // Handle the existence of a negative sign
            bool isNegative = false;
            if (strNumber.StartsWith("-"))
            {
                isNegative = true;
                strNumber = strNumber.Remove(0, 1);
            }

            /* 
                For each char in string, convert the ASCII value of the char to it's digit value, 
                then shift the whole number left (multiply by 10) and add the new digit.
            */
            long value = 0;
            foreach (char c in strNumber)
            {
                value = checked(value * 10); // Use checked so an ArithmeticError is thrown on overflow
                value  = value + (c - '0');
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

    /*
        Main function for the /convertstring endpoint. Returns a Status OK with the converted string payload on success, Status BadRequest with error message on fail.
        
        param value: The query parameter storing the string value of the number to be converted. Nullable.
        returns: A result with payload, to be interpreted by the browser.
    */
    public static IResult Endpoint(String? value)
    {
        if (!String.IsNullOrEmpty(value))
        {
            try
            {
                long numberValue = StrToInteger(value);
                Console.WriteLine(numberValue.ToString());
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