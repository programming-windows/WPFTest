using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entity
{
    public class RoomType:BasicEntity
    {
        /// <summary>
        /// Type of the room.
        /// </summary>
        public String type { get; set; }

        /// <summary>
        /// Description of the room type.
        /// </summary>
        public String description { get; set; }

        /// <summary>
        /// Additional information of the room type.
        /// </summary>
        public String remarks { get; set; }

        /// <summary>
        /// Price of this kind of room.
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// 房型状态(0:未被选中，1：被选中)
        /// </summary>
        public String status { get; set; }

        /// <summary>
        /// A sequence of real prices. The element at index i means the real price of 
        /// the ith day from today. For instance, realPrices[0] is the real price of 
        /// today.
        /// </summary>
        private double[] realPrices;

        public RoomType()
        {
        }

        public RoomType(RoomType t)
        {
            type = t.type;
            description = t.description;
            remarks = t.remarks;
            price = t.price;
            status = t.status;
        }

        /// <summary>
        /// Attach a sequence of real prices.
        /// </summary>
        /// <param name="prices">The prices to attach.</param>
        public void AttachRealPrices(double[] prices)
        {
            realPrices = prices;
        }

        /// <summary>
        /// Given the days offset from today, returns the price of the day.
        /// </summary>
        /// <param name="day">The days offset.</param>
        /// <returns>The price of that day. Returns a negative number if the offset is out of range.</returns>
        public double GetRealPrice(int day)
        {
            try
            {
                return realPrices[day];
            }
            catch (Exception)
            {
            }
            return -0.01;
        }
    }
}
