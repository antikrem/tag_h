using System;
using System.Text.RegularExpressions;

namespace tag_h.Core.Helper.Extensions
{
    public class MatchFirstFailed : Exception { }

    public static class StringExtensions
    {
        public static string MatchFirst(this string value, string regex)
        {
            try
            {
                return Regex.Match(value, regex).Groups[1].Captures[0].Value;
            }
            catch (Exception)
            {
                throw new MatchFirstFailed();
            }
        }
    }
}
