using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator;

public class StringCalculator
{
    private const int _biggestAllowed = 1000;

    public int Calculate(string arg)
    {
        if (arg == "")
        {
            return 0;
        }

        string[] delimiters = ["\n", ","];
        string number_string = arg;

        var tuple = SplitAfterFirstLine(arg);
        if (tuple != null)
        {
            var delim = TryParseDelimiter(tuple.Value.first);
            if (delim != null)
            {
                number_string = tuple.Value.second;
                delimiters = delim;
            }
        }

        if (ParseSingleNum(number_string, out int single_result))
        {
            if (single_result > _biggestAllowed)
            {
                return 0;
            }
            return single_result;
        }

        if (ParseDelimitedSum(number_string, out int multiple_nums, delimiters))
        {
            return multiple_nums;
        }

        return -1;
    }

    private static (string first, string second)? SplitAfterFirstLine(string input)
    {
        var splited = input.Split("\n");
        if (splited.Length > 1)
        {
            return (splited[0], input[(splited[0].Length + 1)..]);
        }

        return null;
    }

    private static string[]? TryParseDelimiter(string line_with_delim)
    {
        if (line_with_delim[0] != '/' || line_with_delim[1] != '/')
        {
            return null;
        }

        var trimmed = line_with_delim.Trim(['/', '[', ']']);

        return trimmed.Split("][");
    }

    private static bool ParseSingleNum(string input, out int result)
    {
        var was_parsed = int.TryParse(input, out result);
        if (result < 0)
        {
            throw new NegativeNumberException($"Negative numbers like '{result}' are not allowed");
        }
        return was_parsed;
    }

    private static bool ParseDelimitedSum(string input, out int result, string[] delimiters)
    {
        var splited_by_comma = input.Split(delimiters, StringSplitOptions.TrimEntries);

        int summed_counter = 0;
        int sum = 0;
        foreach (var item in splited_by_comma)
        {
            var parse_success = ParseSingleNum(item, out int number);
            if (number > _biggestAllowed)
            {
                continue;
            }
            if (!parse_success)
            {
                result = -1;
                return false;
            }
            sum += number;
            summed_counter += 1;
        }
        if (summed_counter < 4)
        {
            result = sum;
            return true;
        }

        result = -1;
        return false;
    }
}

