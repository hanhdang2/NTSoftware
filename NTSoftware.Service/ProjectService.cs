using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Helper;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Repository;
using NTSoftware.Repository.Interface;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTSoftware.Service
{
    public class ProjectService : IProjectService
    {
        #region CONTRUCTOR

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IProjectRepository _projectRepository;
        private readonly UserManager<AppUser> _userManager;
        private IDetailUserRepository _detailUserRepository;
        private IEmployeeProjectRepository _employeeProjectRepository;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper, IProjectRepository projectRepository, UserManager<AppUser> userManager, IDetailUserRepository detailUserRepository, IEmployeeProjectRepository employeeProjectRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _userManager = userManager;
            _detailUserRepository = detailUserRepository;
            _employeeProjectRepository = employeeProjectRepository;
        }

        #endregion CONTRUCTOR

        #region GET

        public ProjectDetailViewModel GetById(int id, int companyId)
        {
            var project = _projectRepository.FindById(id, x => x.DeleteFlag == StatusDelete.NON_DELETED && x.CompanyId == companyId);
            if (project == null)
            {
                return null;
            }
            var data = _mapper.Map<Project, ProjectDetailViewModel>(project);
            var employeeDetail = _detailUserRepository.FindAll(x => x.DeleteFlag == StatusDelete.NON_DELETED);
            var projectEmployee = _employeeProjectRepository.FindAll(x => x.DeleteFlag == StatusDelete.NON_DELETED);
            var query = (from e in employeeDetail
                         join p in projectEmployee on e.Id equals p.UserID
                         select new DetailUserViewModel()
                         {
                             Address = e.Address,
                             Id = e.Id,
                             Birthday = e.Birthday,
                             EmployeeKey = e.EmployeeKey,
                             Gender = e.Gender,
                             IdentityCard = e.IdentityCard,
                             Name = e.Name,
                             PhoneNumber = e.PhoneNumber
                         }).ToList();
            data.lstEmployee = query;
            return data;
        }

        public PagedResult<ProjectViewModel> GetAllPaging(int page, int pageSize, int companyId, string description)
        {

            try
            {
                var project = _projectRepository.FindAll(x => x.CompanyId == companyId && x.DeleteFlag == StatusDelete.NON_DELETED);
                var users = _detailUserRepository.FindAll( x=>x.DeleteFlag == StatusDelete.NON_DELETED);
                var employeeProject = _employeeProjectRepository.FindAll(x => x.DeleteFlag == StatusDelete.NON_DELETED); 

                var query = (from p in project                            
                             select new ProjectViewModel()
                             {
                                 Id = p.Id,
                                 CompanyId = p.CompanyId,
                                 Description = p.Description,
                                 EndDate = p.EndDate,
                                 ManagerId = p.ManagerId,
                                 ManagerName = users.SingleOrDefault(x=>x.Id == p.ManagerId).Name,
                                 ProjectName = p.ProjectName,
                                 StartDate = p.StartDate,
                                 employeeCount = employeeProject.Where(x=>x.ProjectId == p.Id).Count(),

                             }).ToList();
                var paginationSet = new PagedResult<ProjectViewModel>()
                {
                    Results = query,
                    CurrentPage = page,
                    RowCount = query.Count(),
                    PageSize = pageSize
                };
                return paginationSet;
            }
            catch
            {
                return new PagedResult<ProjectViewModel>()
                {
                    Results = new List<ProjectViewModel>(),
                    CurrentPage = page,
                    RowCount = 0,
                    PageSize = pageSize
                };
            }
        }

        #endregion GET

        #region POST
        public Project Add(ProjectDetailViewModel vm)
        {
            var entity = _mapper.Map<ProjectDetailViewModel, Project>(vm);
            _projectRepository.Add(entity);
            SaveChanges();
            if(vm.lstEmployee.Count > 0)
            {
                foreach (var item in vm.lstEmployee)
                {
                    if (_detailUserRepository.Find(x=>x.Id == item.Id) != null)
                    {
                        _employeeProjectRepository.Add(new EmployeeProject() { UserID = item.Id, ProjectId = entity.Id, JoinDate = DateTime.Now });
                    }
                }
                SaveChanges();
            }
            return entity;
        }

        #endregion POST

        #region PUT

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProjectDetailViewModel Vm)
        {
            var data = _mapper.Map<Project>(Vm);
            _projectRepository.Update(data);
            SaveChanges();
        }

        #endregion PUT

        #region DELETE

        public void Delete(int id)
        {
            var entity = _projectRepository.FindById(id);
            _projectRepository.RemoveFlg(entity);
            SaveChanges();
        }

        #endregion DELETE

        #region OTHER_METHOD

        #endregion OTHER_METHOD
    }
}
