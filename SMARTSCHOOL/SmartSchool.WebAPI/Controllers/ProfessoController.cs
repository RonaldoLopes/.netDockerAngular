using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        public ProfessorController(){}
        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Professores: PRonaldo, PCareca, PSara, PEnzo");
        }

       
    }
}
