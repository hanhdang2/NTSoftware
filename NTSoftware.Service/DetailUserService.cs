using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NTSoftware.Core.Models.Models;
using NTSoftware.Core.Models.Models.NTSoftware.Core.Models.Models;
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
        private readonly UserManager<AppUser> _userManager;
        private ICompanyRepository _icompanyRepository;
        public DetailUserService(IUnitOfWork unitOfWork, IMapper mapper, ICompanyRepository icompanyRepo, IDetailUserRepository detailUserRepository, AppDbContext dbContext, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _detailUserRepository = detailUserRepository;
            _dbContext = dbContext;
            _userManager = userManager;
            _icompanyRepository = icompanyRepo;
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
        public void AddUserDetail(UserCompanyDetailViewModel vm)
        {
            var company = new CompanyDetail();
            company.CompanyName = vm.CompanyName;
            company.EmailRepresentative = vm.EmailRepresentative;
            company.PhoneNumber = vm.PhoneNumber;
            company.PositionRepresentative = vm.PositionRepresentative;
            company.RepresentativeName = vm.RepresentativeName;
            company.Address = vm.Address;
            _icompanyRepository.Add(company);
            SaveChanges();

            var user = new AppUser();
            user.CompanyId = company.Id ;
            user.UserName = vm.UserName;
            user.PhoneNumber = vm.PhoneNumber;
            user.Status = vm.Status;
            user.Position = vm.Position;
            user.UserType = vm.UserType;
            user.DepartmentId = vm.DepartmentId;
            user.PasswordHash = vm.Password;
            _userManager.CreateAsync(user);
            SaveChanges();

            var userdetail = vm.UserDetail.ToList();
            foreach (var item in userdetail)
            {
                var detail = new DetailUser();
                detail.Id = item.Id;
                detail.Name = item.Name;
                detail.PhoneNumber = item.PhoneNumber;
                detail.Address = item.Address;
                detail.Birthday = item.Birthday;
                detail.PhoneNumber = item.PhoneNumber;
                detail.Gender = item.Gender;
                _detailUserRepository.Add(detail);
                SaveChanges();
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
