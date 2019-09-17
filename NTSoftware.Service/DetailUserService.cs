using AutoMapper;
using NTSoftware.Core.Models.Models;
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
using System.Text.RegularExpressions;

namespace NTSoftware.Service
{
    public class DetailUserService : IDetailUserService
    {
        #region Contructor
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IDetailUserRepository _detailUserRepository;
        private readonly AppDbContext _dbContext;

        public DetailUserService(IUnitOfWork unitOfWork, IMapper mapper, IDetailUserRepository detailUserRepository, AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _detailUserRepository = detailUserRepository;
            _dbContext = dbContext;
        }



        #endregion Contructor

        public List<DetailUserViewModel> GetAll()
        {
            var data = _detailUserRepository.FindAll().ToList();
            return _mapper.Map<List<DetailUser>, List<DetailUserViewModel>>(data);
        }

        public PagedResult<DetailUserViewModel> GetAllPaging(int page, int pageSize, string name, string phonenumber)
        {
            string nameUnSign = ConvertToUnSign(name);
            string phoneUnSign = ConvertToUnSign(phonenumber);
            var query = _detailUserRepository.GetAll().
                Where(x => ConvertToUnSign(x.Name).Contains(nameUnSign) &&
                ConvertToUnSign(x.PhoneNumber).Contains(phoneUnSign)).AsQueryable();

            int totalRow = query.Count();

            try
            {
                var data = _mapper.Map<List<DetailUser>, List<DetailUserViewModel>>(query.ToList());

                var paginationSet = new PagedResult<DetailUserViewModel>()
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

        public DetailUserViewModel GetById(int id)
        {
            var model = _detailUserRepository.FindById(id);
            return _mapper.Map<DetailUser, DetailUserViewModel>(model);
        }

        public DetailUser Add(DetailUserViewModel Vm)
        {
            var entity = _mapper.Map<DetailUser>(Vm);
            _detailUserRepository.Add(entity);
            SaveChanges();
            return entity;
        }
        public void Update(DetailUserViewModel Vm)
        {
            var data = _mapper.Map<DetailUser>(Vm);
            _detailUserRepository.Update(data);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #region Private Method

        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        #endregion Private Method
    }
}
