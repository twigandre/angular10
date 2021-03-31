using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPractices4ML.ViewModel
{
    public class PracticesAuthorsVM
    {
        public int Id { get; set; }
        public int IdPractice { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string TipoOperacao { get; set; }
        public string UserImage { get; set; }
    }
}
