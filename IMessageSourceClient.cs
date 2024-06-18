namespace LessonNetClient
{
	public interface IMessageSourceClient
	{
		Task SendMessageAsync(string message, CancellationToken token);
	}
}
