using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SEPractices4ML.BusinessLogic.Notificacao;
using SEPractices4ML.ViewModel;
using SEPractices4ML.Utils.Enums;

namespace SEPractices4ML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        INotificationBll _notificationBll;

        public NotificationController(INotificationBll notificationBll)
        {
            _notificationBll = notificationBll;
        }

        [HttpPost]
        public int Post([FromBody] NotificationVM objeto)
        {
            return _notificationBll.AtualizarNotificacao(objeto);
        }

        [HttpGet]
        public List<NotificationVM> Get([FromQuery] NotificationVM objeto)
        {
            return _notificationBll.ListarNotificacoesPorUsuarioLogado((int)objeto.IdUsuario);
        }

    }
}
