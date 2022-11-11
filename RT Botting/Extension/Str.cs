using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit
{
    public static class Str
    {
        public static string StripHTML(string input)
        {
            return Regex.Replace(input.Replace("<br/>", "\n"), "<.*?>", String.Empty);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string TranslateError(string input)
        {
            //You cannot write comments on this site
            if (input.Contains("Вы не можете писать комментарии на этом сайте"))
                    return "This account has been banned";
            //Rating limit reached
            if (input.Contains("Достигнут лимит на кол-во оценок"))
                    return "Rate limit reached. Please cancel and wait for a while";
            if (input.Contains("Нельзя ставить оценки своим комментариям"))
                    return "Can't rate your comments";
            if (input.Contains("Account not found"))
                    return "Account not found";
            if (input.Contains("Authorization has been denied for this request."))
                    return "Authorization has been denied for this request.";
            if (input.Contains("You can't comment at this site"))
                    return "You can't comment at this site";
            if (input.Contains("has banned the autonomous system number"))
                    return "1005: The owner of this website (web.tolstoycomments.com) has banned the autonomous system number (ASN) your IP address is in (197288) from accessing this website.";
            if (input.Contains("Please, confirm your email before rating comments"))
                    return "Please, confirm your email before rating comments";
            else
                return "";
        }

        static readonly Random random = new Random();

        public static string RandStr(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
