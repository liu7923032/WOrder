using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using WOrder.Domain.Entities;

namespace WOrder.Message
{

    public interface IMessageAppService : IAsyncCrudAppService<MessageDto, long, GetAllMessageInput, CreateMessageInput, UpdateMessageInput>
    {
        Task<bool> Read(long messageId);

        Task ReadMyMsg(long orderId);
    }


    [AbpAuthorize]
    public class MessageAppService : AsyncCrudAppService<Sys_Message, MessageDto, long, GetAllMessageInput, CreateMessageInput, UpdateMessageInput>, IMessageAppService
    {
        private readonly IRepository<Sys_Message, long> _messageRepository;
        public MessageAppService(IRepository<Sys_Message, long> messageRepository) : base(messageRepository)
        {
            _messageRepository = messageRepository;
        }



        public async Task<bool> Read(long messageId)
        {
            var message = await _messageRepository.GetAsync(messageId);
            message.IsRead = true;
            return await Task.FromResult(true);
        }

        public async Task ReadMyMsg(long orderId)
        {
            var userId = AbpSession.UserId.Value;
            var message = await _messageRepository.FirstOrDefaultAsync(u => u.SrcId.Equals(orderId) && u.UserId.Equals(userId));
            if (message != null && !message.IsRead)
            {
                message.IsRead = true;
            }
        }

        protected override IQueryable<Sys_Message> CreateFilteredQuery(GetAllMessageInput input)
        {
            var userId = input.UserId.HasValue ? input.UserId.Value : AbpSession.UserId.Value;
            return base.CreateFilteredQuery(input)
                .Where(u => u.UserId.Equals(userId))
                .WhereIf(input.IsRead.HasValue, u => u.IsRead.Equals(input.IsRead.Value))
                .WhereIf(input.SDate.HasValue, u => u.CreationTime > input.SDate.Value)
                .WhereIf(input.EDate.HasValue, u => u.CreationTime < input.EDate.Value.AddDays(1))
                .Include("Creator");
        }


    }
}
