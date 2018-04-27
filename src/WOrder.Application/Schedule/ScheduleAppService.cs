using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using WOrder.Authorization;
using WOrder.Domain.Entities;

namespace WOrder.Schedule
{

    public interface IScheduleAppService : IAsyncCrudAppService<ScheduleDto, long, GetAllScheduleDto, CreateScheduleDto, UpdateScheduleDto>
    {
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="batchSaveDto"></param>
        /// <returns></returns>
        Task<bool> BatchSave(BatchSaveDto saveAll);

        Task<List<GetScheduleDto>> GetSchedules(GetAllScheduleDto dto);

    }

    [AbpAuthorize]
    public class ScheduleAppService : AsyncCrudAppService<WOrder_Schedule, ScheduleDto, long, GetAllScheduleDto, CreateScheduleDto, UpdateScheduleDto>, IScheduleAppService
    {
        private readonly IRepository<WOrder_Schedule, long> _scheduleRepository;

        public ScheduleAppService(IRepository<WOrder_Schedule, long> scheduleRepository) : base(scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }


        protected override IQueryable<WOrder_Schedule> CreateFilteredQuery(GetAllScheduleDto input)
        {
            return base.CreateFilteredQuery(input)
                    .Where(u => u.YFlag.Equals(input.YFlag) && u.MFlag.Equals(input.MFlag))
                    .WhereIf(!string.IsNullOrEmpty(input.ClassType), u => u.ClassType.Equals(input.ClassType))
                    .WhereIf(input.UserId.HasValue, u => u.UserId == input.UserId)
                    .Include("User");

        }

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="saveDto"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Page_Admin)]
        public async Task<bool> BatchSave(BatchSaveDto saveDto)
        {

            if (saveDto.EDate < saveDto.SDate)
            {
                throw new UserFriendlyException("开始时间大于结束时间,请处理");
            }

            int sDay = saveDto.SDate.Day, eDay = saveDto.EDate.Day;
            //1.删除已经由的记录
            await _scheduleRepository.DeleteAsync(u =>
                                                 u.UserId.Equals(saveDto.UserId)
                                                 && u.YFlag.Equals(saveDto.YFlag)
                                                 && u.MFlag.Equals(saveDto.MFlag)
                                                 && u.DFlag >= sDay
                                                 && u.DFlag <= eDay);

            for (int i = sDay; i <= eDay; i++)
            {
                var newEntity = saveDto.MapTo<WOrder_Schedule>();
                newEntity.DFlag = i;
                newEntity.ClassDate = Convert.ToDateTime(saveDto.YFlag + "/" + saveDto.MFlag + "/" + i);
                await _scheduleRepository.InsertAsync(newEntity);
            }
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 查询所有人员的排班信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Page_Admin)]
        public async Task<List<GetScheduleDto>> GetSchedules(GetAllScheduleDto input)
        {
            //1.查询结果
            var schedules = _scheduleRepository.GetAll().Where(u => u.YFlag.Equals(input.YFlag)
           && u.MFlag.Equals(input.MFlag)).Include("User");
            //2.查询条件
            schedules = schedules.WhereIf(!string.IsNullOrEmpty(input.ClassType), u => u.ClassType.Equals(input.ClassType));
            var final = await schedules.ToListAsync();

            var finalData = from a in final
                            group a by new
                            {
                                a.User.UserName,
                                a.UserId,
                                a.User.WorkMode,
                                a.User.AreaName,
                                a.User.Position,
                                a.YFlag,
                                a.MFlag
                            } into g
                            select new GetScheduleDto
                            {
                                UserName = g.Key.UserName,
                                UserId = g.Key.UserId,
                                WorkMode = g.Key.WorkMode,
                                AreaName = g.Key.AreaName,
                                Position = g.Key.Position,
                                YFlag = g.Key.YFlag,
                                MFlag = g.Key.MFlag,
                                UserDays = (from b in final
                                            where b.YFlag == g.Key.YFlag
                                            && b.MFlag == g.Key.MFlag
                                            && b.UserId == g.Key.UserId
                                            select new UserDayDto
                                            {
                                                Id = b.Id,
                                                ClassType = b.ClassType,
                                                DFlag = b.DFlag,
                                                Description = b.Description
                                            }).ToList()
                            };

            return await Task.FromResult(finalData.ToList());
        }
    }
}
