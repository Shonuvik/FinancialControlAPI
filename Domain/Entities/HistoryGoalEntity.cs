using Domain.Enums;

namespace Domain.Entities
{
    public class HistoryGoalEntity
    {
        public CategoryFinancialGoalType Category { get; set; }
        public decimal ActualValue { get; set; }
        public decimal TargetValue { get; set; }
        public DateTime TargetDate { get; set; }
        public string Description { get; set; }
    }
}

