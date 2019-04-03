using MvcDapper.DAL;
using MvcDapper.Entity.Model;
using MvcDapper.IBLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDapper.BLL
{
    public class EmployeeBLL : IEmployeeBLL
    {
        private static readonly EmployeeDAL dal = new EmployeeDAL();
        public IList GetList()
        {
            return dal.GetList();
        }

        public IList GetPageList(string query, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            string where = string.Empty;
            Dictionary<string, object> prams = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(query))
            {
                prams.Add("@EmpName", "%" + query + "%");
                where = " AND EmpName like @EmpName";
            }
            return dal.GetPageListWhere(new StringBuilder(where), prams, orderField, orderType, pageIndex, pageSize, ref count);
        }

        public Employee GetEntity(string id)
        {
            return dal.GetEntity(id);
        }

        public int Update(Employee obj)
        {
            return dal.Update(obj);
        }

        public int Insert(Employee obj)
        {
            return dal.Insert(obj);
        }

        public int Delete(Employee obj)
        {
            return dal.Delete(obj);
        }
    }
}
