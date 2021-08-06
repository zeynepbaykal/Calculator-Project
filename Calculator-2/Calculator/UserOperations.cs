using System.Collections.Generic;
using System.Linq;

namespace ZB.Calculator
{
    public class UserOperations
    {
        public static string CreateUser(string name, string password, string rePassword, string email)
        { 
            string userName = name.ToLower().Trim().Replace(" ", "");

            if (userName.Length > 20 || userName.Length < 3)
            {
                return "User name must be between 3 and 20 characters.";
            }
            else if (email.Length > 50 || email.Length < 6)
            {
                return "E mail must be between 6 and 50 characters.";
            }
            else if (email.Contains("@") == false)
            {
                return "Your e-mail address must contain the '@' character.";
            }
            else if (password.Length > 20 || password.Length < 8)
            {
                return "Password must be between 8 and 20 characters.";
            }
            else if (!password.Any(char.IsUpper))
            {
                return "Password must contain at least one capital case letter.";
            }
            else if (!password.Any(char.IsLower))
            {
                return "Password must contain at  least one lower case letter.";
            }
            else if (!password.Any(char.IsDigit))
            {
                return "Password must contain at least one diğit.";
            }
            else if (checkforspecialcaracter(password) == false)
            {
                return "özel karakter eklemyi unuttubz";
            }
            else if (password != rePassword)
            {
                return "passworler eşit değil";
            }
            else
            {
                string encriptedPassword = Md5Operations.CreateMD5(password);
                System.IO.File.AppendAllText("users.txt", $"{userName}:{email}:{encriptedPassword}" + System.Environment.NewLine);
                return "";
            }         
        }

        public static string CheckUser(string name, string password)
        {
            Dictionary<string, string> allUsers = new Dictionary<string, string>();

            string[] allUsersText = System.IO.File.ReadAllLines("users.txt");
            foreach (var user in allUsersText)
            {
                string[] userInformation = user.Split(':');
                allUsers.Add(userInformation[0], userInformation[2]);
            }
            if (allUsers.ContainsKey(name))
            {
                if (allUsers[name] == password)
                {
                    return string.Empty;
                }
                else
                {
                    return "şifre hatalı.";
                }
            }
            else
            {
                return  "böyle bir kullanıcı adı yok.";
            }
        }

        private static bool checkforspecialcaracter(string input)
        {
            char[] lstNotAllowedChars = new char[] { ',', '.', '%', '-', '&', '/', '!', '?', '=', '$', '\\', '*' };
            int result = input.IndexOfAny(lstNotAllowedChars, 0);
            return result > 0 == true;

        }
    }
}
