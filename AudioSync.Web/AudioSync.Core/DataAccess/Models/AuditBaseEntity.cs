using System;
using System.Collections.Generic;
using System.Text;

namespace AudioSync.Core.DataAccess.Models
{
    public abstract partial class AuditBaseEntity : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
       
    }
}
