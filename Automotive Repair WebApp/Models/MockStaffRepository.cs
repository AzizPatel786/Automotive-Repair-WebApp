using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automotive_Repair_WebApp.Models
{
    public class MockStaffRepository
    {
        private List<Staff> _staffList;

        public MockStaffRepository()
        {
            _staffList = new List<Staff>()
        {
            new Staff() { Id = 1, Name = "Mary", Department = "HR", Email = "mary@pragimtech.com", Occupation = "Boss" },
            new Staff() { Id = 2, Name = "John", Department = "IT", Email = "john@pragimtech.com", Occupation = "Manager" },
            new Staff() { Id = 3, Name = "Sam", Department = "IT", Email = "sam@pragimtech.com", Occupation = "Assistant" },
        };
        }

        public Staff GetStaff(int Id)
        {
            return this._staffList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
