using AutoMapper;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Shared.Constants;
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
    public class RuleService : IRuleService
    {
        #region CONTRUCTOR

        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IRuleRepository _ruleRepository;

        public RuleService(IUnitOfWork unitOfWork, IMapper mapper, IRuleRepository ruleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ruleRepository = ruleRepository;
        }

        #endregion CONTRUCTOR

        #region GET

        public PagedResult<RuleViewModel> GetAllPaging(int page, int pageSize, int companyId)
        {
            try
            {
                var query = _ruleRepository.FindAll(x => x.DeleteFlag == StatusDelete.NON_DELETED && x.CompanyId == companyId).ToList();
                int totalRow = query.Count();
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
                return new PagedResult<RuleViewModel>()
                {
                    Results = new List<RuleViewModel>(),
                    CurrentPage = page,
                    RowCount = 0,
                    PageSize = pageSize
                }; ;
            }
        }

        public RuleViewModel GetById(int id)
        {
            var model = _ruleRepository.FindById(id);
            return _mapper.Map<Rule, RuleViewModel>(model);
        }

        #endregion GET

        #region POST
        public Rule Add(RuleViewModel Vm)
        {
            var entity = _mapper.Map<Rule>(Vm);
            _ruleRepository.Add(entity);
            SaveChanges();
            return entity;
        }

        #endregion POST

        #region PUT

        public void Update(RuleViewModel Vm)
        {
            var data = _mapper.Map<Rule>(Vm);
            _ruleRepository.Update(data);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion PUT

        #region DELETE

        public void Delete(int id)
        {
            var entity = _ruleRepository.FindById(id);
            entity.DeleteFlag = StatusDelete.DELETED;
            _ruleRepository.Update(entity);
            SaveChanges();
        }

        #endregion DELETE

        #region OTHER_METHOD

        #endregion OTHER_METHOD
    }
}
