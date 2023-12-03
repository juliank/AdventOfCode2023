namespace AdventOfCode.Utils;

public static class ExtensionMethods
{
    public static bool TryParse(this IEnumerable<char> chars, out int result)
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

    public static bool TryParse(this IEnumerable<char> chars, out long result)
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
