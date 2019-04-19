using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.db.model
{
    class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string Note { get; set; }
        public int Status { get; set; } = 0;
    }
}
