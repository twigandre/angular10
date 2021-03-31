using System;
using System.Collections.Generic;
using System.Linq;
using SEPractices4ML.ViewModel;
using System.Threading.Tasks;

namespace SEPractices4ML.BusinessLogic.User
{
    public interface IUserBll
    {
        public int SalvarUsuario(UserVM objeto);
        public UserVM GetUserId(int IdUser);
        public UserVM GetUser(UserVM objeto);
    }
}
