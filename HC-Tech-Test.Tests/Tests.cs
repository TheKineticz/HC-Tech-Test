public class ExATests
{
    [Fact]
    public void TestConversion_Zero()
    {
        Assert.Equal(0, ExA.StrToInteger("0"));
    }

    [Fact]
    public void TestConversion_Neg1()
    {
        Assert.Equal(-1, ExA.StrToInteger("-1"));
    }

    [Fact]
    public void TestConversion_Pos1()
    {
        Assert.Equal(1, ExA.StrToInteger("1"));
    }

    [Fact]
    public void TestConversion_LongMax()
    {
        Assert.Equal(long.MaxValue, ExA.StrToInteger(long.MaxValue.ToString()));
    }

    [Fact]
    public void TestConversion_LongMin()
    {
        Assert.Equal(long.MinValue, ExA.StrToInteger(long.MinValue.ToString()));
    }

    [Fact]
    public void TestConversion_StringWithASCII_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ExA.StrToInteger("123TEST456"));
    }

    [Fact]
    public void TestConversion_MisplacedSign_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ExA.StrToInteger("1-234"));
    }
}

public class ExBTests
{
    [Fact]
    public void TestChequeValue_123point45()
    {
        Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FOURTY-FIVE CENTS", ExB.CreateChequeText(123.45m));
    }

    [Fact]
    public void TestChequeValue_Negative_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => ExB.CreateChequeText(-1));
    }

    [Fact]
    public void TestChequeValue_Zero()
    {
        Assert.Equal("ZERO DOLLARS", ExB.CreateChequeText(0));
    }

    [Fact]
    public void TestChequeValue_Pos1()
    {
        Assert.Equal("ONE DOLLAR", ExB.CreateChequeText(1));
    }

    [Fact]
    public void TestChequeValue_Pos10()
    {
        Assert.Equal("TEN DOLLARS", ExB.CreateChequeText(10));
    }

    [Fact]
    public void TestChequeValue_Pos99()
    {
        Assert.Equal("NINETY-NINE DOLLARS", ExB.CreateChequeText(99));
    }

    [Fact]
    public void TestChequeValue_Pos100()
    {
        Assert.Equal("ONE HUNDRED DOLLARS", ExB.CreateChequeText(100));
    }

    [Fact]
    public void TestChequeValue_Pos999()
    {
        Assert.Equal("NINE HUNDRED AND NINETY-NINE DOLLARS", ExB.CreateChequeText(999));
    }

    [Fact]
    public void TestChequeValue_Pos1000()
    {
        Assert.Equal("ONE THOUSAND DOLLARS", ExB.CreateChequeText(1000));
    }

    [Fact]
    public void TestChequeValue_Pos9999()
    {
        Assert.Equal("NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS", ExB.CreateChequeText(9999));
    }

    [Fact]
    public void TestChequeValue_Pos10k()
    {
        Assert.Equal("TEN THOUSAND DOLLARS", ExB.CreateChequeText(10000));
    }

    [Fact]
    public void TestChequeValue_Pos99999()
    {
        Assert.Equal("NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS", ExB.CreateChequeText(99999));
    }

    [Fact]
    public void TestChequeValue_Pos100k()
    {
        Assert.Equal("ONE HUNDRED THOUSAND DOLLARS", ExB.CreateChequeText(100000));
    }

    [Fact]
    public void TestChequeValue_Pos999999()
    {
        Assert.Equal("NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS", ExB.CreateChequeText(999999));
    }

    [Fact]
    public void TestChequeValue_Pos1MIL()
    {
        Assert.Equal("ONE MILLION DOLLARS", ExB.CreateChequeText(1000000));
    }

    [Fact]
    public void TestChequeValue_Pos999999999()
    {
        Assert.Equal("NINE HUNDRED AND NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS", ExB.CreateChequeText(999999999));
    }

    [Fact]
    public void TestChequeValue_Pos1BIL()
    {
        Assert.Equal("ONE BILLION DOLLARS", ExB.CreateChequeText(1000000000));
    }

    [Fact]
    public void TestChequeValue_Pos999999999999()
    {
        Assert.Equal("NINE HUNDRED AND NINETY-NINE BILLION NINE HUNDRED AND NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE DOLLARS", ExB.CreateChequeText(999999999999));
    }

    [Fact]
    public void TestChequeValue_Pos1TRIL()
    {
        Assert.Equal("ONE TRILLION DOLLARS", ExB.CreateChequeText(1000000000000));
    }
}