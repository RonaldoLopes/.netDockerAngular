using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        // GET: api/aluno/1
        //[HttpGet("byId/{id}")]//dessa forma usa queryString, na chamada fica api/aluno/byId?id=1
        // GET: api/aluno
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAllAlunoByID(id, false);

            if (aluno == null) return BadRequest("Nada encontrado");
            return Ok(aluno);
        }
        // GET: api/aluno/nome
        //com queryString : http://localhost:5000/api/aluno/byName?nome=Ronaldo&sobrenome=Alves
        // [HttpGet("byName")]
        // public IActionResult GetByName(string nome, string sobrenome)
        // {
        //     var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));

        //     if (aluno == null) return BadRequest("Nada encontrado");
        //     return Ok(aluno);
        // }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
           if( _repo.SaveChanges())
           {
               return Ok(aluno);
           }
            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = _repo.GetAllAlunoByID(id);
            if (alu == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
           if( _repo.SaveChanges())
           {
               return Ok(aluno);
           }
            return BadRequest("Aluno não Atualizado");
        }

        //O método de requisição HTTP PATCH aplica modificações parciais a um recurso.
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = _repo.GetAllAlunoByID(id);
            if (alu == null) return BadRequest("Aluno não encontrado");

             _repo.Update(aluno);
           if( _repo.SaveChanges())
           {
               return Ok(aluno);
           }
            return BadRequest("Aluno não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAllAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
           if( _repo.SaveChanges())
           {
               return Ok("Aluno deletado");
           }
            return BadRequest("Aluno não deletado");
        }


    }
}
