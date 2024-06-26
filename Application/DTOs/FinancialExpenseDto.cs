﻿using Domain.Enums;

namespace Application.DTOs
{
    public class FinancialExpenseDto
    {
        public decimal Value { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}

