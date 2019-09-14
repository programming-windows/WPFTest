using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Entity
{
    public enum EntityState
    {
        NONE,
        NEW,
        MODIFIED,
        DELETED
    }

    public class BasicEntity
    {
        
        public EntityState entityState { get; set; }//仅程序
    }
}
