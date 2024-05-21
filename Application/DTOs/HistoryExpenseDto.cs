using Domain.Enums;

namespace Application.DTOs
{
    public class HistoryExpenseDto
	{
        public CategoryType Category { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}

