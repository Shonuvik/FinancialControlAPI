using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancialControlController : ControllerBase
{
    public FinancialControlController()
    {
    }

    [HttpPost("NewExpense")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(FinancialExpenseDto request)
    {//incluir a propriedade UserName em todos os request, com intuito de capturarmos
        //o nome do usario que esta cadastrando o recurso via header do jwt informado
        return Ok();
    }
}

