using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SEPractices4ML.BusinessLogic.Practices;
using SEPractices4ML.ViewModel;
using SEPractices4ML.Utils.Enums;

namespace SEPractices4ML.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticesController : Controller
    {
        IPracticesBll _practicesBll;
        public PracticesController(IPracticesBll practicesBll)
        {
            _practicesBll = practicesBll;
        }

        [HttpGet]
        public List<PracticesVM> Get([FromQuery] PracticesVM objeto)
        {
            return _practicesBll.ListarPractice(objeto);
        }

        [HttpGet]
        [Route("[action]")]
        public List<PracticesAnexoVM> GetPracticesAnexo([FromQuery] PracticesVM objeto)
        {
            return _practicesBll.EntityToVMPracticesAnexo(objeto.Id);
        }

        [HttpGet]
        [Route("[action]")]
        public List<MembersVM> GetAuthors()
        {
            return _practicesBll.ListsAuthors();
        }

        [HttpGet("{id}")]
        public PracticesVM Get(int id)
        {
            return _practicesBll.SelecionarPractice(id);
        }

        [HttpPost]
        public Boolean Post([FromBody] PracticesVM objeto)
        {
            return _practicesBll.SalvarPractices(objeto);
        }

        [HttpDelete("{id}")]
        public Boolean Delete(int id)
        {
            return _practicesBll.DeletarPractice(id);
        }
    }
}
