﻿using IOT.Core.IRepository.OrderComment;
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
    public class OrderCommentController : ControllerBase
    {
        private readonly IOrderCommentRepository _orderCommentRepository;

        public OrderCommentController(IOrderCommentRepository  orderCommentRepository)
        {
            _orderCommentRepository = orderCommentRepository;
        }
        [HttpGet]
        [Route("/api/GetOrderComment")]

        public IActionResult GetOrderComment()
        {
            var list = _orderCommentRepository.Query();
            return Ok(new
            {
                msg = "",
                code = 0,
                data = list
            });
        }


        [HttpPost]
        [Route("/api/UptOrderCommentState")]

        public int UptState(int id)
        {
            int i = _orderCommentRepository.UptState(id);
            return i;
        }
        [HttpDelete]
        [Route("/api/DelOrderComment")]

        public int Del(string id)
        {
            int i = _orderCommentRepository.Del(id);
            return i;
        }
    }
}
