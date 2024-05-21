using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class FinancialExpenseEntity : Entity
    {
        public FinancialExpenseEntity() { }

        public FinancialExpenseEntity(
            long userId,
            CategoryType categoryType,
            decimal value,
            string description,
            DateTime transactionDate)
        {
            UserId = userId;
            Category = categoryType;
            Value = value;
            Description = description;
            TransactionDate = transactionDate;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public long UserId { get; set; }

        public CategoryType Category { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreatedAt { get; set; }

        private void Validate()
        {
            if (Value <= 0)
                throw new ExceptionBusiness("Valor deve ser maior que zero.");

            if (TransactionDate == default)
                throw new ExceptionBusiness("Data da transacão inválida.");

            if (UserId <= 0)
                throw new ExceptionBusiness("Usuario inválido.");

            Category = Category == CategoryType.Default ? CategoryType.Monthly : Category;
        }
    }
}

