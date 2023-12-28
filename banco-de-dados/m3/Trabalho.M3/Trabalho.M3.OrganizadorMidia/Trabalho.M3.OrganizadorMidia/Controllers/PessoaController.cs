using System.Data.SQLite;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Trabalho.M3.OrganizadorMidia.Entites;

namespace Trabalho.M3.OrganizadorMidia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly SQLiteConnection _connection;

        public PessoaController()
        {
            var xAbsolutePath = Path.Combine("database.db");
            _connection = new SQLiteConnection($"Data Source={xAbsolutePath}");
        }

        [HttpPost]
        public async Task<ActionResult<int>> Adicionar(Pessoa pPessoa)
        {
            var retorno = -1;
            try
            {
                _connection.Open();
                retorno = await _connection.ExecuteAsync(
                    "INSERT INTO Pessoa(Nome, Sobrenome, DataNascimento, Email) VALUES(@Nome, @Sobrenome, @DataNascimento, @Email)",
                    pPessoa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created(nameof(Pessoa), retorno);
        }

        [HttpGet]
        public async Task<List<Pessoa>> Obter()
        {
            var retorno = new List<Pessoa>();

            _connection.Open();
            retorno = (await _connection.QueryAsync<Pessoa>(
                "SELECT * FROM Pessoa")).ToList();

            return retorno;
        }

        [HttpPut("{pId}")]
        public async Task<ActionResult> Editar(int pId, [FromBody] Pessoa pPessoa)
        {
            try
            {
                _connection.Open();
                await _connection.ExecuteAsync(
                    @"UPDATE Pessoa
                        SET Nome = @Nome,
                            Sobrenome = @Sobrenome,
                            DataNascimento = @DataNascimento,
                            Email = @Email
                    WHERE Id = @id",
                    new
                    {
                        pId,
                        pPessoa.Nome,
                        pPessoa.Sobrenome,
                        pPessoa.DataNascimento,
                        pPessoa.Email
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        [HttpDelete("{pId}")]
        public async Task<ActionResult> Delete(int pId)
        {
            _connection.Open();
            await _connection.ExecuteAsync(
                "DELETE FROM Pessoa WHERE Id=@pId", new {pId});

            return Ok();
        }
    }
}
