﻿using IOT.Core.Common;
using IOT.Core.IRepository.CommType;
using IOT.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Core.Repository.CommType
{
    public class CommTypeRepository : ICommTypeRepository
    {
        public List<Dictionary<string, object>> BindType()
        {
            string sql = $"select * from CommType where State=1";
            //获取所有分类
            List<Model.CommType> lst = DapperHelper.GetList<Model.CommType>(sql);
            return Recursion(lst, 0);
        }

        /// <summary>
        /// 递归获取层级分类
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        private List<Dictionary<string,object>> Recursion(List<Model.CommType> lst,int pid)
        {
            //实例化一个集合
            List<Dictionary<string, object>> json = new List<Dictionary<string, object>>();
            //获取所有子菜单
            List<Model.CommType> subLst = lst.Where(x => x.ParentId == pid).ToList();
            //遍历所有子菜单
            foreach (var item in subLst)
            {
                //实例化一个字典
                Dictionary<string, object> jsonSub = new Dictionary<string, object>();
                jsonSub.Add("value", item.TId);
                jsonSub.Add("label", item.TName);
                if (lst.Count(x=>x.ParentId==item.TId)>0)
                {
                    jsonSub.Add("children", Recursion(lst, item.TId));
                }
                json.Add(jsonSub);
            }
            return json;
        }

        /// <summary>
        /// 添加 or 修改 分类
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        public string CreateType(Model.CommType comm)
        {
            if (comm.TId == 0)  //添加
            {
                try
                {
                    string sql = $"insert into CommType values(null,'{comm.TName}',{comm.Sort},'{comm.TIcon}',{comm.State},{comm.ParentId})";
                    int i = DapperHelper.Execute(sql);
                    if (i > 0)
                    {
                        return "添加成功";
                    }
                    else
                    {
                        return "添加失败";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else  //修改
            {
                try
                {
                    string sql = $"update CommType set TName='{comm.TName}',Sort={comm.Sort},TIcon='{comm.TIcon}',State={comm.State},ParentId={comm.ParentId} where TId={comm.TId}";
                    int i = DapperHelper.Execute(sql);
                    if (i > 0)
                    {
                        return "修改成功";
                    }
                    else
                    {
                        return "修改失败";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int DeleteType(int tid)
        {
            //查询要删除商品的子菜单数量
            int sonNum = Convert.ToInt32(DapperHelper.Exescalar($"select count(*) from CommType where ParentId={tid}"));
            if (sonNum == 0)
            {
                //删除的SQL语句
                string sql = $"delete from CommType where TId={tid}";
                return DapperHelper.Execute(sql);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        public List<IOT.Core.Model.V_CommType> GetCommTypes(string st = "", string key = "")
        {
            List<IOT.Core.Model.V_CommType> lst = DapperHelper.GetList<IOT.Core.Model.V_CommType>("select * from V_CommType order by sort");
            //状态
            if (!string.IsNullOrEmpty(st))
            {
                lst = lst.Where(x => x.State == Convert.ToInt32(st)).ToList();
            }
            if (!string.IsNullOrEmpty(key))
            {
                lst = lst.Where(x => x.TName.Contains(key)).ToList();
            }
            return lst;
        }

        /// <summary>
        /// 通过类别id获取分类
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public Model.V_CommType GetCommTypeByTid(int tid)
        {
            return DapperHelper.GetList<IOT.Core.Model.V_CommType>($"select * from V_CommType where TId={tid}").FirstOrDefault();
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        public List<Model.CommType> GetOneType()
        {
            return DapperHelper.GetList<Model.CommType>("select * from CommType where ParentId=0 and State=1");
        }

        /// <summary>
        /// 获取二级分类
        /// </summary>
        /// <returns></returns>
        public List<Model.CommType> GetTwoType(int pid)
        {
            return DapperHelper.GetList<Model.CommType>($"select * from CommType where ParentId={pid} and State=1");
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public int UpdateState(int tid)
        {
            try
            {
                //获取要修改的分类
                IOT.Core.Model.CommType commType = DapperHelper.GetList<IOT.Core.Model.CommType>($"select * from CommType where TId={tid}").FirstOrDefault();
                if (commType.State == 0)
                {
                    commType.State = 1;
                }
                else
                {
                    commType.State = 0;
                }
                //修改的SQL语句
                string sql = $"update CommType set State={commType.State} where TId={commType.TId}";
                return DapperHelper.Execute(sql);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
