using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.EntitiesUseCase;

namespace TSB.Portal.Backend.Application.UseCases.GetPriceSelectors;
public class GetPriceSelectorsUseCase : IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput>
{
	private readonly DataContext database;
	public GetPriceSelectorsUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetPriceSelectorsOutput> Handle(GetPriceSelectorsInput priceSelectorsInput)
	{
		return GetPriceSelectors(priceSelectorsInput);
	}

	private DefaultResponse<GetPriceSelectorsOutput> GetPriceSelectors(GetPriceSelectorsInput priceSelectorsInput)
	{
		try
		{
			return new ()
			{
				Status = 200,
				Error = false,
				Message = Messages.Success,
				Data = new ()
				{
					PriceSelectorList = this.GetPriceSelectorList()
				}
			};
		}
		catch (Exception ex)
		{
			return new ()
			{
				Status = 500,
				Error = true,
				Message = Messages.Error + ex,
				Data = null
			};
		}
	}

	private List<PriceSelector> GetPriceSelectorList()
	{
		return new ()
		{
			new ()
			{
				Id = "IRE",
				Title = "Incêndio, raio e explosão",
				Icon = "fire",
				MonthlyPayment = 0M,
				NumberOfMonths = 26,
				List = GetListSelectorPrices()
			},
			new ()
			{
				Id = "PP",
				Title = "Perda e pagamento de aluguel",
				Icon = "building",
				MonthlyPayment = 0M,
				NumberOfMonths = 32,
				List = GetListSelectorPrices()
			},
			new ()
			{
				Id = "VGC",
				Title = "Vendaval, granizo e ciclone",
				Icon = "wind",
				MonthlyPayment = 0M,
				NumberOfMonths = 54,
				List = GetListSelectorPrices()
			},
			new ()
			{
				Id = "RCF",
				Title = "Responsabilidade civil familiar",
				Icon = "user-group",
				MonthlyPayment = 0M,
				NumberOfMonths = 41,
				List = GetListSelectorPrices()
			},
			new ()
			{
				Id = "DE",
				Title = "Danos elétricos",
				Icon = "bolt",
				MonthlyPayment = 0M,
				NumberOfMonths = 71,
				List = GetListSelectorPrices()
			},
			new ()
			{
				Id = "RO",
				Title = "Roubo",
				Icon = "shield",
				MonthlyPayment = 0M,
				NumberOfMonths = 22,
				List = GetListSelectorPrices()
			},
		};
	}

	private List<Selector> GetListSelectorPrices()
	{
		return new ()
		{
			new ()
			{
				Text = "R$ 0,00",
				Value = decimal.Zero
			},
			new ()
			{
				Text = "R$ 1.000,00",
				Value = 1000M
			},
			new ()
			{
				Text = "R$ 5.000,00",
				Value = 5000M
			},
			new ()
			{
				Text = "R$ 10.000,00",
				Value = 10000M
			},
			new ()
			{
				Text = "R$ 25.000,00",
				Value = 25000M
			},
			new ()
			{
				Text = "R$ 50.000,00",
				Value = 50000M
			},
			new ()
			{
				Text = "R$ 100.000,00",
				Value = 100000M
			}
		};
	}
}
// Server Driven UI