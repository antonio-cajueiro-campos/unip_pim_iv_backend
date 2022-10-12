using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.EntitiesUseCase;

namespace TSB.Portal.Backend.Application.UseCases.GetPriceSelectors;
public class GetPriceSelectorsUseCase : IDefaultUseCase<GetPriceSelectorsOutput, GetPriceSelectorsInput> {
	private DataContext database { get; set; }
	public GetPriceSelectorsUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetPriceSelectorsOutput> Handle(GetPriceSelectorsInput priceSelectorsInput) {
		return UserRegister(priceSelectorsInput);
	}

	private DefaultResponse<GetPriceSelectorsOutput> UserRegister(GetPriceSelectorsInput priceSelectorsInput) {

		try {
			return new() {
				Status = 200,
				Error = false,
				Message = Messages.Success,
				Data = new () {
					PriceSelectorList = this.GetPriceSelectorList()
				}
			};
		} catch (Exception ex) {
			return new() {
				Status = 500,
				Error = true,
				Message = Messages.Error + ex,
				Data = null
			};
		}
	}

	private List<PriceSelector> GetPriceSelectorList() {
		return new List<PriceSelector>(){
			new () {
				Title = "Incêndio, raio e explosão",
				Icon = "fire",
				Total = 0,
				List = new () {
					new () {
						Text = "DEF",
						Value = 10
					}
				}
			},
			new () {
				Title = "Perda e pagamento de aluguel",
				Icon = "building",
				Total = 0,
				List = new () {
					new () {
						Text = "DEF",
						Value = 10
					}
				}
			},
			new () {
				Title = "Vendaval, granizo e ciclone",
				Icon = "wind",
				Total = 0,
				List = new () {
					new () {
						Text = "DEF",
						Value = 10
					}
				}
			},
			new () {
				Title = "Responsabilidade civil familiar",
				Icon = "user-group",
				Total = 0,
				List = new () {
					new () {
						Text = "DEF",
						Value = 10
					}
				}
			},
			new () {
				Title = "Danos elétricos",
				Icon = "bolt",
				Total = 0,
				List = new () {
					new () {
						Text = "DEF",
						Value = 10
					}
				}
			},
			new () {
				Title = "Roubo",
				Icon = "shield",
				Total = 0,
				List = new () {
					new () {
						Text = "DEF",
						Value = 10
					}
				}
			},
		};
	}
}