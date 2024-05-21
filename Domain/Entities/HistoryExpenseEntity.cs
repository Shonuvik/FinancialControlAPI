using Domain.Enums;

namespace Domain.Entities
{
    public class HistoryExpenseEntity : Entity
	{
        public CategoryType Category { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}

