
namespace TSB.Portal.Backend.CrossCutting.Extensions
{
    public static class DecimalExtensions
    {
		public static decimal GetValueOrZero(this decimal? number)
		{
			return number ?? decimal.Zero;
		}
    }
}
