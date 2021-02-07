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

        public List<string> CriptografarLogin(LoginVM LoginSenha)
        {

            var LoginCriptografada = Convert.ToBase64String(Encoding.ASCII.GetBytes(ParseToMD5(LoginSenha.Login.Trim().Split('&'))));
            var SenhaCriptografada = Convert.ToBase64String(Encoding.ASCII.GetBytes(ParseToMD5(LoginSenha.Senha.Trim().Split('&'))));

            return new List<string>();
        }

        private string ParseToMD5(string[] Array)
        {
            StringBuilder retorno = new StringBuilder();

            foreach (var item in Array)
            {
                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(item));

                byte[] result = md5.Hash;

                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    retorno.Append(result[i].ToString("x2"));
                }
            }                                           
            return retorno.ToString();
        }


    }
}
