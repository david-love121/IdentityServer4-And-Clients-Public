using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
namespace IDSEmpty.Crypto
{
    public class SaltandHash
    {
    public string Hash(string Password, string Salt)
        {
            byte[] SBytes = Encoding.ASCII.GetBytes(Password + Salt);
            SHA256 SHA = SHA256.Create();
            byte[] Hash = SHA.ComputeHash(SBytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Hash.Length; i++)
            {
                builder.Append(Hash[i].ToString("x2"));
            }
            return builder.ToString();
        }
        public Tuple<string, string> ComputeSH(string Password)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            string Salt = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            byte[] SBytes = Encoding.UTF8.GetBytes(Password + Salt);
            SHA256 SHA = SHA256.Create();
            byte[] Hash = SHA.ComputeHash(SBytes);
           
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Hash.Length; i++)
            {
                builder.Append(Hash[i].ToString("x2"));
            }
            string HashedString2 = builder.ToString();
            
            return Tuple.Create<string, string>(HashedString2, Salt);
            
        }
    }
}
