using System.Data.SQLite;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Trabalho.M3.OrganizadorMidia.Entites;

namespace Trabalho.M3.OrganizadorMidia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly SQLiteConnection _connection;

        public AgendamentoController()
        {
            var xAbsolutePath = Path.Combine("database.db");
            _connection = new SQLiteConnection($"Data Source={xAbsolutePath}");
        }

        [HttpPost]
        public async Task<ActionResult<int>> Adicionar(Agendamento pAgendamento)
        {
            var xRetorno = -1;
            try
            {
                _connection.Open();
                xRetorno = await _connection.ExecuteAsync(
                    "INSERT INTO Agendamento(IdUsuario, IdMidia, DataAgendamento, Estado) VALUES(@IdUsuario, @IdMidia, @DataAgendamento, @Estado)",
                    pAgendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created(nameof(Agendamento), xRetorno);
        }

        [HttpGet]
        public async Task<List<Agendamento>> Obter()
        {
            var xRetorno = new List<Agendamento>();

            _connection.Open();
            xRetorno = (await _connection.QueryAsync<Agendamento>(
                "SELECT * FROM Agendamento")).ToList();

            return xRetorno;
        }

        [HttpPut("{pId}")]
        public async Task<ActionResult> Editar(int pId, [FromBody] Agendamento pAgendamento)
        {
            try
            {
                _connection.Open();
                await _connection.ExecuteAsync(
                    @"UPDATE Agendamento
                        SET IdUsuario = @IdUsuario,
                            IdMidia = @IdMidia,
                            DataAgendamento = @DataAgendamento,
                            Estado = @Estado
                    WHERE Id = @pId",
                    new
                    {
                        pId,
                        pAgendamento.IdUsuario,
                        pAgendamento.IdMidia,
                        pAgendamento.DataAgendamento,
                        pAgendamento.Estado
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
                "DELETE FROM [Agendamento] WHERE [Id]=@pId", new {pId});

            return Ok();
        }
    }
}
