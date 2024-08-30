﻿using Capstone_____Vescio_Pia_Francesca__BE_.Entity;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Classes;
using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationApiController : ControllerBase
    {
        private readonly INationService _nationSvc;

        public NationApiController(INationService nationSvc)
        {
            _nationSvc = nationSvc;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
                var nations = await _nationSvc.GetAllNations();
            var nationsSelect = new List<Nation>();
                foreach (var nation in nations) {
                var nationSel= await _nationSvc.getNation(nation.Id);
                nationsSelect.Add(nationSel);
            }
                return Ok(nationsSelect);
            
        }

        // GET api/<NationApiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var nation = await _nationSvc.Read(id);
            var nationSel = await _nationSvc.getNation(nation.Id);
            return Ok(nationSel);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test funzionante");
        }
    }
}
