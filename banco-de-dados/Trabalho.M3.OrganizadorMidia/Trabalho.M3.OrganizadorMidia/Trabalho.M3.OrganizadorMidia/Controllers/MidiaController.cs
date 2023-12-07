using Microsoft.AspNetCore.Mvc;
using Trabalho.M3.OrganizadorMidia.Entites;

namespace Trabalho.M3.OrganizadorMidia.Controllers;

[ApiController]
[Route("[controller]")]
public class MidiaController : ControllerBase
{
    public MidiaController()
    {
    }

    [HttpPost]
    public ActionResult<int> Adicionar(Entites.Midia pMidia)
    {
        return Created(nameof(Entites.Midia), pMidia);
    }

    [HttpGet]
    public List<Entites.Midia> Obter()
    {
        return new List<Entites.Midia>();
    }

    [HttpPut("{pId}")]
    public ActionResult Editar(int pId, [FromBody]Entites.Midia pMidia)
    {
        if (pId != pMidia.Id)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{pId}")]
    public ActionResult Delete(int pId)
    {
        return Delete(pId);
    }
}
