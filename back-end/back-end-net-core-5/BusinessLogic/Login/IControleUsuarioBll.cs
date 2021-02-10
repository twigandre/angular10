using System;
using System.Collections.Generic;
using System.Linq;
using back_end_net_core_5.ViewModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace back_end_net_core_5.BusinessLogic
{
    public interface IControleUsuarioBll
    {
        int CreateUser(LoginVM objeto);
    }
}
