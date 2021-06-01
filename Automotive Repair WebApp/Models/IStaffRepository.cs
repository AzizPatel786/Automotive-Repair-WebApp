using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automotive_Repair_WebApp.Models
{
    public interface IStaffRepository
    {
        Staff GetStaff(int Id);
    }
}
