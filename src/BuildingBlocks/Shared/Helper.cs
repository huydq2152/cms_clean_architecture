using System.Text.RegularExpressions;
using Shared.Extensions;
using shortid;
using shortid.Configuration;

namespace Shared;

public static class Helper
{
    public static class StringHelper
    {
        public static class DateTimeHelper
        {
            #region QuarterRange

            public static (DateTime startDate, DateTime endDate) GetQuarterRange(int quarter, int year)
            {
                var targetDate = new DateTime(year, quarter * 3, 1);
                var result = GetQuarterRange(targetDate);
                return (result.startDate, result.endDate);
            }

            private static (DateTime startDate, DateTime endDate) GetQuarterRange(DateTime input)
            {
                var inputMonth = input.Month;
                var inputYear = input.Year;

                var startDate = new DateTime();
                var endDate = new DateTime();

                if (inputMonth.IsBetween(1, 3))
                {
                    startDate = new DateTime(inputYear, 1, 1);
                    endDate = startDate.AddMonths(3).AddMinutes(-1);
                }

                if (inputMonth.IsBetween(4, 6))
                {
                    startDate = new DateTime(inputYear, 4, 1);
                    endDate = startDate.AddMonths(3).AddMinutes(-1);
                }

                if (inputMonth.IsBetween(7, 9))
                {
                    startDate = new DateTime(inputYear, 7, 1);
                    endDate = startDate.AddMonths(3).AddMinutes(-1);
                }

                if (inputMonth.IsBetween(10, 12))
                {
                    startDate = new DateTime(inputYear, 10, 1);
                    endDate = startDate.AddMonths(3).AddMinutes(-1);
                }

                return (startDate, endDate);
            }

            #endregion
        }

        public static string RemoveVietnameseTone(string text)
        {
            var result = RemoveSymbol(text.ToLower());
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }

        public static string RemoveSymbol(string input)
        {
            return Regex.Replace(input, @"[^a-zA-Z0-9]", string.Empty);
            ;
        }

        public static string Identity()
        {
            return "A" + Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }

        public static string ShortIdentity(int length = 8, bool useSpecialChar = false, bool useNumber = true)
        {
            if (length < 8)
                length = 8;
            return ShortId.Generate(new GenerationOptions
            {
                Length = length,
                UseSpecialCharacters = false,
                UseNumbers = true
            }).ToUpper();
        }

        public static string CodeFormat(string formatInput, string suffixCode)
        {
            var res = formatInput;
            if (suffixCode.Length > res.Length)
                return suffixCode;
            var prefix = res.Substring(0, res.Length - suffixCode.Length);
            res = prefix + suffixCode;
            return res;
        }

        public static string ShortCodeFromString(string input)
        {
            var res = "";
            if (!string.IsNullOrEmpty(input))
                Array.ForEach(input.Split(" ", StringSplitOptions.RemoveEmptyEntries), s => res += s[0]);
            return new string(res.Where(char.IsLetter).ToArray()).ToUpper();
        }

        public static string UpperFirstLetterOfWord(string input)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }

        public static string GetFirstLetterOfWord(string input)
        {
            try
            {
                var res = "";
                if (!string.IsNullOrEmpty(input))
                    Array.ForEach(input.Trim().Split(' '), s => res += s[0]);
                return res;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string UpperFirstChar(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var res = input.ToLower();
            res = res[0].ToString().ToUpper() + res.Substring(1, res.Length - 1);
            return res;
        }

        public static string CharByIndex(int index)
        {
            char[] arrayTitle = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            return arrayTitle[index].ToString();
        }

        public static string NewLineByWord(string input, string seperator, int rangeSize)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) return input;
            var words = input.Split(' ').ToList();
            if (words.Count <= rangeSize) return input;

            var tempWords = new List<string>();

            while (words.Count > rangeSize)
            {
                var rangeWord = "";
                for (var i = 0; i < rangeSize; i++)
                {
                    rangeWord += words[i] + " ";
                }

                tempWords.Add(rangeWord);
                words.RemoveRange(0, rangeSize);
            }

            return string.Join(seperator, tempWords);
        }

        public static string ShortTextByWord(string input, int rangeSize = 15)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) return input;
            var words = input.Split(' ').ToList();
            if (words.Count <= rangeSize) return input;

            var tempWords = new List<string>();

            while (words.Count > rangeSize)
            {
                var rangeWord = "";
                for (var i = 0; i < rangeSize; i++)
                {
                    rangeWord += words[i] + " ";
                }

                tempWords.Add(rangeWord);
                words.RemoveRange(0, rangeSize);
            }

            if (tempWords.Count > 1)
                return tempWords.First() + " ...";
            return tempWords.First();
        }

        public static string Tab(int count)
        {
            var res = "";
            for (var i = 0; i < count; i++)
            {
                res += "    ";
            }

            return res;
        }
    }
}