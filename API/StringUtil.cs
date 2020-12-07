using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestRailHelperStarShip.Enums;

namespace TestRailHelperStarShip.API
{
    public class StringUtil
    {
        private static IList<string> Alphabet => GetAlphabet(LetterType.Lowercase);

        public static string ReturnStringWithoutNonDigitSymbols(string strToDeleteNonDigits)
        {
            var sb = new StringBuilder();
            var nonDigitStrArr = strToDeleteNonDigits.Where(c => char.IsDigit(c)).ToArray();
            return new string(nonDigitStrArr);
        }

        public static IList<string> GetAlphabet(LetterType letterType)
        {
            var alphabet = new List<string>();
            for (int i = 65; i <= 90; i++)
            {
                var letter = ((char)i).ToString();
                switch (letterType)
                {
                    case LetterType.Uppercase:
                        letter = letter.ToUpper(); break;
                    default: letter = letter.ToLower(); break;
                }
                alphabet.Add(letter);
            }
            return alphabet;
        }

        public static string GetRandomText(int textLength = 20, bool useSpace = false, bool useUpperCase = false,
           int meanWordLength = 5)
        {
            var letterType = useUpperCase ? LetterType.Uppercase : LetterType.Lowercase;

            var stringBuilder = new StringBuilder();
            var randomOrdinal = new Random();
            for (int i = 0; i < textLength; i++)
            {
                int symbolNum = randomOrdinal.Next(Alphabet.Count); //no out of range cause max returns: alphabet.Length - 1 
                string returnSymbol = Alphabet[symbolNum];
                if (useUpperCase)
                {
                    var randProbablityValue = randomOrdinal.NextDouble();
                    var thresholdProbabilityValue = 1 - 1.0 / meanWordLength;
                    bool upperCaseCondition = randProbablityValue >= thresholdProbabilityValue;
                    if (upperCaseCondition && i > 0)
                    {
                        returnSymbol = returnSymbol.ToUpper();
                    }
                }
                if (useSpace && i > 0 && i < textLength - 1 && stringBuilder[i - 1] != ' ')
                {
                    var randProbablityValue = randomOrdinal.NextDouble();
                    var thresholdProbabilityValue = 1 - 1.0 / meanWordLength;
                    bool spaceCondition = randProbablityValue >= thresholdProbabilityValue;
                    if (spaceCondition)
                    {
                        returnSymbol = " ";
                    }
                }
                stringBuilder.Append(returnSymbol);
            }
            return stringBuilder.ToString();
        }

        public static string MakeBasicAuthBase64String(string login, string password)
        {
            var basicAuthStr = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login}:{password}"));
            return basicAuthStr;
        }
        public static string CompleteStringWitnSpasesOrTruncate(string currentString, int stringLengthToGet, string trancateSeq = "...")
        {
            var trancateSeqLength = trancateSeq.Length;
            if (currentString.Length < stringLengthToGet)
            {
                var lessOn = stringLengthToGet - currentString.Length + trancateSeqLength;
                return currentString + new string(' ', lessOn);
            }
            if (currentString.Length > stringLengthToGet)
            {
                var truncateStr = currentString[0..stringLengthToGet] + trancateSeq;
                return truncateStr;
            }
            return currentString + new string(' ', trancateSeqLength);
        }

        public static string GetDelimeterString(int length, char delimeterChar)
        {
            return new string(delimeterChar, length);
        }

        public static TEnum GetEnumFromStr<TEnum>(string enumValue) where TEnum : struct
        {
            var enumType = typeof(TEnum);
            return (TEnum)Enum.Parse(enumType, enumValue);
        }

    }
}
