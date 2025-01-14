﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOT.Core.Model;
using IOT.Core.Common;
using IOT.Core.IRepository.Delivery;

namespace IOT.Core.Repository.Delivery
{
    public class DeliveryRepository : IDeliveryRepository
    {
        public int Delete(string ids)
        {
            string sql = $"DELETE FROM Delivery WHERE DeliveryId IN({ids})";
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'删除配送ID为{ids}的配送表信息',NOW(),'配送表')");
            return DapperHelper.Execute(sql);
        }

        public int Insert(Model.Delivery Model)
        {
            string sql = $"INSERT INTO Delivery VALUES(NULL,{Model.UserId},{Model.ColonelID},'{Model.DeliveryPath}','{Model.DeliveryName}')";
            return DapperHelper.Execute(sql);
        }

        public List<Model.Delivery> Query()
        {
            string sql = "SELECT a.WarehouseId, a.DeliveryId,a.DeliveryName,c.ColonelName,c.Phone Phones,a.DeliveryPath,c.Address Addresss FROM Delivery a JOIN Users b ON a.UserId=b.UserId join Colonel c ON a.ColonelID=c.ColonelID";
            return DapperHelper.GetList<IOT.Core.Model.Delivery>(sql);
        }

        public int Update(Model.Delivery Model)
        {
            string sql = $"UPDATE Delivery SET DeliveryName='{Model.DeliveryName}',DeliveryPath='{Model.DeliveryPath}' WHERE DeliveryId={Model.DeliveryId}";
            DapperHelper.Execute($"INSERT INTO Lognote VALUES(NULL,'修改配送小区名称为{Model.DeliveryName}的配送表信息',NOW(),'配送表')");
            return DapperHelper.Execute(sql);
        }
    }
}
