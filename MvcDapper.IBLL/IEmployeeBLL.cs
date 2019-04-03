﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MvcDapper.Entity.Model;

namespace MvcDapper.IBLL
{
    public interface IEmployeeBLL
    {

        /// <summary>
        /// 获取一个list的数据
        /// </summary>
        /// <returns></returns>
        IList GetList();
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="query">搜索键值</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        IList GetPageList(string query, string orderField, string orderType, int pageIndex, int pageSize, ref int count);
        /// <summary>
        /// 根据主键值获得一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Employee GetEntity(string id);

        int Update(Employee obj);

        int Insert(Employee obj);
        int Delete(Employee obj);
    }
}
