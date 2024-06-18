using LessonNet;
using NetMQ;
using NetMQ.Sockets;

namespace LessonNetClient
{
	public class NetMqClient : IMessageSourceClient
	{
		public async Task SendMessageAsync(string message, CancellationToken token)
		{
			using (var client = new RequestSocket(">tcp://localhost:12345"))
			{
				Messenge msg = new Messenge() { Text = message, DateTime = DateTime.Now, NicNameFrom = "from", NicNameTo = "all" };
				string json = msg.SerializeMessToJson();

				client.SendFrame(json);

				var response = await Task.Run(() => client.ReceiveFrameString(), token);
				Console.WriteLine($"Сервер ответил: {response}");
			}
		}
	}
}
