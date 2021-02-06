using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace back_end_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public LoginController()
        {

        }

        [HttpPost]
        public int Post([FromBody] object objeto)
        {
            return 200;
        }


    }
}
