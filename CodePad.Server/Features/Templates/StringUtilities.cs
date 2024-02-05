namespace CodePad.Server.Features.Templates;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

public static class StringUtilities
{
    public static string Clean(this string text)
    {
        var data = text
                   .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                   .Distinct()
                   .OrderBy(c => c)
                   .ToArray();

        var result = String.Join(Environment.NewLine, data)
                                .TrimStart(Environment.NewLine.ToCharArray());
        return result;
    }

    private static string getEnumFriendlyName(object source)
    {
        if (source == null)
            return string.Empty;

        var type = source.GetType();
        var memberInfos = type.GetMember(source.ToString());
        if (memberInfos.Any())
        {
            var description = memberInfos.First().GetCustomAttributes(typeof(DescriptionAttribute),
                false).FirstOrDefault();
            if (description != null)
                return ((DescriptionAttribute)description).Description;

            var display = memberInfos.First().GetCustomAttributes(typeof(DisplayAttribute),
                false).FirstOrDefault();
            if (display != null)
                return ((DisplayAttribute)display).Name;
        }

        return null;
    }

    public static string ToFriendlyString(this object o)
    {
        if (o == null)
            return string.Empty;

        if (o is Enum)
        {
            var enumFriendlyName = getEnumFriendlyName(o);
            if (enumFriendlyName != null)
                return enumFriendlyName;
            if (o.ToString() == "0")
                return string.Empty;
        }

        var val = o.ToString();

        var stringBuilder = new StringBuilder();
        var lastWasCap = false;

        foreach (var c in val)
        {
            var s = c.ToString();
            var n = (int)c;
            if (n == 32 | n == 95)
            {
                stringBuilder.Append(" ");
                lastWasCap = false;
            }
            else if (n >= 65 & n <= 90)
            {
                //Capital letters
                if (!lastWasCap)
                {
                    stringBuilder.Append(" ");
                    lastWasCap = true;
                }
                stringBuilder.Append(s);
            }
            else
            {
                lastWasCap = false;
                stringBuilder.Append(s);
            }
        }
        return stringBuilder.ToString().Trim();
    }
}
