using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Presentation.Extensions;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FinancialControlController : ControllerBase
{
    private readonly IFinancialControlService _financialControlService;

    public FinancialControlController(IFinancialControlService financialControlService)
    {
        _financialControlService = financialControlService;
    }

    [HttpGet("GetHistoryExpense")]
    [ProducesResponseType(typeof(List<HistoryExpenseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(CategoryType category)
    {
        Request.Headers.TryGetValue("Authorization", out StringValues values);
        var userName = values.GetName();

        var result = await _financialControlService.FindHistoryAsync(category, userName);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("NewExpense")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(FinancialExpenseDto request, [FromQuery] CategoryType categoryType)
    {
        Request.Headers.TryGetValue("Authorization", out StringValues values);
        var userName = values.GetName();

        await _financialControlService.NewExpenseAsync(request, userName, categoryType);
        return Ok();
    }
}

