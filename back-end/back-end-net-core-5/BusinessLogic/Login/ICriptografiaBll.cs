using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end_net_core_5.ViewModel;

namespace back_end_net_core_5.BusinesLogic
{
    public interface ICriptografiaBll
    {
        List<string> CriptografarLogin(LoginVM LoginSenha);
    }
}
