using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SEPractices4ML.BusinessLogic.User;
using SEPractices4ML.ViewModel;
using SEPractices4ML.Utils.Enums;

namespace SEPractices4ML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserBll _userBll;
        public UserController(IUserBll userBll)
        {
            _userBll = userBll;
        }

        [HttpPost]
        public int Post([FromBody] UserVM objeto)
        {
           return _userBll.SalvarUsuario(objeto);
        }

        [HttpGet("{id}")]
        public UserVM Get(int id)
        {
            return _userBll.GetUserId(id);
        }

        [HttpGet]
        public UserVM Get([FromQuery] UserVM objeto)
        {
            return _userBll.GetUser(objeto);
        }

    }
}
