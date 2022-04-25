namespace SensitiveWordsAPI.Helper
{
    public static class StarOutSensitiveWord
    {
        public static string StarOutWord(string word)
        {
            var stared = word.Replace(word, "*");

            return stared;
        }
    }
}
