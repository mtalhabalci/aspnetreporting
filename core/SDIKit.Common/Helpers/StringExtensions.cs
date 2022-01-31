using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SDIKit.Common.Helpers
{
    public static class StringExtensions
    {
        public static string ControllerName(this string controllerName)
        {
            return controllerName.EndsWith("Controller") ? controllerName.Substring(0, controllerName.Length - 10) : controllerName;
        }

        public static string FromBase64(this string value)
        {
            var base64EncodedBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Validates whether a string is compliant with a strong password policy.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsStrongPassword(this string s)
        {
            bool isStrong = Regex.IsMatch(s, @"[\d]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[a-z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[A-Z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
            if (isStrong) isStrong = s.Length > 6;
            return isStrong;
        }

        /// <summary>
        /// Check wheter a string is an valid e-mail address
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static string NormalizeColumnName<T>(this string value)
        {
            //T = genre
            var type = typeof(T);
            var property = type.GetProperties().First(i => i.Name.ToLower() == value.ToLower());
            return property.Name;
        }

        public static string ReverseString(this string s)
        {
            char[] c = s.ToCharArray();
            string ts = string.Empty;

            for (int i = c.Length - 1; i >= 0; i--)
                ts += c[i].ToString();

            return ts;
        }

        public static string ToBase64(this string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Takes the first character of an input string and makes it uppercase
        /// </summary>
        /// <param name="Input">Input string</param>
        /// <returns>String with the first character capitalized</returns>
        public static string ToFirstCharacterUpperCase(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return null;
            char[] InputChars = Input.ToCharArray();
            for (int x = 0; x < InputChars.Length; ++x)
            {
                if (InputChars[x] != ' ' && InputChars[x] != '\t')
                {
                    InputChars[x] = char.ToUpper(InputChars[x]);
                    break;
                }
            }
            return new string(InputChars);
        }

        public static string ToFormattedString(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Verilen metni tarayıcı ve seo standartlarına uygun şekilde uri lere çevirir.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToSeoUri(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Trim();

            // first turkish replaces
            text = text.Replace("İ", "i");
            text = text.Replace("I", "i");
            text = text.Replace("ı", "i");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ğ", "g");
            text = text.Replace("ü", "u");
            text = text.Replace("Ü", "u");
            text = text.Replace("ş", "s");
            text = text.Replace("Ş", "s");
            text = text.Replace("ö", "o");
            text = text.Replace("Ö", "o");
            text = text.Replace("ç", "c");
            text = text.Replace("Ç", "c");
            // tolower
            text = text.ToLower();

            // clear spaces
            do
            {
                text = text.Replace(" ", "-");
                text = text.Replace("--", "-");
            } while (text.Contains(" ") || text.Contains("--"));

            // clear special chars
            text = Regex.Replace(text, @"[^a-z0-9\-\s]", "", RegexOptions.Compiled);

            return text;
        }

        /// <summary>
        /// 12,345
        /// 1,234,567.325
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static string ToStringMoney(this string digit)
        {
            string afterPoint = string.Empty;
            string strDigit = digit;
            int pos = digit.IndexOf('.');
            if (digit.IndexOf('.') != -1)
            {
                strDigit = digit.Substring(0, pos);
                afterPoint = digit.Substring(pos, digit.Length - pos);
            }

            int len = strDigit.Length;
            if (len <= 3)
                return digit;

            strDigit = strDigit.ReverseString();
            string result = string.Empty;
            for (int i = 0; i < len; i++)
            {
                result += strDigit[i];
                if ((i + 1) % 3 == 0 && i != len - 1)
                    result += ',';
            }

            if (string.IsNullOrEmpty(afterPoint))
                result = result.ReverseString();
            else result = result.ReverseString() + afterPoint;
            return result;
        }

        public static string ToTrLower(this string value)
        {
            return value.ToLower(CultureInfo.GetCultureInfo(1055));
        }

        public static string ToTrUpper(this string value)
        {
            return value.ToUpper(CultureInfo.GetCultureInfo(1055));
        }
    }
}