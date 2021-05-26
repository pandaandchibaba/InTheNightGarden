﻿using IOT.Core.IRepository.OrderInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderInfoController : ControllerBase
    {
        private readonly IOrderInfoRepository _orderInfoRepository;

        public OrderInfoController(IOrderInfoRepository orderInfoRepository)
        {
            _orderInfoRepository = orderInfoRepository;
        }



        [HttpGet]
        [Route("/api/GetListOrderInfo")]
        public IActionResult GetListOrderInfo(string name = "", int state = 0, string end = "", int tui = 0, int dingt = 0, int uid = 0, string cname = "", string ziti = "")
        {
            try
            {
                List<IOT.Core.Model.OrderInfo> list = _orderInfoRepository.GetOrderInfos(name, state, end, tui, dingt, uid, cname, ziti);
                if (name==""&&state==0&&end==""&&tui==0&&dingt==0&&uid==0&& cname==""&&ziti=="")
                {
                    return Ok(new
                    {
                        msg = "",
                        code = 0,
                        data = list
                    });
                }
                else
                {
                    
                    if (!string.IsNullOrEmpty(name))
                    {
                        list = list.Where(x => x.CommodityName.Contains(name) || x.Phone.Contains(name)).ToList();
                    }
                    if (state!=0)
                    {
                        list = list.Where(x => x.OrderState == state).ToList();
                    }
                    if (!string.IsNullOrEmpty(end))
                    {
                        list = list.Where(x => x.StartTime < Convert.ToDateTime(state)).ToList();
                    }
                    if (tui != 0)
                    {
                        list = list.Where(x => x.OrderState == tui).ToList();
                    }
                    if (dingt != 0)
                    {
                        list = list.Where(x => x.OrderState == tui).ToList();
                    }
                    if (uid != 0)
                    {
                        list = list.Where(x => x.UserId == tui).ToList();
                    }
                    if (!string.IsNullOrEmpty(cname))
                    {
                        list = list.Where(x => x.CommodityName.Contains(cname)).ToList();
                    }
                    if (!string.IsNullOrEmpty(ziti))
                    {
                        list = list.Where(x => x.Address.Contains(ziti)).ToList();
                    }
                    return Ok(new
                    {
                        msg = "",
                        code = 0,
                        data = list
                    });
                }
                    
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpGet]
        [Route("/api/GetOrderInfo")]
        
        public IActionResult GetOrderInfo()
        {
            try
            {
                List<Model.OrderInfo> list = _orderInfoRepository.Query();
                return Ok(new
                {
                    msg = "",
                    code = 0,
                    data = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("/api/getnum")]

        public IActionResult getnum()
        {
            try
            {
                List<Model.v_OrderInfo> list = _orderInfoRepository.getnum();
                return Ok(new
                {
                    msg = "",
                    code = 0,
                    data = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("/api/UptOrderInfoRemark")]
        
        public int UptRemark(Model.OrderInfo Model)
        {
            try
            {
                int i = _orderInfoRepository.UptRemark(Model);
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("/api/UptOrderInfoSendWay")]
        
        public int UptSendWay(Model.OrderInfo Model)
        {
            try
            {
                int i = _orderInfoRepository.UptSendWay(Model);
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("/api/UptOrderInfoOrderState")]
        
        public int UptOrderState(Model.OrderInfo Model)
        {
            try
            {
                int i = _orderInfoRepository.UptOrderState(Model);
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("/api/insert")]

        public int insert(Model.OrderInfo orderInfo)
        {
            try
            {
                int i = _orderInfoRepository.insert(orderInfo);
                return i;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
