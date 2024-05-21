using System;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Presentation.Extensions;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FinancialGoalController : ControllerBase
    {
        private readonly IFinancialGoalService _financialGoalService;

        public FinancialGoalController(IFinancialGoalService financialGoalService)
        {
            _financialGoalService = financialGoalService;
        }

        [HttpGet("GetProgress")]
        [ProducesResponseType(typeof(List<HistoryGoalDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(CategoryFinancialGoalType category)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues values);
            var userName = values.GetName();

            var result = await _financialGoalService.FindProgressAsync(category, userName);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("NewGoal")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(FinancialGoalDto requestDto, [FromQuery] CategoryFinancialGoalType category)
        {
            Request.Headers.TryGetValue("Authorization", out StringValues value);
            var userName = value.GetName();

            await _financialGoalService.AddNewGoalAsync(requestDto, userName, category);
            return Ok();
        }
    }
}

