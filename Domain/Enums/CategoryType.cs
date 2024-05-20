using System.ComponentModel;

namespace Domain.Enums
{
    public enum CategoryType
	{
		[Description("Padrao")]
		Default,

		[Description("Mensal")]
		Monthly,

		[Description("Saúde")]
		Health,

		[Description("Alimentacao")]
		Food,

		[Description("Transporte")]
		Transport
	}
}

