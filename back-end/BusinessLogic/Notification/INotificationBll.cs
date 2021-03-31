using System;
using System.Collections.Generic;
using System.Linq;
using SEPractices4ML.ViewModel;
using System.Threading.Tasks;

namespace SEPractices4ML.BusinessLogic.Notificacao
{ 
    public interface INotificationBll
    {
        public List<NotificationVM> ListarNotificacoesPorUsuarioLogado(int IdUsuario);
        public void CadastrarNotificacao(NotificationVM objeto);
        public int AtualizarNotificacao(NotificationVM objeto);
    }
}
