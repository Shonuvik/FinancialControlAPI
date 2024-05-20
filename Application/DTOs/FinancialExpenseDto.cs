using Domain.Enums;

namespace Application.DTOs
{
    public class FinancialExpenseDto
    {
        public string UserName { get; set; }

        public CategoryType CategoryType { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}

