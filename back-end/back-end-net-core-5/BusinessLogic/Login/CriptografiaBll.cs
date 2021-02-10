using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using back_end_net_core_5.ViewModel;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;

namespace back_end_net_core_5.BusinessLogic
{
    public class CriptografiaBll : ICriptografiaBll
    { 
        MD5 md5 = new MD5CryptoServiceProvider();

        public LoginVM CriptografarLogin(LoginVM LoginSenha)
        {
            LoginSenha.Login = Convert.ToBase64String(Encoding.ASCII.GetBytes(ParseToArgon2(ParseToMD5(LoginSenha.Login.Trim().Split('&')))));
            LoginSenha.Senha = Convert.ToBase64String(Encoding.ASCII.GetBytes(ParseToArgon2(ParseToMD5(LoginSenha.Senha.Trim().Split('&')))));

            return LoginSenha;
        }

        private string ParseToMD5(string[] Array)
        {
            StringBuilder stringBuildMd5 = new StringBuilder();

            foreach (var item in Array)
            {
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(item));

                byte[] result = md5.Hash;

                for (int i = 0; i < result.Length; i++)
                {
                    stringBuildMd5.Append(result[i].ToString("x2"));
                }
            }

            return stringBuildMd5.ToString();
        }

        private string ParseToArgon2(string hashMd5)
        {
            var salt = CreateSalt();
            var hash = HashPassword(hashMd5, salt);

            StringBuilder stringBuilderArgon2 = new StringBuilder();

            foreach (var item in hash)
            {
                stringBuilderArgon2.Append(item.ToString("x2"));
            }

            return stringBuilderArgon2.ToString();
        }

        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }



    }
}
