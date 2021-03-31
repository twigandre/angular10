using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEPractices4ML.ViewModel;
using SEPractices4ML.Dao.Entityes;
using SEPractices4ML.BusinessLogic.Notificacao;
using SEPractices4ML.Dao.Repository;
using SEPractices4ML.Utils.Enums;
using Microsoft.AspNetCore.Mvc;

namespace SEPractices4ML.BusinessLogic.User
{
    public class UserBll : IUserBll
    {
        IRepository<UserEntity> _repositoryUser;
        IRepository<MembersEntity> _repositoryMembers;
        INotificationBll _notificationBll;
        public UserBll(IRepository<UserEntity> repositoryUser,
                       IRepository<MembersEntity> repositoryMembers,
                       INotificationBll notificationBll)
        {
            _repositoryUser = repositoryUser;
            _repositoryMembers = repositoryMembers;
            _notificationBll = notificationBll;
        }

        public int SalvarUsuario(UserVM objeto)
        {
            try
            {

                #region Adicionar Novo Usuário

                //VALIDAÇÃO CADASTRO COM INTEGRAÇÃO GOOGLE E FACEBOOK
                if (objeto.Password == "Autentication with Google" || objeto.Password == "Autentication with Facebook") 
                {
                    var VerificaUsuarioCadastrado = _repositoryUser.Selecionar(o => o.Email == objeto.Email && o.Password == objeto.Password);

                    if (VerificaUsuarioCadastrado != null)
                    {
                        return (VerificaUsuarioCadastrado.Password == "Autentication with Google") ?
                                (int)RestResponse.ERROR_USUARIO_JA_CADASTRADO_COM_GOOGLE : 
                                    (int)RestResponse.ERROR_USUARIO_JA_CADASTRADO_COM_FACEBOOK;
                    }
                }
                else //VALIDAÇÃO DE CADASTRO - PREENCHIMENTO DO FORMULARIO
                {
                    var VerificaUsuarioCadastrado = _repositoryUser.Selecionar(o => o.Email == objeto.Email);
                    if (VerificaUsuarioCadastrado != null)
                       return (int)RestResponse.ERROR_EMAIL_JA_CADASTRADO;

                    var VerificaUsernameCadastrado = _repositoryUser.Selecionar(o => o.UserName == objeto.UserName);
                    if (VerificaUsernameCadastrado != null)
                        return (int)RestResponse.ERROR_USERNAME_JA_CADASTRADO;
                }

                var UserEntity = new UserEntity
                {
                    Email = objeto.Email,
                    Name = objeto.Name,
                    Password = objeto.Password,
                    UserImage = objeto.UserImage.ToString(),
                    UserName = objeto.UserName,
                    UsuarioHabilitado = (int?)EnumUsuarioHabilitado.HABILITADO
                };             
                   
                _repositoryUser.Salvar(UserEntity);                               
                _repositoryUser.Context.SaveChanges();

                #endregion

                #region Adicionar Novo Membro
                var MembersEntity = new MembersEntity
                {
                    Email = objeto.Email,
                    Name = objeto.Name,
                    IdUsuario = UserEntity.Id,
                    AnaliseFinalizada = (int)EnumAnaliseFinalizadaMembro.MEMBRO_PRECISA_ATUALIZAR_REGISTRO
                };
                _repositoryMembers.Salvar(MembersEntity);
                _repositoryMembers.Context.SaveChanges();
                #endregion

                #region Cadastrar Notificacao - Boas Vindas ao Usuário
                var notificationWelcomeVM = new NotificationVM
                {
                    DescricaoNotificacao = "Bem vindo SEPractices4ML! Acompanhe aqui as atividades envolvendo o seu usuario.",
                    IsConcluida = (int?)EnumConclusaoNotificacao.PENDENTE,
                    LevelNotification = (int?)EnumLevelNotification.USER,
                    TipoNotificacao = (int?)EnumTipoNotificaco.BOAS_VINDAS_AO_USUARIO,
                    IdUsuario = UserEntity.Id
                };
                _notificationBll.CadastrarNotificacao(notificationWelcomeVM);
                #endregion

                return (int)RestResponse.SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (int)RestResponse.SERVER_ERROR;
            }
        }

        public UserVM GetUserId(int IdUser)
        {
            var UserEntity = _repositoryUser.Selecionar(o => o.Id == IdUser);

            if (UserEntity == null)
                return null;

            return new UserVM
            {
                Id = UserEntity.Id,
                Email = UserEntity.Email,
                Name = UserEntity.Name,
                Password = UserEntity.Password,
                UserImage = UserEntity.UserImage,
                UserName = UserEntity.UserName
            };           

        }

        public UserVM GetUser(UserVM objeto)
        {
            var returnVM = new UserVM();

            var UserEntity = _repositoryUser.Selecionar(o => o.Email == objeto.Email && 
                                                             o.Password == objeto.Password);
            if (UserEntity == null)
            {
               returnVM.RequesteResponse = (int?)RestResponse.NOT_FOUND;
               return returnVM;
            }

            return new UserVM {
                Id = UserEntity.Id,
                Email = UserEntity.Email,
                Name = UserEntity.Name,
                Password = UserEntity.Password,
                UserImage = UserEntity.UserImage,
                UserName = UserEntity.UserName,
                RequesteResponse = (int?)RestResponse.SUCCESS,
                TipoUsuario = UserEntity.TipoUsuario,
                UsuarioHabilitado = UserEntity.UsuarioHabilitado
            };
        }

    }
}
