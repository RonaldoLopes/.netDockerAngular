using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.V1.Dto;
using SmartSchool.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.WebAPI.V1.Controllers
{
    ///<summary>
    ///
    ///</summary>
    [ApiController]
    [ApiVersion("1.0")]
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
        /// <summary>
        /// Método responsável para retornar todos os meus alunos
        /// </summary>
        /// <param name="pageParams">pageNumber_pageSize</param>
        /// <returns>AlunoDTO</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        ///<summary>
        ///Método responsável por retornar apenas 1 unico  AlunoDTO
        ///</summary>
        ///<return></return>
        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
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

        //O método de requisição HTTP PATCH aplica modificações parciais a um recurso.
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
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
