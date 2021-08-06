using System;
using System.IO;

namespace ZB.Calculator
{
    public class HistoryOperations
    {
        public static void AddToHistory(string userName, string line)
        {
            var appendedText = line + Environment.NewLine + File.ReadAllText(userName + "_history.txt");
            File.WriteAllText(userName + "_history.txt", appendedText);
        }

        public static string[] ReadFromHistory(string userName)
        {
            if (File.Exists(userName + "_history.txt"))
                return File.ReadAllLines(userName + "_history.txt");
            else
                return new string[] { };
        }
    }
}
