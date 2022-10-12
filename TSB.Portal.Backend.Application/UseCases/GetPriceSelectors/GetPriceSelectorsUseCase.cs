using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.EntitiesUseCase;

namespace TSB.Portal.Backend.Application.UseCases.GetPriceSelectors;
public class GetPriceSelectorsUseCase : IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput>
{
	private DataContext database { get; set; }
	public GetPriceSelectorsUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetPriceSelectorsOutput> Handle(GetPriceSelectorsInput priceSelectorsInput)
	{
		return UserRegister(priceSelectorsInput);
	}

	private DefaultResponse<GetPriceSelectorsOutput> UserRegister(GetPriceSelectorsInput priceSelectorsInput)
	{

		try
		{
			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Success,
				Data = new()
				{
					PriceSelectorList = this.GetPriceSelectorList()
				}
			};
		}
		catch (Exception ex)
		{
			return new()
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
		return new List<PriceSelector>(){
			new () {
				Title = "Incêndio, raio e explosão",
				Icon = "fire",
				Total = 0,
				List = GetListSelectorPrices()
			},
			new () {
				Title = "Perda e pagamento de aluguel",
				Icon = "building",
				Total = 0,
				List = GetListSelectorPrices()
			},
			new () {
				Title = "Vendaval, granizo e ciclone",
				Icon = "wind",
				Total = 0,
				List = GetListSelectorPrices()
			},
			new () {
				Title = "Responsabilidade civil familiar",
				Icon = "user-group",
				Total = 0,
				List = GetListSelectorPrices()
			},
			new () {
				Title = "Danos elétricos",
				Icon = "bolt",
				Total = 0,
				List = GetListSelectorPrices()
			},
			new () {
				Title = "Roubo",
				Icon = "shield",
				Total = 0,
				List = GetListSelectorPrices()
			},
		};
	}

	private List<Selector> GetListSelectorPrices()
	{
		return new List<Selector>()
		{
			new ()
			{
				Text = "R$ 1.000,00",
				Value = 1.000M
			},
			new ()
			{
				Text = "R$ 5.000,00",
				Value = 5.000M
			},
			new ()
			{
				Text = "R$ 10.000,00",
				Value = 10.000M
			},
			new ()
			{
				Text = "R$ 25.000,00",
				Value = 25.000M
			},
			new ()
			{
				Text = "R$ 50.000,00",
				Value = 50.000M
			},
			new ()
			{
				Text = "R$ 100.000,00",
				Value = 100.000M
			},
		};
	}
}