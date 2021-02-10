using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end_net_core_5.ViewModel;
using back_end_net_core_5.Dao.Entityes;
using back_end_net_core_5.Dao.Repository;
using back_end_net_core_5.Utils.Enums;
using Microsoft.AspNetCore.Mvc;

namespace back_end_net_core_5.BusinessLogic
{
    public class ControleUsuarioBll : IControleUsuarioBll
    {
        IUploadBucketBll _aws;
        ICriptografiaBll _criptografia;
        IRepository<LoginEntity> _loginEntity;

        public ControleUsuarioBll(ICriptografiaBll criptografia,
                                  IRepository<LoginEntity> loginEntity,
                                  IUploadBucketBll aws)
        {
            _aws = aws;
            _criptografia = criptografia;
            _loginEntity = loginEntity;
        }

        public int CreateUser(LoginVM objeto)
        {
            //_aws.UploadItemToBucket();

            LoginVM objetoCriptografado = _criptografia.CriptografarLogin(objeto);

            try
            {
                var LoginSenhaEntity = new LoginEntity {
                    Login = objetoCriptografado.Login,
                    Senha = objetoCriptografado.Senha
                };

                _loginEntity.Salvar(LoginSenhaEntity);
                _loginEntity.Context.SaveChanges();

                return (int)RestResponse.SUCCESS;
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);
                return (int)RestResponse.SERVER_ERROR;
            }             
        }

    }
}
