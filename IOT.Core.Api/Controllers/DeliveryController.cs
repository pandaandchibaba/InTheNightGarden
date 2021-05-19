﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.Core.IRepository.Delivery;
namespace IOT.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        [HttpGet]
        [Route("/api/ShowDelivery")]
        public IActionResult ShowDelivery(string deliveryname )
        {
            List<IOT.Core.Model.Delivery> ld = _deliveryRepository.Query();
            //查找
            if (!string.IsNullOrEmpty(deliveryname))
            {
                ld = ld.Where(x => x.DeliveryName.Contains(deliveryname)).ToList();
            }
            return Ok(new
            {
                msg = "",
                code = 0,
                data = ld
            });


        }

        [HttpPost]
        [Route("/api/AddDelivery")]
        public int AddDelivery(IOT.Core.Model.Delivery delivery)
        {
            int i = _deliveryRepository.Insert(delivery);
            return i;
        
        }

        [HttpDelete]
        [Route("/api/DelDelivery")]
        public int DelDelivery(string ids)
        {
            int i = _deliveryRepository.Delete(ids);
            return i;

        }


        [HttpPut]
        [Route("/api/UptDelivery")]
        public int UptDelivery(IOT.Core.Model.Delivery delivery)
        {
            int i = _deliveryRepository.Update(delivery);
            return i;

        }
    }
}
