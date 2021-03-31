using System;
using System.Collections.Generic;
using System.Linq;
using SEPractices4ML.ViewModel;
using System.Threading.Tasks;

namespace SEPractices4ML.BusinessLogic.Member
{
    public interface IMemberBll
    {
        public int UpdateMember(MembersVM objeto);
        public MembersVM GetMember(int IdUsuario);
        public List<MembersVM> ListMembers();
        public List<MembersVM> SelectMember(MembersVM objeto);

    }
}
