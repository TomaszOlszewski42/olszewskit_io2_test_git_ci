using Calculator;

namespace StringCalculatorTests;

[TestClass]
public sealed class StringCalculatorTests
{
    [TestMethod]
    public void Calculate_EmptyString_ShouldReturn0()
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate("");

        Assert.AreEqual(0, calculated);
    }

    [TestMethod]
    [DataRow("123", 123)]
    [DataRow("1", 1)]
    [DataRow("42", 42)]
    public void Calculate_SingleNum_ShouldReturnThisNum(string number_str, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(number_str);

        Assert.AreEqual(result, calculated);
    }

    [TestMethod]
    [DataRow("123, 42", 165)]
    [DataRow("100, 1", 101)]
    public void Calculate_TwoNumsCommaDelimited_ShouldReturnSum(string input, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(input);

        Assert.AreEqual(result, calculated);
    }

    [TestMethod]
    [DataRow("123\n42", 165)]
    [DataRow("100\n1", 101)]
    public void Calculate_TwoNumsNewlineDelimeter_ShouldReturnSum(string input, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(input);

        Assert.AreEqual(result, calculated);
    }

    [TestMethod]
    [DataRow("123\n42, 12", 177)]
    [DataRow("100,1,2", 103)]
    [DataRow("1\n2\n3", 6)]
    public void Calculate_ThreeSumsDelimitedByCommaOrNewline_ShouldReturnSum(string input, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(input);

        Assert.AreEqual(result, calculated);
    }

    [TestMethod]
    [DataRow("-1")]
    [DataRow("3, -1, 5")]
    public void Calculate_NegativeNumbers_ShoudlThrow(string input)
    {
        var calcuator = new StringCalculator();

        Assert.Throws<NegativeNumberException>(() => calcuator.Calculate(input));
    }

    [TestMethod]
    [DataRow("123456789", 0)]
    [DataRow("1001, 1", 1)]
    [DataRow("3, 1000000, 42\n5", 50)]
    public void Calculate_NumbersGreaterThan1000_ShouldIgnoreThose(string input, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(input);

        Assert.AreEqual(result, calculated);
    }

    [TestMethod]
    [DataRow("//#\n101#3", 104)]
    [DataRow("//#\n52#3#2", 57)]
    public void Calculate_CustomDelimiter_ShouldReturnSum(string input, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(input);

        Assert.AreEqual(result, calculated);
    }

    [TestMethod]
    [DataRow("//[###]\n101###3", 104)]
    [DataRow("//[###]\n52###3###2", 57)]
    public void Calculate_CustomMulticharDelimiter_ShouldReturnSum(string input, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(input);

        Assert.AreEqual(result, calculated);
    }

    [TestMethod]
    [DataRow("//[##][a]\n101##3a4", 108)]
    [DataRow("//[xx][<>]\n52<>3xx2", 57)]
    public void Calculate_MultipleCustomMulticharDelimiter_ShouldReturnSum(string input, int result)
    {
        var calculator = new StringCalculator();

        var calculated = calculator.Calculate(input);

        Assert.AreEqual(result, calculated);
    }
}

