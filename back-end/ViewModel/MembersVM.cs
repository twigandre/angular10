using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPractices4ML.ViewModel
{
    public class MembersVM
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Degree { get; set; }

        public string Organization { get; set; }

        public string CurrentlyWork { get; set; }

        public string AreaActuationRole { get; set; }

        public int? AnaliseFinalizada { get; set; }

        public string WebSite { get; set; }

        public UserVM User { get; set; }

        #region Imagem Membro
        public string ImagemMembro { get; set; }
        #endregion

    }
}
