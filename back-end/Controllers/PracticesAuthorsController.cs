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
    public class PracticesAuthorsController
    {
        IPracticesBll _practicesBll;
        public PracticesAuthorsController(IPracticesBll practicesBll)
        {
            _practicesBll = practicesBll;
        }

        [HttpGet("{id}")]
        public List<PracticesAuthorsVM> Get(int IdPractice)
        {
            return _practicesBll.EntityToVMPracticesAuthors(IdPractice);
        }

        [HttpDelete("{id}")]
        public Boolean Delete(int id)
        {
        var PracticesAuthorsVM = new PracticesAuthorsVM { Id = id };
        return _practicesBll.ApagarAutor(PracticesAuthorsVM);
        }

    }
}
