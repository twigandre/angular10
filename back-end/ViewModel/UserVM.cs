using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPractices4ML.ViewModel
{
    public class UserVM
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string UserImage { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }
        
        public int? TipoUsuario { get; set; }

        public int? UsuarioHabilitado { get; set; }

        public int? RequesteResponse { get; set; }
    }
}
