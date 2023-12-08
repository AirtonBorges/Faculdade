using System.Data.SQLite;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Trabalho.M3.OrganizadorMidia.Entites;

namespace Trabalho.M3.OrganizadorMidia.Controllers;

[ApiController]
[Route("[controller]")]
public class MidiaController : ControllerBase
{
    private readonly SQLiteConnection _connection;

    public MidiaController()
    {
        var xAbsolutePath = Path.Combine("database.db");
        _connection = new SQLiteConnection($"Data Source={xAbsolutePath}");
    }

    [HttpPost]
    public async Task<ActionResult<int>> Adicionar(Midia pMidia)
    {
        var xRetorno = -1;
        try
        {
           _connection.Open();
            xRetorno = await _connection.ExecuteAsync(
                "INSERT INTO Midia(Titulo, Sinopse, DataLancamento, Plataforma, Url) VALUES(@Titulo, @Sinopse, @DataLancamento, @Plataforma, @Url)",
                pMidia);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Created(nameof(Midia), xRetorno);
    }

    [HttpGet]
    public async Task<List<Midia>> Obter()
    {
        var xRetorno = new List<Midia>();

        _connection.Open();
        xRetorno = (await _connection.QueryAsync<Midia>(
            "SELECT * FROM Midia")).ToList();

        return xRetorno;
    }

    [HttpPut("{pId}")]
    public async Task<ActionResult> Editar(int pId, [FromBody]Midia pMidia)
    {
        var xRetorno = -1;
        try
        {
            _connection.Open();
            await _connection.ExecuteAsync(
                @"UPDATE Midia
                        SET Titulo = @Titulo,
                            Sinopse = @Sinopse,
                            DataLancamento = @DataLancamento,
                            Plataforma = @Plataforma,
                            Url = @Url
                    WHERE Id = @pId",
                new
                {
                    pId
                    , pMidia.Titulo
                    , pMidia.Sinopse
                    , pMidia.DataLancamento
                    , pMidia.Plataforma
                    , pMidia.Url
                });
        }
        catch(Exception ex)
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
            "DELETE FROM [Midia] WHERE [Id]=@pId", new {pId});

        return Ok();
    }
}
