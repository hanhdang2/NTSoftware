using AutoMapper;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Dtos;
using NTSoftware.Core.Shared.Interface;
using NTSoftware.Repository;
using NTSoftware.Repository.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace NTSoftware.Service
{
    public class RuleService: IRuleService
    {
        #region Contructor
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IRuleRepository _iruleRepository;
        private readonly AppDbContext _dbContext;

        public RuleService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext, IRuleRepository iruleRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _iruleRepository = iruleRepo;
            _dbContext = dbContext;
        }
        #endregion Contructor
        public List<RuleViewModel> GetAll()
        {
            var data = _iruleRepository.FindAll().ToList();
            return _mapper.Map<List<Rule>, List<RuleViewModel>>(data);
        }

        public PagedResult<RuleViewModel> GetAllPaging(int page, int pageSize)
        {
            var query = _iruleRepository.FindAll().ToList();
            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<Rule>, List<RuleViewModel>>(query);

                var paginationSet = new PagedResult<RuleViewModel>()
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

        public RuleViewModel GetById(int id)
        {
            var model = _iruleRepository.FindById(id);
            return _mapper.Map<Rule, RuleViewModel>(model);
        }

        public Rule Add(RuleViewModel Vm)
        {
            var entity = _mapper.Map<Rule>(Vm);
            _iruleRepository.Add(entity);
            SaveChanges();
            return entity;
        }
        public void Update(RuleViewModel Vm)
        {
            var data = _mapper.Map<Rule>(Vm);
            _iruleRepository.Update(data);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
