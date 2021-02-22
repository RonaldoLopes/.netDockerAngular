using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context)
        {
           _context = context; 
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        /*
        IQueryable é uma interface mais específica que possui um provedor de consulta, como o LINQ-To-SQL. É um objeto especial, 
        que ao utilizar um método, ele gera uma query ou uma estrutura de consulta para uma base de dados e 
        retorna estes resultados como uma lista ou um enumerável.
        */
        /*
        Se deseja exibir mais dados no relacionamento,  usar a query abaixo
        */
        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor) 
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor) 
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }
        public Aluno GetAllAlunoByID(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if(includeProfessor) 
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }


        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                            .ThenInclude(d => d.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            
            if(includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                        .Where(aluno => aluno.Disciplinas.Any(
                                d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)
                        ));

            return query.ToArray();
        }
        public Professor GetAllProfessoreByID(int professorId, bool includeProfessor = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeProfessor)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(professor => professorId == professorId);

            return query.FirstOrDefault();
        }



        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}