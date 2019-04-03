using MvcDapper.Entity.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace MvcDapper.DAL
{
    public class EmployeeDAL
    {
        #region init dbconnection
        private static readonly string connString = ConfigurationManager.ConnectionStrings["CompanyNG"].ConnectionString;
        private IDbConnection _conn;
        public IDbConnection Conn
        {
            get
            {
                _conn = new SqlConnection(connString);
                _conn.Open();
                return _conn;
            }
        }
        #endregion

        /// <summary>
        /// 获取一个list的数据
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            using (Conn)
            {
                string query = "SELECT * FROM Employee";

                return Conn.Query<Employee>(query).ToList();
            }

        }

        /// <summary>
        /// 分页获取数据列表(带条件)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="prams">参数</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetPageListWhere(StringBuilder where, Dictionary<string, object> prams, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            int num = (pageIndex - 1) * pageSize;
            int num1 = pageIndex * pageSize;
            using (Conn)
            {
                StringBuilder strSql = new StringBuilder();
                StringBuilder sql = new StringBuilder();
                sql.Append(@"SELECT * FROM (SELECT  EmpID,EmpName,EmpSex,EmpAge,EmpTel,EmpAdress 
                                                FROM Employee) A WHERE 1=1 ");
                sql.Append(where);
                strSql.Append("Select * From (Select ROW_NUMBER() Over (Order By " + orderField + " " + orderType + "");
                strSql.Append(") As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
                count = Conn.Query<int>("Select Count(1) From (" + sql + ") As t", prams).Single();
                return Conn.Query<Employee>(strSql.ToString(), prams).ToList();
            }
        }

        /// <summary>
        /// 根据主键值获得一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEntity(string id)
        {
            using (Conn)
            {
                string query = "SELECT * FROM Employee WHERE EmpID=@EmpID";
                return Conn.Query<Employee>(query, new { EmpID = id }).SingleOrDefault();
            }
        }

        public int Update(Employee obj)
        {
            using (Conn)
            {
                string query = @"UPDATE Employee 
                                    SET  EmpName=@EmpName,EmpSex=@EmpSex,EmpAge=@EmpAge,EmpTel=@EmpTel,EmpAdress=@EmpAdress
                                       WHERE EmpID =@EmpID";
                return Conn.Execute(query, obj);
            }
        }
        public int Insert(Employee obj)
        {
            using (Conn)
            {
                string query = @"INSERT INTO Employee 
                                    VALUES(@EmpName,@EmpSex,@EmpAge,@EmpTel,@EmpAdress)";
                return Conn.Execute(query, obj);
            }
        }
        public int Delete(Employee obj)
        {
            using (Conn)
            {
                string query = @"DELETE FROM Employee WHERE EmpID = @EmpID";
                return Conn.Execute(query, obj);
            }
        }
    }
}
