using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LFM.DataAccess.DB.Core.Entities.ToDoEntities
{
    [Table("ToDoOperationCodes")]
    public class ToDoOperationCode
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ToDoEntity> ToDos { get; set; }
    }
}
