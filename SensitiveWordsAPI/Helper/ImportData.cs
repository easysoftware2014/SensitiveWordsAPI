using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SensitiveWordsAPI.Helper
{
    public static class ImportData
    {
        public static string[] ReadFile()
        {

            string[] lines;
            var list = new List<string>();
            var fileStream = new FileStream(@"sql_sensitive_list.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            lines = list.ToArray();

            return lines;
        }
    }
}
