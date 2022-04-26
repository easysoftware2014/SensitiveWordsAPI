using System.Text;

namespace SensitiveWordsAPI.Helper
{
    public static class StarOutSensitiveWord
    {
        public static string StarOutWord(string word)
        {
            var length = word.Length;
            var masked = new StringBuilder();
            for(var i=0;i < length;i++)
            {
                masked.Append("*");
            }

            return masked.ToString();
        }
    }
}
