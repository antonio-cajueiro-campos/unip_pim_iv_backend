using System.ComponentModel;
using System.Dynamic;
using System.Reflection;


namespace TSB.Portal.Backend.CrossCutting.Extensions;

public static class ObjectMapper
{
	public static TResult MapObjectTo<TSource, TResult>(this TSource objFrom, TResult objTo)
	{

		PropertyInfo[] ToProperties = objTo.GetType().GetProperties();
		PropertyInfo[] FromProperties = objFrom.GetType().GetProperties();

		ToProperties.ToList().ForEach(objToProp =>
		{
			PropertyInfo FromProp = FromProperties.FirstOrDefault(objFromProp =>
				objFromProp.Name == objToProp.Name && objFromProp.PropertyType == objToProp.PropertyType
			);

			if (FromProp != null) objToProp.SetValue(objTo, FromProp.GetValue(objFrom));

		});
		return objTo;
	}

	public static TResult MapObjectToIfNotNull<TSource, TResult>(this TSource NovosDados, TResult DadosAntigos)
	{

		PropertyInfo[] PropsAntigas = DadosAntigos.GetType().GetProperties();
		PropertyInfo[] PropsNovas = NovosDados.GetType().GetProperties();

		PropsAntigas.ToList().ForEach(propAntiga =>
		{
			PropertyInfo novaProp = PropsNovas.FirstOrDefault(objFromProp =>
				objFromProp.Name == propAntiga.Name && objFromProp.PropertyType == propAntiga.PropertyType
			);

			if (novaProp != null && novaProp.GetValue(NovosDados) != null) propAntiga.SetValue(DadosAntigos, novaProp.GetValue(NovosDados));

		});
		return DadosAntigos;
	}

	public static dynamic MapObjectToDynamic(this object value)
	{
		IDictionary<string, object> expando = new ExpandoObject();

		foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
			expando.Add(property.Name, property.GetValue(value));

		return expando as ExpandoObject;
	}
}
