using Microsoft.AspNetCore.Mvc;
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
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Ronaldo",
                Sobrenome = "Lopes",
                Telefone = "12q345"
            },
            new Aluno(){
                Id = 2,
                Nome = "Sara",
                Sobrenome = "Borges",
                Telefone = "22222"
            },
            new Aluno(){
                Id = 3,
                Nome = "Enzo",
                Sobrenome = "Pereira",
                Telefone = "555555"
            },
        };
        public AlunoController(){}
        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        // GET: api/aluno/1
        [HttpGet("byId/{id}")]//dessa forma usa queryString, na chamada fica api/aluno/byId?id=1
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);

            if(aluno == null) return BadRequest("Nada encontrado");
            return Ok(aluno);
        }
        // GET: api/aluno/nome
        //com queryString : http://localhost:5000/api/aluno/byName?nome=Ronaldo&sobrenome=Alves
        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));

            if(aluno == null) return BadRequest("Nada encontrado");
            return Ok(aluno);
        }
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        //O método de requisição HTTP PATCH aplica modificações parciais a um recurso.
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
        
       
    }
}
