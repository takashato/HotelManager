using HotelManager.db.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.data
{
    public class Session
    {
        public Staff CurrentStaff { get; set; }

        public Session(Staff staff)
        {
            CurrentStaff = staff;
        }
    }
}
