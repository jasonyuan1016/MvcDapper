using MvcDapper.BLL;
using MvcDapper.Entity.Model;
using MvcDapper.IBLL;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDapper.Controllers
{
    public class Emp_InfoController : Controller
    {
        // GET: Employee
        IEmployeeBLL iemployeebll = new EmployeeBLL();
        public ActionResult EmpList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadList(string rows, string page, string sort, string order, string query)
        {

            int count = default(int);
            IList list = iemployeebll.GetPageList(query, sort, order, int.Parse(page), int.Parse(rows), ref count);
            return Content(JsonConvert.SerializeObject(new
            {
                total = count,
                rows = list
            }));
        }

        [HttpPost]
        public ActionResult LoadForm(string id)
        {
            if (!string.IsNullOrEmpty(id))
                return Json(iemployeebll.GetEntity(id));
            else
                return null;

        }

        [HttpPost]
        public ActionResult AcceptClick(Employee obj)
        {
            int isOk = default(int);
            //判断ID的值，ID为0表示ID为默认值，插入记录。非0的ID代表数据库已存在记录，则修改
            if (obj.EmpID != 0)
                isOk = iemployeebll.Update(obj);
            else
                isOk = iemployeebll.Insert(obj);
            return Content(isOk.ToString());
        }

        [HttpPost]
        public ActionResult Del(string id)
        {
            if (!string.IsNullOrEmpty(id))
                return Content(iemployeebll.Delete(new Employee { EmpID = int.Parse(id) }).ToString());
            else
                return null;
        }

    }
}