﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IOT.Core.IRepository.PutLibrary;
using IOT.Core.Repository.PutLibrary;
using IOT.Core.Model;
namespace IOT.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PutLibraryController : ControllerBase
    {
        private readonly IPutLibraryRepository _putLibraryRepository;

        public PutLibraryController(IPutLibraryRepository putLibraryRepository)
        {
            _putLibraryRepository = putLibraryRepository;
        }

        [HttpGet]
        [Route("/api/ShowPutLibrary")]
        public IActionResult ShowPutLibrary(string warehousename,string putno,string sdate = "", string zdate = "")
        {
            List<IOT.Core.Model.PutLibrary> lp = _putLibraryRepository.Query();
            if (!string.IsNullOrEmpty(warehousename))
            {
                lp = lp.Where(x => x.WarehouseName.Contains(warehousename)).ToList();
            }
            if (!string.IsNullOrEmpty(putno))
            {
                lp = lp.Where(x => x.PutNO.Contains(putno)).ToList();
            }
            if (!string.IsNullOrEmpty(sdate))
            {
                lp = lp.Where(x => x.PutDate.Date >= Convert.ToDateTime(sdate)).ToList();
            }
            if (!string.IsNullOrEmpty(zdate))
            {
                lp = lp.Where(x =>x.PutDate.Date <= Convert.ToDateTime(zdate)).ToList();
            }

            return Ok(new
            {
                msg = "",
                code = 0,
                data = lp
            });
        }
        [HttpPost]
        [Route("/api/AddPutLibrary")]
        public int AddPutLibrary(IOT.Core.Model.PutLibrary putLibrary)
        {
            int i = _putLibraryRepository.Insert(putLibrary);
            return i;
        }

        [HttpDelete]
        [Route("/api/DelPutLibrary")]
        public int DelPutLibrary(string ids)
        {
            int i = _putLibraryRepository.Delete(ids);
            return i;
        }
        [HttpPut]
        [Route("/api/UptPutLibrary")]
        public int UptPutLibrary(IOT.Core.Model.PutLibrary putLibrary)
        {
            int i = _putLibraryRepository.Update(putLibrary);
            return i;
        }

        [HttpGet]
        [Route("/api/FtPutLibrary")]
        public IActionResult FtPutLibrary(int id)
        {
            List<IOT.Core.Model.PutLibrary> lp = _putLibraryRepository.Query();
            IOT.Core.Model.PutLibrary putLibrary = lp.FirstOrDefault(x => x.PutLibraryId.Equals(id));
            return Ok(new
            {
                msg = "",
                code = 0,
                data = putLibrary
            });
        }


    }
}
