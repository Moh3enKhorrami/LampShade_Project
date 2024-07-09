namespace _0_Framework.Application;

public static class Tools
{
    public static  string ToDiscountFormat(this DateTime date)
    {
        if (date == new DateTime()) return "";
        return $"{date.Year}/{date.Month}/{date.Day}";
    }
}