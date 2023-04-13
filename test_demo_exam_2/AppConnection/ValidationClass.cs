using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test_demo_exam_2.AppConnection
{
    class ValidationClass
    {
        public bool CheckStringData(string str, int minLength, int maxLength)
        {
            if(str.Length >= minLength && str.Length <= maxLength)
            {
                return true;
            }
            return false;
        }

        public bool CheckIntData(string number, int minValue)
        {
            try
            {
                int correctNumber = Int32.Parse(number);
                if(correctNumber >= minValue) 
                { 
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool CheckPassword(string password)
        {
            string pattern = "((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*])[0-9!@#$%^&*a-zA-Z]{6,50})";
            Match isMatch = Regex.Match(password, pattern);
            return isMatch.Success;
        }

        public bool CheckUniqueLogin(string login)
        {
            var user = AppConnect.modelDB.Users.FirstOrDefault(x => x.Login == login);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public bool CheckUniqueEmail(string email)
        {
            var user = AppConnect.modelDB.Users.FirstOrDefault(x => x.Email == email);
            if(user == null)
            {
                return true;
            }
            return false;
        }

        public bool CheckEmail(string email)
        {
            if (email.Length < 5 || email.Length > 250)
            {
                return false;
            }
            string pattern = "(^[0-9a-z.-_]+@[a-z]+\\.[a-z]+)";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public bool CheckPhone(string phone)
        {
            if(phone.Length != 11)
            {
                return false;
            }
            string pattern = "^8\\d{3}\\d{3}\\d{2}\\d{2}";
            Match isMatch = Regex.Match(phone, pattern);
            return isMatch.Success;
        }

        public bool CheckUniquePhone(string phone)
        {
            var user = AppConnect.modelDB.Users.FirstOrDefault(x => x.Phone == phone);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public bool CheckDateOfBirth(string date)
        {
            try
            {
                DateTime correctDate = DateTime.Parse(date);

                if (correctDate >= DateTime.Now.AddDays(-365 * 110) && correctDate <= DateTime.Now.AddDays(-365 * 14))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
