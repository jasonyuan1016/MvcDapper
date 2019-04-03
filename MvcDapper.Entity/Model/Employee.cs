using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDapper.Entity.Model
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpSex { get; set; }
        public int EmpAge { get; set; }
        public string EmpTel{ get; set; }
        public string EmpAdress { get; set; }
    }
}
