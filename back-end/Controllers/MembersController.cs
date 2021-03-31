using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SEPractices4ML.BusinessLogic.Member;
using SEPractices4ML.ViewModel;
using SEPractices4ML.Utils.Enums;

namespace SEPractices4ML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        IMemberBll _memberBll;

        public MembersController(IMemberBll memberBll)
        {
            _memberBll = memberBll;
        }

        [HttpPost]
        public int Post([FromBody] MembersVM objeto)
        {
            return _memberBll.UpdateMember(objeto);
        }

        [HttpGet("{id}")]
        public MembersVM Get(int id)
        {
            return _memberBll.GetMember(id);
        }

        [HttpGet]
        public List<MembersVM> Get()
        {
            return _memberBll.ListMembers();
        }

        [HttpGet]
        [Route("[action]")]
        public List<MembersVM> GetMemberName([FromQuery] MembersVM objeto)
        {
            return _memberBll.SelectMember(objeto);
        }

    }
}
