using System;

namespace StoreManagement.Data.Entities
{
    public abstract class BaseIdLogEntity
    {
        public int Id { get; set; }
        //public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        //public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
