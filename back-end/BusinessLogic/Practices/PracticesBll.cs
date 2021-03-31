using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEPractices4ML.ViewModel;
using SEPractices4ML.Dao.Entityes;
using SEPractices4ML.Dao.Repository;
using SEPractices4ML.Utils.Enums;
using Microsoft.AspNetCore.Mvc;

namespace SEPractices4ML.BusinessLogic.Practices
{
    public class PracticesBll : IPracticesBll
    {
        IRepository<PracticesEntity> _repositoryPractices;
        IRepository<PracticesAuthorsEntity> _repositoryPracticesAuthors;
        IRepository<PracticesAnexoEntity> _repositoryPracticesAnexo;
        IRepository<MembersEntity> _repositoryMembersEntity;
        IRepository<UserEntity> _repositoryUserEntity;

        public PracticesBll(IRepository<PracticesEntity> repositoryPractices,
                            IRepository<PracticesAuthorsEntity> repositoryPracticesAuthors,
                            IRepository<PracticesAnexoEntity> repositoryPracticesAnexo,
                            IRepository<MembersEntity> repositoryMembersEntity,
                            IRepository<UserEntity> repositoryUserEntity)
        {
            _repositoryPractices = repositoryPractices;
            _repositoryPracticesAuthors = repositoryPracticesAuthors;
            _repositoryPracticesAnexo = repositoryPracticesAnexo;
            _repositoryMembersEntity = repositoryMembersEntity;
            _repositoryUserEntity = repositoryUserEntity;
        }

        public List<PracticesVM> ListarPractice(PracticesVM objeto)
        {
            var LisPracticesEntity = new List<PracticesEntity>();

            #region Listagem
            if (objeto.Name != null)
            {
                LisPracticesEntity = _repositoryPractices.Listar(o => o.Name.ToUpper().Contains(objeto.Name.ToUpper()));
            }
            else
            {
                LisPracticesEntity = _repositoryPractices.Listar();
            }
            #endregion

            if (LisPracticesEntity.Count() == 0)
                return null;

            var listaPracticeVM = new List<PracticesVM>();
            var practicesVM = new PracticesVM();

            foreach (var item in LisPracticesEntity)
            {              
                practicesVM.AuthorsPractice = EntityToVMPracticesAuthors(item.Id);
                practicesVM.ContribuitionTypes = item.ContribuitionTypes;
                practicesVM.Description = item.Description;
                practicesVM.Id = item.Id;
                practicesVM.IdUser = item.IdUser;
                practicesVM.Link = item.Link;
                practicesVM.Name = item.Name;
                practicesVM.OrganizationContext = item.OrganizationContext;
                practicesVM.ReferencesDescribing = item.ReferencesDescribing;
                practicesVM.SeKnowLedge = item.SeKnowLedge;
                practicesVM.TypesAiMlApplications = item.TypesAiMlApplications;
                listaPracticeVM.Add(practicesVM);
                practicesVM = new PracticesVM();
            }

            return listaPracticeVM;
        }

        public PracticesVM SelecionarPractice(int IdPractice)
        {
            var practiceEntity = _repositoryPractices.Selecionar(o => o.Id == IdPractice);

            return new PracticesVM {
                AnexosPractice = EntityToVMPracticesAnexo(practiceEntity.Id),
                AuthorsPractice = EntityToVMPracticesAuthors(practiceEntity.Id),
                ContribuitionTypes = practiceEntity.ContribuitionTypes,
                Description = practiceEntity.Description,
                Id = practiceEntity.Id,
                IdUser = practiceEntity.IdUser,
                Link = practiceEntity.Link,
                Name = practiceEntity.Name,
                OrganizationContext = practiceEntity.OrganizationContext,
                ReferencesDescribing = practiceEntity.ReferencesDescribing,
                SeKnowLedge = practiceEntity.SeKnowLedge,
                TypesAiMlApplications = practiceEntity.TypesAiMlApplications
            };
        }

        public Boolean SalvarPractices(PracticesVM objeto)
        {

            try
            {
                #region Salvar Practice

                    var practicesEntity = new PracticesEntity {
                        Id = objeto.Id,
                        Name = objeto.Name,
                        Description = objeto.Description,
                        TypesAiMlApplications = objeto.TypesAiMlApplications,
                        OrganizationContext = objeto.OrganizationContext,
                        SeKnowLedge = objeto.SeKnowLedge,
                        ContribuitionTypes = objeto.ContribuitionTypes,
                        Link = objeto.Link,
                        ReferencesDescribing = objeto.ReferencesDescribing, 
                        IdUser = objeto.IdUser
                    };

                    #region Add Or Update
                    if (objeto.Id > 0)
                    {
                        _repositoryPractices.Update(practicesEntity);
                    }
                    else
                    {
                        _repositoryPractices.Salvar(practicesEntity);
                    }
                    #endregion     
                    
                    _repositoryPractices.Context.SaveChanges();
                    objeto.Id = practicesEntity.Id;

                #endregion

                #region Salvar Anexos Practices
                    if (objeto.AnexosPractice.Count() > 0)
                    {
                        foreach (var item in objeto.AnexosPractice)
                        {
                            var practicesAnexoEntity = new PracticesAnexoEntity();

                            practicesAnexoEntity.Id = item.Id;
                            practicesAnexoEntity.Name = item.Name;
                            practicesAnexoEntity.ObjetoAnexo = item.ObjetoAnexo;
                            practicesAnexoEntity.IdPractice = practicesEntity.Id;
                            practicesAnexoEntity.ExtensaoAnexo = item.ExtensaoAnexo;

                            #region Add Or Update
                            if (item.Id > 0)
                            {
                                _repositoryPracticesAnexo.Update(practicesAnexoEntity);
                            }
                            else
                            {
                                _repositoryPracticesAnexo.Salvar(practicesAnexoEntity);
                            }
                            _repositoryPracticesAnexo.Context.SaveChanges();
                            #endregion

                            practicesAnexoEntity = new PracticesAnexoEntity();
                        }
                    }
                #endregion

                #region Salvar Author Practices
                    if (objeto.AuthorsPractice.Count() > 0)
                    {
                        foreach (var item in objeto.AuthorsPractice)
                        {
                            var practicesAuthorsEntity = new PracticesAuthorsEntity();

                            practicesAuthorsEntity.Id = item.Id;
                            practicesAuthorsEntity.IdPractice = practicesEntity.Id; 
                            practicesAuthorsEntity.IdUser = item.IdUser;

                            #region Add Or Update
                            if (item.Id > 0)
                            {
                                _repositoryPracticesAuthors.Update(practicesAuthorsEntity);
                            }
                            else
                            {
                                _repositoryPracticesAuthors.Salvar(practicesAuthorsEntity);
                            }
                            _repositoryPracticesAnexo.Context.SaveChanges();
                            #endregion

                            practicesAuthorsEntity = new PracticesAuthorsEntity();
                        }
                    }   
                #endregion

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }            
        }

        public Boolean DeletarPractice(int IdPractice)
        {
            try
            {
                #region Apagar Anexo Practices

                var ListAnexo = _repositoryPracticesAnexo.Listar(o => o.IdPractice == IdPractice);

                if(ListAnexo.Count() > 0)
                {
                    foreach(var item in ListAnexo)
                    {
                        _repositoryPracticesAnexo.Apagar(item);
                        _repositoryPracticesAnexo.Context.SaveChanges();
                    }
                }
                #endregion

                #region Apagar Authors Practices
                var ListAuthorsPractices = _repositoryPracticesAuthors.Listar(o => o.IdPractice == IdPractice);

                if (ListAuthorsPractices.Count() > 0)
                {
                    foreach (var item in ListAuthorsPractices)
                    {
                        _repositoryPracticesAuthors.Apagar(item);
                        _repositoryPracticesAuthors.Context.SaveChanges();
                    }
                }
                #endregion

                #region Apagar Practices
                var PracticesnEntity = new PracticesEntity { Id = IdPractice };
                _repositoryPractices.Apagar(PracticesnEntity);
                _repositoryPractices.Context.SaveChanges();
                #endregion

                return true;

            }catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<MembersVM> ListsAuthors()
        {
            var ListemMember = _repositoryMembersEntity.Listar();

            if (ListemMember.Count() == 0)
                return null;

            var listMember = new List<MembersVM>();
            var Membervm = new MembersVM();

            foreach(var item in ListemMember)
            {
                Membervm.Name = item.Name;
                Membervm.IdUsuario = item.IdUsuario;
                Membervm.ImagemMembro = _repositoryUserEntity.Selecionar(o => o.Id == item.IdUsuario).UserImage;
                listMember.Add(Membervm);
                Membervm = new MembersVM();
            }
            return listMember;
        }

        public List<PracticesAuthorsVM> EntityToVMPracticesAuthors(int IdPractice)
        {
            var ListpracticesAuthorsEntity = _repositoryPracticesAuthors.Listar(o => o.IdPractice == IdPractice);

            if (ListpracticesAuthorsEntity.Count() == 0)
                return null;

            var listPracticesAuthorsVM = new List<PracticesAuthorsVM>();
            var practicesAuthorsVM = new PracticesAuthorsVM();

            foreach (var item in ListpracticesAuthorsEntity)
            {
                #region Usuarios
                var consultaUsuario = _repositoryUserEntity.Selecionar(o => o.Id == item.IdUser);
                practicesAuthorsVM.Name = consultaUsuario?.Name;
                practicesAuthorsVM.UserImage = consultaUsuario?.UserImage;
                #endregion

                practicesAuthorsVM.Id = item.Id;
                practicesAuthorsVM.IdPractice = item.IdPractice;
                practicesAuthorsVM.IdUser = item.IdUser;
                listPracticesAuthorsVM.Add(practicesAuthorsVM);
                practicesAuthorsVM = new PracticesAuthorsVM();
            }
            return listPracticesAuthorsVM;
        }

        public List<PracticesAnexoVM> EntityToVMPracticesAnexo(int IdPractice)
        {
            var ListpracticesAnexoEntity = _repositoryPracticesAnexo.Listar(o => o.IdPractice == IdPractice);

            if (ListpracticesAnexoEntity.Count()==0)
                return null;

            var listPracticesAnexoVM = new List<PracticesAnexoVM>();
            var practicesAnexoVM = new PracticesAnexoVM();

            foreach(var item in ListpracticesAnexoEntity)
            {
                practicesAnexoVM.ExtensaoAnexo = item.ExtensaoAnexo;
                practicesAnexoVM.Id = item.Id;
                practicesAnexoVM.IdPractice = item.IdPractice;
                practicesAnexoVM.Name = item.Name;
                practicesAnexoVM.ObjetoAnexo = item.ObjetoAnexo;
                listPracticesAnexoVM.Add(practicesAnexoVM);
                practicesAnexoVM = new PracticesAnexoVM();
            }
            return listPracticesAnexoVM;
        }

        public Boolean ApagarAutor(PracticesAuthorsVM objeto)
        {
            var practicesAuthorsEntity = new PracticesAuthorsEntity();
            practicesAuthorsEntity.Id = objeto.Id;
            _repositoryPracticesAuthors.Apagar(practicesAuthorsEntity);
            _repositoryPracticesAuthors.Context.SaveChanges();
            return true;
        }

        public Boolean ApagarAnexo(PracticesAnexoVM objeto)
        {
            var practicesAnexoEntity = new PracticesAnexoEntity();
            practicesAnexoEntity.Id = objeto.Id;
            _repositoryPracticesAnexo.Apagar(practicesAnexoEntity);
            _repositoryPracticesAnexo.Context.SaveChanges();
            return true;            
        }

    }
}
