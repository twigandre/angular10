using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using back_end_net_core_5.BusinessLogic;
using back_end_net_core_5.ViewModel;
using back_end_net_core_5.Utils.Enums;

namespace back_end_net_core_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ICriptografiaBll _criptografia;
        IControleUsuarioBll _controleUsuario;
        public LoginController(ICriptografiaBll criptografia,
                               IControleUsuarioBll controleUsuario)
        {
            _criptografia = criptografia;
            _controleUsuario = controleUsuario;
        }

        [HttpPost]
        public ActionResult Post([FromBody] LoginVM objeto)
        {
            return StatusCode(_controleUsuario.CreateUser(objeto));
        }
    }
}
