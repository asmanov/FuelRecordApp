using System.Text;
using System.Security.Cryptography;
using System.Data.Common;
using RefuelingLibrary;

namespace UserLibrary
{
    public class Person
    {
        private string password;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password 
        {
            get { return password; }
            set
            { 
                password = GetSHA256(value, Name);
            }
                
        }
        public Location? Location { get; set; }

        public Person()
        {

        }

        static string GetSHA256(string data, string addData = "")
        {
            SHA256Managed crypt = new SHA256Managed();
            byte[] bytes = Encoding.UTF8.GetBytes(addData + data + addData);
            byte[] bytesHash = crypt.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (var item in bytesHash)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }

    }
}