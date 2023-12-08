using System.Data.SQLite;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Trabalho.M3.OrganizadorMidia.Entites;

namespace Trabalho.M3.OrganizadorMidia.Controllers;

[ApiController]
[Route("[controller]")]
public class MidiaController : ControllerBase
{
    private readonly SQLiteConnection _sqlite;

    public MidiaController()
    {
        _sqlite = new SQLiteConnection("");
    }

    [HttpPost]
    public async Task<ActionResult<int>> Adicionar(Midia pMidia)
    {
        var xRetorno = -1;
        try
        {
            var xAbsolutePath = Path.Combine("database.db");
            await using var connection = new SQLiteConnection($"Data Source={xAbsolutePath}");
            connection.Open();
            xRetorno = connection.Query<int>(
                "INSERT INTO Midia(Titulo, Sinopse, DataLancamento, Plataforma, Url) VALUES(@Titulo, @Sinome, @DataLancamento, @Plataforma, @Url)",
                new
                {
                    pMidia.Titulo,
                    pMidia.Sinopse,
                    pMidia.DataLancamento,
                    pMidia.Plataforma,
                    pMidia.Url
                }).First();
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

        using var connection = new SqlConnection("Data Source=database.db");
        xRetorno = (await connection.QueryAsync<Midia>(
            "SELECT * FROM Midia")).ToList();

        return xRetorno;
    }

    [HttpPut("{pId}")]
    public async Task<ActionResult> Editar(int pId, [FromBody]Midia pMidia)
    {
        var xRetorno = -1;
        try
        {
            await using var connection = new SqlConnection("Data Source=database.db");
            await connection.ExecuteAsync(
                "UPDATE Midia SET(@Titulo, @Sinome, @DataLancamento, @Plataforma, @Url)",
                pMidia);
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
        await using var connection = new SqlConnection("Data Source=database.db");
        await connection.ExecuteAsync(
            "DELETE FROM [Midia] WHERE [Id]=@id",
            new { id = pId });

        return await Delete(pId);
    }
}
