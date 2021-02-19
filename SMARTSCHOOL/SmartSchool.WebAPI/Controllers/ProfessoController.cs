using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
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
        private readonly SmartContext _context;
        public ProfessorController(SmartContext context){
            _context = context;
        }

        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }
        // GET: api/professor/1
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);

            if(professor == null) return BadRequest("Nada encontrado");
            return Ok(professor);
        }
        // GET: api/professor/nome
        //com queryString : http://localhost:5000/api/professor/byName?nome=Ronaldo&sobrenome=Alves
        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(nome));

            if(professor == null) return BadRequest("Nada encontrado");
            return Ok(professor);
        }
        

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(prof == null) return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        //O método de requisição HTTP PATCH aplica modificações parciais a um recurso.
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(prof == null) return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if(professor == null) return BadRequest("Professor não encontrador");

            _context.Remove(professor);
            _context.SaveChanges();
            return Ok();
        }
       
    }
}
