using AutoMapper;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
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
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IProjectRepository _iprojectRepository;
        private readonly AppDbContext _dbContext;
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IProjectRepository iprojectRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iprojectRepository = iprojectRepo;
            _dbContext = dbContext;
        }

        public ProjectViewModel GetById(int id)
        {
            var data = _iprojectRepository.FindById(id);
            return _mapper.Map<Project, ProjectViewModel>(data);
        }

        public List<ProjectViewModel> GetAll()
        {
            var model = _iprojectRepository.FindAll().ToList();
            return _mapper.Map<List<Project>, List<ProjectViewModel>>(model);
        }

        public PagedResult<ProjectViewModel> GetAllPaging(int page, int pageSize, string description)
        {
            var query = _iprojectRepository.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Project>, List<ProjectViewModel>>(query);

                var paginationSet = new PagedResult<ProjectViewModel>()
                {
                    Results = data,
                    CurrentPage = page,
                    RowCount = totalRow,
                    PageSize = pageSize
                };
                return paginationSet;
            }
            catch
            {
                return null;
            }
        }

        public Project Add(ProjectViewModel vm)
        {
            var entity = _mapper.Map<ProjectViewModel, Project>(vm);
            _iprojectRepository.Add(entity);
            SaveChanges();
            return entity;
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
