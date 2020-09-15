using System.Linq;

namespace Entities
{
    public class ContactInfo
    {
        private string _phone;
        private string _phonejob;
        private string _phonebad;
        
        public string Email { get; set; }
        public string EmailBad { get; set; }
        public string Phone {
            get => FormatPhone(_phone);
            set => _phone = value;
        }
        public string PhoneJob
        {
            get => FormatPhone(_phonejob);
            set => _phonejob = value;
        }
        public string PhoneBad
        {
            get => FormatPhone(_phonebad);
            set => _phonebad = value;
        }

        public string PhoneInner { get; set; }

        private static string FormatPhone(string unformattedPhone)
        {
            string formattedPhone = unformattedPhone;
            if (string.IsNullOrEmpty(unformattedPhone))
            {
                return unformattedPhone;
            }
            string phone = string.Concat(unformattedPhone.Where(c => char.IsDigit(c)));

            if (phone.Length == 10)
            {
                formattedPhone = string.Format("+7({0}){1}-{2}-{3}",
                       phone.Substring(0, 3),
                       phone.Substring(3, 3),
                       phone.Substring(6, 2),
                       phone.Substring(8, 2));
            }
            else if (phone.Length == 11 && (phone[0] == '8' || phone[0] == '7'))
            {
                formattedPhone = string.Format("+7({0}){1}-{2}-{3}",
                    phone.Substring(1, 3),
                    phone.Substring(4, 3),
                    phone.Substring(7, 2),
                    phone.Substring(9, 2));
            }
            else if (phone.Length == 12 && phone[0] == '+' && phone[1] == '7')
            {
                formattedPhone = string.Format("+7({0}){1}-{2}-{3}",
                    phone.Substring(2, 3),
                    phone.Substring(5, 3),
                    phone.Substring(8, 2),
                    phone.Substring(10, 2));
            }           
            else if (phone.Length == 7)
            {
                formattedPhone = string.Format("+7(000){0}-{1}-{2}", phone.Substring(0, 3), phone.Substring(3, 2), phone.Substring(5, 2));
            }
            return formattedPhone;
        }
    }
}