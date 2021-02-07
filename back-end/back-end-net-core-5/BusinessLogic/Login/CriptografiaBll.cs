using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using back_end_net_core_5.ViewModel;
using System.Security.Cryptography;

namespace back_end_net_core_5.BusinesLogic
{
    public class CriptografiaBll : ICriptografiaBll
    {

        MD5 md5 = new MD5CryptoServiceProvider();

        public LoginVM CriptografarLogin(LoginVM LoginSenha)
        {
            LoginSenha.Login = Convert.ToBase64String(Encoding.ASCII.GetBytes(ParseToMD5(LoginSenha.Login.Trim().Split('&'))));
            LoginSenha.Senha = Convert.ToBase64String(Encoding.ASCII.GetBytes(ParseToMD5(LoginSenha.Senha.Trim().Split('&'))));
            return LoginSenha;
        }

        private string ParseToMD5(string[] Array)
        {
            StringBuilder retorno = new StringBuilder();

            foreach (var item in Array)
            {
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(item));

                byte[] result = md5.Hash;

                for (int i = 0; i < result.Length; i++)
                {
                    retorno.Append(result[i].ToString("x2"));
                }
            }                                           
            return retorno.ToString();
        }


    }
}
