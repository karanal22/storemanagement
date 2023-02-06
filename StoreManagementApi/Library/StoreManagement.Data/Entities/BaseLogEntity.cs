using System;

namespace StoreManagement.Data.Entities
{
    public abstract class BaseLogEntity
    {
        //public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        //public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
