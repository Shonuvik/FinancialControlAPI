using System;
using Domain.Enums;

namespace Application.DTOs
{
    public class FinancialGoalDto
    {
        public decimal ActualValue { get; set; }
        public decimal TargetValue { get; set; }
        public DateTime TargetDate { get; set; }
        public string Description { get; set; }
    }
}

