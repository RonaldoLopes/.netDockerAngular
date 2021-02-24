using System;

namespace SmartSchool.WebAPI.V1.Dto
{
    public class AlunoDto
    {
        /// <summary>
        /// Identificador e chave do banco
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Chave do Aluno para outros negócios na instituição
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// Nome é o primeiro nome e o Sobrenome do Aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Esta idade é o calculo relacionado a data de nascimento do Aluno
        /// </summary>
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public int Idade { get; set; }
        public DateTime DataIni { get; set; }
        public bool Ativo { get; set; }
    }
}