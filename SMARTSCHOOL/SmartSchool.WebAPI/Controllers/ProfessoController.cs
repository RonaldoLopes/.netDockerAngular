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
        private readonly IRepository  _repo;
        public ProfessorController(IRepository  repo)
        {
            _repo= repo;
        }

        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }
        // GET: api/professor/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var professor = _repo.GetAllProfessoreByID(id, false);

            if(professor == null) return BadRequest("Nada encontrado");
            return Ok(professor);
        }
        // // GET: api/professor/nome
        // //com queryString : http://localhost:5000/api/professor/byName?nome=Ronaldo&sobrenome=Alves
        // [HttpGet("byName")]
        // public IActionResult GetByName(string nome, string sobrenome)
        // {
        //     var professor = _context.Professores.FirstOrDefault(p => p.Nome.Contains(nome));

        //     if(professor == null) return BadRequest("Nada encontrado");
        //     return Ok(professor);
        // }
        

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Add(professor);
           if( _repo.SaveChanges())
           {
               return Ok(professor);
           }
            return BadRequest("Professor não cadastrado");
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetAllProfessoreByID(id, false);
            if(prof == null) return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            } 
            return BadRequest("Professor não atualizado");
        }

        //O método de requisição HTTP PATCH aplica modificações parciais a um recurso.
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetAllProfessoreByID(id, false);
            if(prof == null) return BadRequest("Professor não encontrado");

             _repo.Update(professor);
            if(_repo.SaveChanges())
            {
                return Ok(professor);
            } 
            return BadRequest("Professor não atualizado");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetAllProfessoreByID(id, false);
            if(professor == null) return BadRequest("Professor não encontrador");

            _repo.Delete(professor);
            if(_repo.SaveChanges())
            {
                return Ok("Professor deletado");
            }

            return BadRequest("Professor não deletado");
        }
       
    }
}
