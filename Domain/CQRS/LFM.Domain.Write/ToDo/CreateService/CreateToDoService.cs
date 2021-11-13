using System;
using System.Linq;
using System.Threading.Tasks;
using LFM.DataAccess.DB.Core.Context;
using LFM.DataAccess.DB.Core.Entities.ToDoEntities;
using LFM.DataAccess.DB.Core.Types;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LFM.Domain.Write.ToDo.CreateService
{
    internal sealed class CreateToDoService : ICreateToDoService
    {
        private readonly LfmDbContext _context;

        public CreateToDoService(LfmDbContext context)
        {
            _context = context;
        }

        public async Task CreateToDo<TCommand>(TCommand command, int requestedUserId, int operationId)
            where TCommand : NeedsApproveCommand
        {
            var existedToDo = await _context.ToDos
                .Where(t => t.CreatedByUserId == requestedUserId)
                .Where(t => t.OperationCodeId == operationId)
                .Where(t => t.StatusId == (int) ToDoStatusEnum.Pending)
                .FirstOrDefaultAsync();

            var jsonCommand = JsonConvert.SerializeObject(command);

            if (existedToDo != null)
            {
                existedToDo.JsonCommand = jsonCommand;
                await _context.SaveChangesAsync();
                return;
            }

            ToDoEntity toDo = new ToDoEntity
            {
                StatusId = (int)ToDoStatusEnum.Pending,
                JsonCommand = jsonCommand,
                OperationCodeId = operationId,
                CreatedByUserId = requestedUserId,
                CreatedDateTime = DateTime.Now
            };

            _context.ToDos.Add(toDo);
            await _context.SaveChangesAsync();
        }
    }
}