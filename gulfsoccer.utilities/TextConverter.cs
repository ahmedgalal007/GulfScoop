using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace gulfsoccer.utilities
{
    public class TextConverter
    {
        public static IEnumerable<string> StringToParagraphs(string txt)
        {
            if (String.IsNullOrEmpty(txt))
            {
                return new List<string>();
            }
            return Regex.Split(txt, @"(\r\n?|\n){2}")
                             .Where(p => p.Any(char.IsLetterOrDigit));
        }

        public static IEnumerable<string> StringToWords(string txt)
        {
            List<string> words = new List<string>();
            foreach (var paragraph in StringToParagraphs(txt))
            {
                words.AddRange(paragraph.Split(new[] { ' ' },
                                      StringSplitOptions.RemoveEmptyEntries)
                                     .Select(w => w.Trim()));
                //do something
            }
            return words;
        }

        public static int GetWordsCount(string txt)
        {
            return StringToWords(txt).Count();
        }

    }
}
