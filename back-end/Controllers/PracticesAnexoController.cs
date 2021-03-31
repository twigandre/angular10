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
    public class PracticesAnexoController
    {
        IPracticesBll _practicesBll;
        public PracticesAnexoController(IPracticesBll practicesBll)
        {
            _practicesBll = practicesBll;
        }

        [HttpGet("{id}")]
        public List<PracticesAnexoVM> Get(int IdPractice)
        {
            return _practicesBll.EntityToVMPracticesAnexo(IdPractice);
        }

        [HttpDelete("{id}")]
        public Boolean Delete(int id)
        {
            var PracticesAuthorsVM = new PracticesAnexoVM { Id = id };
            return _practicesBll.ApagarAnexo(PracticesAuthorsVM);
        }
    }
}
