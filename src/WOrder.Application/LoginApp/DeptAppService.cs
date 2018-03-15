using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using WOrder.Domain.Entities;

namespace WOrder.LoginApp
{
    public interface IDeptAppService : IAsyncCrudAppService<DeptDto, int, GetAllDeptDto, CreateDeptDto, UpdateDeptDto>
    {

    }


    [AbpAuthorize]
    public class DeptAppService : AsyncCrudAppService<WOrder_Department, DeptDto, int, GetAllDeptDto, CreateDeptDto, UpdateDeptDto>, IDeptAppService
    {

        private readonly IRepository<WOrder_Department> _deptRepository;

        public DeptAppService(IRepository<WOrder_Department> deptRepository) : base(deptRepository)
        {
            _deptRepository = deptRepository;
        }



    }



}
