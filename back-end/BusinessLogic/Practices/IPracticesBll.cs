using System;
using System.Collections.Generic;
using System.Linq;
using SEPractices4ML.ViewModel;
using System.Threading.Tasks;

namespace SEPractices4ML.BusinessLogic.Practices
{
    public interface IPracticesBll
    {
        List<PracticesVM> ListarPractice(PracticesVM objeto);
        Boolean SalvarPractices(PracticesVM objeto);
        PracticesVM SelecionarPractice(int IdPractice);
        Boolean DeletarPractice(int IdPractice);
        public List<PracticesAuthorsVM> EntityToVMPracticesAuthors(int IdPractice);
        public List<PracticesAnexoVM> EntityToVMPracticesAnexo(int IdPractice);
        public List<MembersVM> ListsAuthors();
        public Boolean ApagarAutor(PracticesAuthorsVM objeto);
        public Boolean ApagarAnexo(PracticesAnexoVM objeto);
    }
}
