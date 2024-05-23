using LessonNet;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace LessonNetClient
{
	public class Program
	{
		public static void Main(string[] args)
		{
			UdpClientSend();
		}

		public static void UdpClientSend()
		{
			UdpClient udpClient = new UdpClient();
			IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 12345);

			Console.WriteLine("Введите сообщение (или 'Exit' для завершения):");

			while (true)
			{
				string input = Console.ReadLine();
				if (input.Equals("Exit", StringComparison.OrdinalIgnoreCase))
				{
					SendMessage(udpClient, endPoint, input);
					break;
				}

				SendMessage(udpClient, endPoint, input);

				// Получение подтверждения от сервера
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
				byte[] confirmationBuffer = udpClient.Receive(ref remoteEP);
				string confirmationMessage = Encoding.UTF8.GetString(confirmationBuffer);
				Console.WriteLine($"Сервер ответил: {confirmationMessage}");
			}

			udpClient.Close();
			Console.WriteLine("Клиент завершил работу.");
		}

		private static void SendMessage(UdpClient udpClient, IPEndPoint endPoint, string text)
		{
			Messenge msg = new Messenge() { Text = text, DateTime = DateTime.Now, NicNameFrom = "from", NicNameTo = "all" };
			string json = msg.SerializeMessToJson();
			byte[] buffer = Encoding.UTF8.GetBytes(json);
			udpClient.Send(buffer, buffer.Length, endPoint);
		}
	}
}
