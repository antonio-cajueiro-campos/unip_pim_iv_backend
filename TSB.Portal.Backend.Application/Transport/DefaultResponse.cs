namespace TSB.Portal.Backend.Application.Transport;
public class DefaultResponse<T>
{
	public int Status { get; set; }
	public bool Error { get; set; }
	public string Message { get; set; }
	public T Data { get; set; }

}