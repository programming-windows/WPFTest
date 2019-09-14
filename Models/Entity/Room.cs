using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity
{
    public class Room : BasicEntity
    {
        public enum RoomState
        {
            VACANT,
            OCCUPIED,
            OTHER
        }

        /// <summary>
        /// Type of the room.
        /// </summary>
        public RoomType type { get; set; }

        /// <summary>
        /// Number of the room.
        /// </summary>
        public String roomNumber { get; set; }

        /// <summary>
        /// Location of the room.
        /// </summary>
        public String location { get; set; }

        /// <summary>
        /// Aditional comments on the room.
        /// </summary>
        public String comments { get; set; }

        /// <summary>
        /// Price of the room.
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// State of the room.
        /// </summary>
        public RoomState state { get; set; }
    }
}
