using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V2.Dto;
using SmartSchool.WebAPI.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.WebAPI.V2.Controllers
{
    ///<summary>
    ///Versão 2 do controlador de alunos
    ///</summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        ///<summary>
        ///
        ///</summary>
        ///<param name="repo"></param>
        ///<param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/<AlunoController>
        ///<summary>
        ///Método responsável para retornar todos os meus alunos
        ///</summary>
        ///<return></return>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);  

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        // GET: api/aluno/1
        //[HttpGet("byId/{id}")]//dessa forma usa queryString, na chamada fica api/aluno/byId?id=1
        // GET: api/aluno
        ///<summary>
        ///Método responsável por retornar apenas um Aluno por ID
        ///</summary>
        ///<param name="id"></param>
        ///<return></return>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAllAlunoByID(id, false);

            if (aluno == null) return BadRequest("Nada encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
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
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
           if( _repo.SaveChanges())
           {
               return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
           }
            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var aluno = _repo.GetAllAlunoByID(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
           if( _repo.SaveChanges())
           {
               return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
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
