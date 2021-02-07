using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using back_end_net_core_5.BusinesLogic;
using back_end_net_core_5.ViewModel;
using back_end_net_core_5.Utils.Enums;

namespace back_end_net_core_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ICriptografiaBll _criptografia;
        public LoginController(ICriptografiaBll criptografia)
        {
            _criptografia = criptografia;
        }

        [HttpPost]
        public ActionResult Post([FromBody] LoginVM objeto)
        {
            var teste = _criptografia.CriptografarLogin(objeto);
            return Ok(RestResponse.SUCCESS);
        }
    }
}
