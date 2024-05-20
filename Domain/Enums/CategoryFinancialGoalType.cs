using System;
using System.ComponentModel;

namespace Domain.Enums
{
	public enum CategoryFinancialGoalType
	{
		[Description("Viagem")]
		Trip,

		[Description("Reserva de emergencia")]
		EmergencyReserve,

		[Description("Estudos")]
		Studies,

		[Description("Reforma")]
		Remodeling,
	}
}

