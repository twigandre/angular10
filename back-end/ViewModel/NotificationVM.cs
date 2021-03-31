using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPractices4ML.ViewModel
{
    public class NotificationVM
    {
        public int Id { get; set; }

        public int? IdUsuario { get; set; }
                
        public String DescricaoNotificacao { get; set; }

        public int? LevelNotification { get; set; }

        public int? IsConcluida { get; set; }

        public int? TipoNotificacao { get; set; }
    }
}
