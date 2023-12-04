namespace AdventOfCode.Utils;

public static class ExtensionMethods
{
    public static int ParseInt(this IEnumerable<char> chars)
    {
        var s = new string(chars.ToArray());
        return int.Parse(s);
    }

    public static long ParseLong(this IEnumerable<char> chars)
    {
        var s = new string(chars.ToArray());
        return long.Parse(s);
    }

    public static bool TryParseInt(this IEnumerable<char> chars, out int result)
    {
        var s = new string(chars.ToArray());
        if (int.TryParse(s, out var i))
        {
            result = i;
            return true;
        }

        result = 0;
        return false;
    }

    public static bool TryParseLong(this IEnumerable<char> chars, out long result)
    {
        var s = new string(chars.ToArray());
        if (long.TryParse(s, out var l))
        {
            result = l;
            return true;
        }

        result = 0;
        return false;
    }
}
