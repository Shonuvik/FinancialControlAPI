using System;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class FinancialGoalEntity : Entity
    {
        public FinancialGoalEntity() { }

        public FinancialGoalEntity(long userId,
                                   CategoryFinancialGoalType category,
                                   decimal actualValue,
                                   decimal targetValue,
                                   DateTime targetDate,
                                   string description)
        {
            UserId = userId;
            Category = category;
            ActualValue = actualValue;
            TargetValue = targetValue;
            TargetDate = TargetDate;
            Description = description;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public long UserId { get; set; }
        public CategoryFinancialGoalType Category { get; set; }
        public decimal ActualValue { get; set; }
        public decimal TargetValue { get; set; }
        public DateTime TargetDate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        private void Validate()
        {
            if (UserId <= 0)
                throw new ExceptionBusiness("Usuário informado nao exites.");

            if (ActualValue <= 0)
                throw new ExceptionBusiness("Valor deve ser maior que zero.");

            if (TargetValue <= 0)
                throw new ExceptionBusiness("Valor alvo deve ser maior que zero.");

            if (string.IsNullOrEmpty(Description))
                throw new ExceptionBusiness("Descricao deve ser informada.");
        }
    }
}

