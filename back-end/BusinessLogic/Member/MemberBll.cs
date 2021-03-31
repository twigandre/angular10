using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEPractices4ML.ViewModel;
using SEPractices4ML.BusinessLogic.User;
using SEPractices4ML.Dao.Entityes;
using SEPractices4ML.Dao.Repository;
using SEPractices4ML.Dao;
using SEPractices4ML.Utils.Enums;
using Microsoft.AspNetCore.Mvc;

namespace SEPractices4ML.BusinessLogic.Member
{
    public class MemberBll : IMemberBll
    {
        private readonly Context _context; 
        IRepository<MembersEntity> _repositoryMembers;
        IRepository<UserEntity> _repositoryUser;
        IUserBll _userBll;

        public MemberBll(Context context,
                         IRepository<MembersEntity> repositoryMembers, 
                         IRepository<UserEntity> repositoryUser,
                         IUserBll userBll)
        {
            _context = context;
            _repositoryMembers = repositoryMembers;
            _repositoryUser = repositoryUser;
            _userBll = userBll;
        }

        public int UpdateMember(MembersVM objeto)
        {
            try
            {
                var memberEntity = new MembersEntity {
                    AreaActuationRole = objeto.AreaActuationRole,
                    CurrentlyWork = objeto.CurrentlyWork,
                    Degree = objeto.Degree,
                    Email = objeto.Email,
                    IdUsuario = objeto.IdUsuario,
                    Id = objeto.Id,
                    Name = objeto.Name,
                    Organization = objeto.Organization,
                    WebSite = objeto.WebSite, 
                    AnaliseFinalizada = objeto.AnaliseFinalizada
                };
                //_context.Members.
                _repositoryMembers.Update(memberEntity);
                _repositoryMembers.Context.SaveChanges();

                return (int)RestResponse.SUCCESS;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (int)RestResponse.SERVER_ERROR;
            }
        }

        public MembersVM GetMember(int IdUsuario)
        {
            var result = _repositoryMembers.Selecionar(o => o.IdUsuario == IdUsuario);

            if (result == null)
                return null;

            return new MembersVM {
                AreaActuationRole = result.AreaActuationRole,
                CurrentlyWork = result.CurrentlyWork,
                Degree = result.Degree,
                Email = result.Email,
                IdUsuario = result.IdUsuario,
                Id = result.Id,
                Name = result.Name,
                Organization = result.Organization,
                WebSite = result.WebSite,
                AnaliseFinalizada = result.AnaliseFinalizada,
                User = _userBll.GetUserId(IdUsuario)
            };
        }

        public List<MembersVM> ListMembers()
        {            
            var EntityList = _repositoryMembers.Listar();

            if (EntityList.Count() == 0)
                return null;
            
            return EntityToVMMember(EntityList);
        }

        public List<MembersVM> SelectMember(MembersVM objeto)
        {
           
            var EntityList = _repositoryMembers.Listar(o=> o.Name.ToUpper().Contains(objeto.Name.ToUpper()));

            if (EntityList.Count() == 0)
                return null;           

            return EntityToVMMember(EntityList);
        }

        private List<MembersVM> EntityToVMMember(List<MembersEntity> objeto)
        {
            var listMembers = new List<MembersVM>();
            var membersVM = new MembersVM();

            #region Iteração
            foreach (var item in objeto)
            {
                membersVM.AreaActuationRole = item.AreaActuationRole;
                membersVM.CurrentlyWork = item.CurrentlyWork;
                membersVM.Degree = item.Degree;
                membersVM.Email = item.Email;
                membersVM.IdUsuario = item.IdUsuario;
                membersVM.Id = item.Id;
                membersVM.Name = item.Name;
                membersVM.Organization = item.Organization;
                membersVM.WebSite = item.WebSite;
                membersVM.AnaliseFinalizada = item.AnaliseFinalizada;
                membersVM.User = _userBll.GetUserId(item.IdUsuario);

                listMembers.Add(membersVM);
                membersVM = new MembersVM();
            }
            #endregion

            return listMembers.OrderBy(o => o.Name).ToList();
        }

    }
}
