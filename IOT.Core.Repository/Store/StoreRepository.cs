﻿using IOT.Core.Common;
using IOT.Core.IRepository.Store;
using IOT.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOT.Core.Repository.Store
{
    public class StoreRepository : IStoreRepository
    {
        //实例化帮助文件
        RedisHelper<Model.Store> rs = new RedisHelper<Model.Store>();
        //缓存关键字
        string redisKey;
        //全部数据
        List<IOT.Core.Model.Store> lst = new List<Model.Store>();
        //构造函数
        public StoreRepository()
        {
            redisKey = "store_list";
            lst = rs.GetList(redisKey);
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelCom(int ids)
        {
            string sql = $"DELETE FROM Commodity WHERE CommodityId=({ids})";
            int i= DapperHelper.Execute(sql);
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'删除商品状态ID为{ids}的商品表信息',NOW(),'商品表')");
            return i;
        }
        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DelStore(int ids)
        {
            string sql = $"delete from Store where Mid in ({ids})";
            int i = DapperHelper.Execute(sql);
            if (i > 0)
            {
                //找到要删除的对象
                Model.Store store = lst.FirstOrDefault(x => x.Mid.Equals(ids));
                lst.Remove(store);
            }
            //从新存入
            rs.SetList(lst, redisKey);
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'删除门店ID为{ids}的门店表信息',NOW(),'门店表')");
            return i;
        }

        public List<Model.Commodity> GetCommodities()
        {
            string sql = "SELECT *FROM Commodity";
            return DapperHelper.GetList<IOT.Core.Model.Commodity>(sql);
        }

        /// <summary>
        /// 显示门店
        /// </summary>
        /// <returns></returns>
        public List<Model.Store> GetStores()
        {
            if (lst==null||lst.Count==0)
            {
                //不存在
                lst = DapperHelper.GetList<IOT.Core.Model.Store>("SELECT * FROM Store");
                //从新存入
                rs.SetList(lst, redisKey);
            }
            return lst;
        }

        /// <summary>
        /// 显示查询门店
        /// </summary>
        /// <param name="address"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<V_Store> GetStores(string address = "", string key = "")
        {
            //获取全部数据
            List<V_Store> lst = DapperHelper.GetList<V_Store>("select * from V_Store");
            lst = lst.Where(x => (string.IsNullOrEmpty(address) ? true : x.Address.Contains(address)) && (string.IsNullOrEmpty(key) ? true : (x.UserName.Contains(key) || x.Phone == key))).ToList();
            return lst;
        }
        /// <summary>
        /// 反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Model.Store> GetStoresFan(int id)
        {
            string sql = $"select * from Store where Mid={id}";
            return DapperHelper.GetList<IOT.Core.Model.Store>(sql);
        }

        public int InsertStore(Model.Store Model)
        {
            string sql = $"INSERT INTO Store VALUES(NULL,'{Model.MName}','{Model.Shopowner}','{Model.Goods}','{Model.Volume}','{Model.StoreType}','{Model.Extraction}','{Model.State}','{Model.StoreNo}','{Model.Pwd}','{Model.Phone}','{Model.Background}','{Model.Logo}','{Model.Approve}')";
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'添加门店名称为{Model.MName}的门店表信息',NOW(),'门店表')");
            return DapperHelper.Execute(sql);
        }
        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public int UptCom(int cid)
        {
            string sql = $"SELECT * FROM Commodity ";

            List<IOT.Core.Model.Commodity> list = DapperHelper.GetList<IOT.Core.Model.Commodity>(sql);

            IOT.Core.Model.Commodity order = list.FirstOrDefault(x => x.CommodityId.Equals(cid));
            string sqlq = "";
            if (order.State == 0)
            {
                sqlq = $"UPDATE Commodity SET State=State+1 where CommodityId={cid}";
            }
            else
            {
                sqlq = $"UPDATE Commodity SET State=State-1 where CommodityId={cid}";

            }
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'修改商品状态ID为{cid}的商品表信息',NOW(),'商品表')");
            return DapperHelper.Execute(sqlq);
        }
        /// <summary>
        /// 修改门店
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int UptStore(Model.Store Model)
        {
            string sql = $"UPDATE Store SET MName ='{Model.MName}',Shopowner='{Model.Shopowner}',Goods='{Model.Goods}',Volume='{Model.Volume}',StoreType='{Model.StoreType}',Extraction='{Model.Extraction}',State='{Model.State}',StoreNo='{Model.StoreNo}',Pwd='{Model.Pwd}',Phone='{Model.Phone}',Background='{Model.Background}',Logo='{Model.Logo}',Approve='{Model.Approve}' where Mid='{Model.Mid}'";
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'修改门店名称为{Model.MName}的门店表信息',NOW(),'门店表')");
            return DapperHelper.Execute(sql);
        }
        /// <summary>
        /// 修改门店状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UptStoreState(int id)
        {
            string sql = $"SELECT * FROM Store ";

            List<IOT.Core.Model.Store> list = DapperHelper.GetList<IOT.Core.Model.Store>(sql);

            IOT.Core.Model.Store order = list.FirstOrDefault(x => x.Mid.Equals(id));
            string sqlq = "";
            if (order.State == 0)
            {
                sqlq = $"UPDATE Store SET State=State+1 where Mid={id}";
            }
            else
            {
                sqlq = $"UPDATE Store SET State=State-1 where Mid={id}";

            }
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'修改门店状态ID为{id}的门店表信息',NOW(),'门店表')");
            return DapperHelper.Execute(sqlq);
        }
    }
}
