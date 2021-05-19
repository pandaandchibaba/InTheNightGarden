﻿using IOT.Core.Common;
using IOT.Core.IRepository.Commodity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Core.Repository.Commodity
{
    public class CommodityRepository : ICommodityRepository
    {
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="commodity"></param>
        /// <returns></returns>
        public int CreateCommodity(Model.Commodity commodity)
        {
            //添加的SQL语句
            string sql = $"insert into Commodity values (null,'{commodity.CommodityName}','{commodity.CommodityPic}',{commodity.ShopPrice},{commodity.ShopNum},{commodity.Repertory},{commodity.Sort},0,now(),{commodity.TId},'{commodity.Remark}',{commodity.TemplateId},'{commodity.CommodityKey}','{commodity.SendAddress}','{commodity.Job}',{commodity.Integral},{commodity.SId},'{commodity.Color}','{commodity.Size}',0,0,{commodity.CostPrice},{commodity.ColonelID},{commodity.Mid})";
            return DapperHelper.Execute(sql);
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="code">怎样显示 1、出售中，2、仓库中，3、已出售，4、回收站</param>
        /// <param name="tid">类别</param>
        /// <param name="key">查询关键词</param>
        /// <returns></returns>
        public List<IOT.Core.Model.V_Commodity> GetCommodities(int code = 1, int tid = 0, string key = "")
        {
            string sql = "";
            if (code == 1)  //出售中
            {
                sql = "select * from V_Commodity where DeleteState=0 and State=1";
            }
            else if (code == 2)  //仓库中
            {
                sql = "select * from V_Commodity where DeleteState=0";
            }
            else if (code == 3)  //已销售商品
            {
                sql = "select * from V_Commodity where DeleteState=0 and IsSell=1";
            }
            else if (code == 4)  //回收站
            {
                sql = "select * from V_Commodity where DeleteState=1";
            }
            List<IOT.Core.Model.V_Commodity> lst = DapperHelper.GetList<IOT.Core.Model.V_Commodity>(sql);
            //类别查询
            if (tid!=0)
            {
                lst = lst.Where(x => x.TId == tid).ToList();
            }
            //关键字查询
            if (!string.IsNullOrEmpty(key))
            {
                lst = lst.Where(x => x.CommodityName.Contains(key) || x.CommodityKey == key || x.CommodityId.ToString() == key).ToList();
            }
            return lst;
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="CId"></param>
        /// <returns></returns>
        public int UpdateDeleteState(int CId)
        {
            //获取要修改的商品
            IOT.Core.Model.Commodity commodity = DapperHelper.GetList<IOT.Core.Model.Commodity>($"select * from Commodity where CommodityId={CId}").FirstOrDefault();
            if (commodity.DeleteState==0)
            {
                commodity.DeleteState = 1;
            }
            else
            {
                commodity.DeleteState = 0;
            }
            //修改的SQL语句
            string sql = $"update Commodity set DeleteState={commodity.DeleteState} where CommodityId={commodity.CommodityId}";
            return DapperHelper.Execute(sql);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="CId"></param>
        /// <returns></returns>
        public int UpdateState(int CId)
        {
            //获取要修改的商品
            IOT.Core.Model.Commodity commodity = DapperHelper.GetList<IOT.Core.Model.Commodity>($"select * from Commodity where CommodityId={CId}").FirstOrDefault();
            if (commodity.State == 0)
            {
                commodity.State = 1;
            }
            else
            {
                commodity.State = 0;
            }
            //修改的SQL语句
            string sql = $"update Commodity set State={commodity.State} where CommodityId={commodity.CommodityId}";
            return DapperHelper.Execute(sql);
        }
    }
}