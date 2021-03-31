using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEPractices4ML.ViewModel;
using SEPractices4ML.Dao.Entityes;
using SEPractices4ML.Dao.Repository;
using SEPractices4ML.Utils.Enums;

namespace SEPractices4ML.BusinessLogic.Notificacao
{
    public class NotificationBll : INotificationBll
    {
        IRepository<NotificationEntity> _repositoryNotification;
        IRepository<UserEntity> _repositoryUser;

        public NotificationBll(IRepository<NotificationEntity> repositoryNotification,
                               IRepository<UserEntity> repositoryUser)
        {
            _repositoryNotification = repositoryNotification;
            _repositoryUser = repositoryUser;
        }

        public void CadastrarNotificacao(NotificationVM objeto)
        {
            var NotificationEntity = new NotificationEntity { 
                DescricaoNotificacao = objeto.DescricaoNotificacao,
                IdUsuario = objeto.IdUsuario,
                IsConcluida = objeto.IsConcluida,
                LevelNotification = objeto.LevelNotification,
                TipoNotificacao = objeto.TipoNotificacao
            };
            _repositoryNotification.Salvar(NotificationEntity);
            _repositoryNotification.Context.SaveChanges();
        }

        public int AtualizarNotificacao(NotificationVM objeto)
        {
            var notificacaoEntity = _repositoryNotification.Selecionar(o => o.Id == objeto.Id);

            notificacaoEntity = new NotificationEntity
            {
                Id = objeto.Id,
                DescricaoNotificacao = objeto.DescricaoNotificacao,
                IdUsuario = objeto.IdUsuario,
                IsConcluida = objeto.IsConcluida,
                LevelNotification = objeto.LevelNotification,
                TipoNotificacao = objeto.TipoNotificacao
            };
            
            _repositoryNotification.Update(notificacaoEntity);
            _repositoryNotification.Context.SaveChanges();
            return (int)RestResponse.SUCCESS;
        }

        public List<NotificationVM> ListarNotificacoesPorUsuarioLogado(int IdUsuario)
        {
            var UsuarioEntity = _repositoryUser.Selecionar(o => o.Id == IdUsuario);

            var notificacoesEntity = new List<NotificationEntity>();

            if (UsuarioEntity.TipoUsuario == (int?)EnumTipoUsuario.ADMINISTRADOR)
            {
                notificacoesEntity = _repositoryNotification.Listar(o => (o.LevelNotification == (int?)EnumLevelNotification.ADMIN)
                                                                      || (o.LevelNotification == (int?)EnumLevelNotification.USER && o.IdUsuario == IdUsuario)
                                                                      && o.IsConcluida == (int?)EnumConclusaoNotificacao.PENDENTE);
            }
            else //EnumTipoUsuario.COMUM
            {
                notificacoesEntity = _repositoryNotification.Listar(o => o.IdUsuario == IdUsuario
                                                                      && o.LevelNotification == (int?)EnumLevelNotification.USER
                                                                      && o.IsConcluida == (int?)EnumConclusaoNotificacao.PENDENTE);
            }            

            if (notificacoesEntity.Count == 0)
                return null;

            return EntityToVMNotification(notificacoesEntity);
        }


        private List<NotificationVM> EntityToVMNotification(List<NotificationEntity> objeto)
        {
            var listNotification = new List<NotificationVM>();
            var notificationVM = new NotificationVM();

            #region Iteração
            foreach (var item in objeto)
            {
                notificationVM.DescricaoNotificacao = item.DescricaoNotificacao;
                notificationVM.IdUsuario = item.IdUsuario;
                notificationVM.IsConcluida = item.IsConcluida;
                notificationVM.LevelNotification = item.LevelNotification;
                notificationVM.TipoNotificacao = item.TipoNotificacao;
                notificationVM.Id = item.Id;
                listNotification.Add(notificationVM);
                notificationVM = new NotificationVM();
            }
            #endregion

            return listNotification.OrderByDescending(o => o.Id).ToList();
        }
    }
}
