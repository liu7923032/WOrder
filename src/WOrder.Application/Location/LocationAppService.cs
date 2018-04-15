using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using WOrder.Domain.Entities;

namespace WOrder.Location
{

    public interface ILocationAppService : IAsyncCrudAppService<LocationDto, long, GetAllLocatinDto, CreateLocationInput, UpdateLocationInput>
    {

    }


    [AbpAuthorize]
    public class LocationAppService : AsyncCrudAppService<WOrder_Location, LocationDto, long, GetAllLocatinDto, CreateLocationInput, UpdateLocationInput>, ILocationAppService
    {
        private readonly IRepository<WOrder_Location, long> _locationRepository;
        public LocationAppService(IRepository<WOrder_Location, long> locationRepository) : base(locationRepository)
        {
            _locationRepository = locationRepository;
        }

        protected override IQueryable<WOrder_Location> CreateFilteredQuery(GetAllLocatinDto input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.UserId.HasValue, u => u.UserId.Equals(input.UserId));
        }
    }
}
